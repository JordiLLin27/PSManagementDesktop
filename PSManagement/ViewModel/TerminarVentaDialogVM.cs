using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using PSManagement.Model;
using PSManagement.Service;

namespace PSManagement.ViewModel
{
    class TerminarVentaDialogVM : INotifyPropertyChanged
    {
        public facturas FacturaFinal { get; set; }
        public string CantidadAbonadaCliente { get; set; }
        public float CambioCliente { get; set; }
        public ObservableCollection<detallesfactura> DetallesFactura { get; set; }

        public TerminarVentaDialogVM(facturas factura, ObservableCollection<detallesfactura> detallesArticulosFactura)
        {
            FacturaFinal = factura;
            DetallesFactura = detallesArticulosFactura;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Controla si se puede ejecutar el comando para terminar la venta.
        /// </summary>
        /// <returns>Devuelve verdadero si se ha introducido un importe correcto, o si se ha introducido el método de pago por tarjeta, falso en otro caso</returns>
        internal bool SaveCanExecute()
        {
            if (!string.IsNullOrEmpty(FacturaFinal.TipoDePago))
            {
                if (FacturaFinal.TipoDePago.Equals("Efectivo"))
                {
                    //Expresión que controla si se introduce bien un importe
                    Regex patron = new Regex(@"^\d+(,\d{1,2})?$");

                    if (!string.IsNullOrEmpty(CantidadAbonadaCliente))

                        if (patron.IsMatch(CantidadAbonadaCliente))
                            return float.Parse(CantidadAbonadaCliente) > 0 && float.Parse(CantidadAbonadaCliente) >= Math.Round(FacturaFinal.ImporteTotalConIva, 2);
                        else
                            return false;
                    else
                        return false;
                }
                else
                    return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Calcula el cambio a devolver al cliente.
        /// </summary>
        public void CalcularCambio()
        {
            float x, y;

            x = float.Parse(CantidadAbonadaCliente);
            y = FacturaFinal.ImporteTotalConIva;

            CambioCliente = x - y;
        }

        //Cambia el tipo de pago de la factura a 'Efectivo'
        internal void SetTipoPagoEfectivo()
        {
            FacturaFinal.TipoDePago = "Efectivo";
        }

        //Cambia el tipo de pago de la factura a 'Tarjeta'
        internal void SetTipoPagoTarjeta()
        {
            FacturaFinal.TipoDePago = "Tarjeta";
        }

        /// <summary>
        /// Ejecuta el comando de terminar la venta.
        /// </summary>
        /// <returns>Verdadero si se ha completado el proceso de terminar la venta, falso en otro caso</returns>
        internal bool SaveExecuted()
        {
            try
            {
                //Asigna la fecha actual a ala factura
                FacturaFinal.FechaCreacion = DateTime.Now;
                FacturaFinal.ImporteTotalSinIVa = PrecioSinIVa();

                //Añade la factura a la base de datos
                BbddService.AddFactura(FacturaFinal);

                //Añade todos los detalles de la factura a la base de datos
                foreach (detallesfactura item in DetallesFactura)
                {
                    item.CodFactura = FacturaFinal.IdFactura;
                    BbddService.AddDetallesAFactura(item);
                }

                //Imprime la factura
                ImprimirFactura();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Imprime un fichero .txt en la ruta seleccionada con el texto personalizado sobre la venta.
        /// </summary>
        private void ImprimirFactura()
        {
            StringBuilder cadena = new StringBuilder();

            string rutaNombreFactura = Properties.Settings.Default.RutaFacturasDefault + @"\factura" + FacturaFinal.IdFactura + DateTime.Now.ToShortDateString().Replace("/", "")
                + DateTime.Now.ToShortTimeString().Replace(":", "") + ".txt";

            cadena.Append("Factura realizada el: " + DateTime.Now.ToLongDateString()
                + "\nDetalles de la factura:\n---------------------------------------------------\n");

            foreach (detallesfactura item in DetallesFactura)
            {
                cadena.Append(item.ARTICULO.Nombre + "\t\t" + item.TallaONum + "\t\tuds: " + item.CantidadArticulo + "\t\t" + Math.Round(item.ARTICULO.PrecioUnitario, 2) + "€\n");
            }

            if (FacturaFinal.TipoDePago == "Efectivo")
            {
                cadena.Append("---------------------------------------------------\n" +
                    "\t\t\tImporte total sin IVA: " + Math.Round(FacturaFinal.ImporteTotalSinIVa, 2) + " €\n\t\t\t" + "IVA: " + VatApiService.RescatarIva() +
                    "%\n\t\t\tImporte total con iva: " + Math.Round(FacturaFinal.ImporteTotalConIva, 2) + "\n\t\t\tImporte abonado por el cliente: " + CantidadAbonadaCliente + " €\n\t\t\tDevolución cambio: " + Math.Round(CambioCliente, 2)
                    + " €\nTipo de pago: " + FacturaFinal.TipoDePago);
            }
            else
            {
                cadena.Append("---------------------------------------------------\n" +
                    "\t\t\tImporte total sin IVA: " + Math.Round(FacturaFinal.ImporteTotalSinIVa, 2) + " €\n\t\t\t" + "IVA: " + VatApiService.RescatarIva() +
                    "%\n\t\t\tImporte total con iva: " + Math.Round(FacturaFinal.ImporteTotalConIva, 2) + " €\nTipo de pago: " + FacturaFinal.TipoDePago);
            }

            File.WriteAllText(rutaNombreFactura, cadena.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// Recupera el iva y lo resta al precio total de la factura. Para mostrarlo en la factura.
        /// </summary>
        /// <returns></returns>
        private float PrecioSinIVa()
        {
            int iva = VatApiService.RescatarIva();

            float porcentajeIva = (float)iva / 100;

            float precioSinIva = FacturaFinal.ImporteTotalConIva - (porcentajeIva * FacturaFinal.ImporteTotalConIva);

            return precioSinIva;
        }
    }
}
