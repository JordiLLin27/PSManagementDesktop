using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSManagement.Model;
using PSManagement.Service;

namespace PSManagement.ViewModel
{
    class TerminarVentaDialogVM : INotifyPropertyChanged
    {
        public facturas FacturaFinal { get; set; }
        public float CantidadAbonadaCliente { get; set; }
        public float CambioCliente { get; set; }
        public ObservableCollection<detallesfactura> DetallesFactura { get; set; }

        public TerminarVentaDialogVM(facturas factura, ObservableCollection<detallesfactura> detallesArticulosFactura)
        {
            FacturaFinal = factura;
            DetallesFactura = detallesArticulosFactura;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal bool SaveCanExecute()
        {
            if (!string.IsNullOrEmpty(FacturaFinal.TipoDePago))
            {
                if (FacturaFinal.TipoDePago.Equals("Efectivo"))
                {
                    return CantidadAbonadaCliente > 0 && CantidadAbonadaCliente >= Math.Round(FacturaFinal.ImporteTotalConIva, 2);
                }
                else
                    return true;
            }
            else
                return false;
        }

        public void CalcularCambio()
        {
            float x, y;

            x = CantidadAbonadaCliente;
            y = FacturaFinal.ImporteTotalConIva;

            CambioCliente = x - y;
        }

        internal void SetTipoPagoEfectivo()
        {
            FacturaFinal.TipoDePago = "Efectivo";
        }

        internal void SetTipoPagoTarjeta()
        {
            FacturaFinal.TipoDePago = "Tarjeta";
        }

        internal bool SaveExecuted()
        {
            try
            {

                FacturaFinal.FechaCreacion = DateTime.Now;
                FacturaFinal.ImporteTotalSinIVa = PrecioSinIVa();


                BbddService.AddFactura(FacturaFinal);

                foreach (detallesfactura item in DetallesFactura)
                {
                    item.CodFactura = FacturaFinal.IdFactura;
                    BbddService.AddDetallesAFactura(item);
                }

                ImprimirFactura();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ImprimirFactura()
        {
            StringBuilder cadena = new StringBuilder();

            string rutaNombreFactura = Properties.Settings.Default.RutaFacturasDefault + @"\factura" + FacturaFinal.IdFactura + DateTime.Now.ToShortDateString().Replace("/", "") + DateTime.Now.ToShortTimeString().Replace(":", "") + ".txt"; ;

            cadena.Append("Factura realizada el: " + DateTime.Now.ToLongDateString() + "\nDetalles de la factura:\n---------------------------------------------------\n");

            foreach (detallesfactura item in DetallesFactura)
            {
                cadena.Append(item.ARTICULO.Nombre + "\t\t" + item.TallaONum + "\t\tuds: " + item.CantidadArticulo + "\t\t" + Math.Round(item.ARTICULO.PrecioUnitario, 2) + "€\n");
            }
            cadena.Append("---------------------------------------------------\n" +
                "\t\t\tImporte total sin IVA: " + Math.Round(FacturaFinal.ImporteTotalSinIVa, 2) + " €\n\t\t\t" + "IVA: " + VatApiService.RescatarIva() + "%\n\t\t\tImporte total con iva: " + Math.Round(FacturaFinal.ImporteTotalConIva, 2) + " €\nTipo de pago: " + FacturaFinal.TipoDePago);


            File.WriteAllText(rutaNombreFactura, cadena.ToString(), Encoding.UTF8);
        }

        private float PrecioSinIVa()
        {
            int iva = VatApiService.RescatarIva();

            float porcentajeIva = (float)iva / 100;

            float precioSinIva = FacturaFinal.ImporteTotalConIva - (porcentajeIva * FacturaFinal.ImporteTotalConIva);

            return precioSinIva;
        }
    }
}
