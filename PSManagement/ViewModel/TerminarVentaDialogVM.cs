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
        public double CantidadAbonadaCliente { get; set; }
        public double CambioCliente { get; set; }
        public ObservableCollection<detallesfactura> DetallesFactura { get; set; }

        public TerminarVentaDialogVM(facturas factura, ObservableCollection<detallesfactura> detallesArticulosFactura)
        {
            FacturaFinal = factura;
            DetallesFactura = detallesArticulosFactura;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal bool SaveCanExecute()
        {
            return CantidadAbonadaCliente > 0 && CantidadAbonadaCliente >= FacturaFinal.ImporteTotalConIva;
        }

        public void CalcularCambio()
        {
            float x, y;

            x = (float)CantidadAbonadaCliente;
            y = (float)FacturaFinal.ImporteTotalConIva;

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

                FacturaFinal.ImporteTotalConIva = Math.Round(FacturaFinal.ImporteTotalConIva, 2);
                FacturaFinal.ImporteTotalSinIVa = Math.Round(FacturaFinal.ImporteTotalSinIVa, 2);

                ImprimirFactura();
                BBDDService.AddFactura(FacturaFinal);

                foreach (detallesfactura item in DetallesFactura)
                {
                    item.CodFactura = FacturaFinal.IdFactura;
                    BBDDService.AddDetallesAFactura(item);
                }
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

            cadena.Append("Factura realizada el: " + DateTime.Now.ToLongDateString() + "\nDetalles de la factura:\n---------------------------------------------------\n");

            foreach (detallesfactura item in DetallesFactura)
            {
                cadena.Append(item.ARTICULO.Nombre + "\t\t\t\tuds: " + item.CantidadArticulo + "\t\t" + Math.Round(item.ARTICULO.PrecioUnitario, 2) + "€\n");
            }
            cadena.Append("---------------------------------------------------\n" +
                "\t\t\tImporte total sin IVA: " + FacturaFinal.ImporteTotalSinIVa + " €\n\t\t\t" + "IVA: " + VatApiService.RescatarIva() + "%\n\t\t\tImporte total con iva: " + FacturaFinal.ImporteTotalConIva + " €");

            Properties.Settings.Default.RutaFacturasDefault += @"\factura" + FacturaFinal.IdFactura + FacturaFinal.FechaCreacion.ToShortDateString().Replace("/", "") + ".txt";
            File.WriteAllText(Properties.Settings.Default.RutaFacturasDefault, cadena.ToString(), Encoding.UTF8);
        }

        private double PrecioSinIVa()
        {
            int iva = VatApiService.RescatarIva();

            double porcentajeIva = (double)iva / 100;

            double precioSinIva = FacturaFinal.ImporteTotalConIva - (porcentajeIva * FacturaFinal.ImporteTotalConIva);

            return precioSinIva;
        }
    }
}
