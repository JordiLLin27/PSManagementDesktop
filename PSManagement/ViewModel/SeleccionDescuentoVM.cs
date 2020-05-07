using PSManagement.Model;
using PSManagement.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSManagement.ViewModel
{
    class SeleccionDescuentoVM : INotifyPropertyChanged
    {
        public ObservableCollection<descuentos> ListaDescuentos { get; set; }
        public ObservableCollection<detallesfactura> DetallesEnFactura { get; set; }
        public ObservableCollection<detallesfactura> DetallesSeleccionadosParaDescuento { get; set; }


        public descuentos DescuentoSeleccionado { get; set; }
        public facturas FacturaDescuento { get; set; }
        private int CantidadArticulosEnFactura { get; set; }

        public SeleccionDescuentoVM(facturas factura, ObservableCollection<detallesfactura> detallesfactura)
        {
            ListaDescuentos = BBDDService.GetDescuentos();
            FacturaDescuento = factura;
            DetallesEnFactura = detallesfactura;
            DetallesSeleccionadosParaDescuento = new ObservableCollection<detallesfactura>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal bool DiscountCanExecute => !(DescuentoSeleccionado is null);

        internal bool DiscountExecuted()
        {
            if (DescuentoSeleccionado.UnidadesCondicionAplicar != 0)
            {
                if (DescuentoSeleccionado.TipoCondicion == "UDExactas")
                {
                    if (CantidadArticulosEnFactura == DescuentoSeleccionado.UnidadesCondicionAplicar)
                    {
                        AplicarDescuento();
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    if (CantidadArticulosEnFactura >= DescuentoSeleccionado.UnidadesCondicionAplicar)
                    {
                        AplicarDescuento();
                        return true;
                    }
                    else
                        return false;
                }
            }
            else
            {
                AplicarDescuento();
                return true;
            }
        }

        internal bool PuedeSeleccionarArticulos()
        {
            return DescuentoSeleccionado != null;
        }

        internal void AddListaDescuentos(detallesfactura content)
        {
            DetallesSeleccionadosParaDescuento.Add(content);
            CantidadArticulosEnFactura += content.CantidadArticulo;
        }

        internal void RemoveListaDescuentos(detallesfactura content)
        {
            DetallesSeleccionadosParaDescuento.Remove(content);
            CantidadArticulosEnFactura -= content.CantidadArticulo;
        }

        private void AplicarDescuento()
        {
            float precioSeleccionado = 0;

            foreach (detallesfactura item in DetallesSeleccionadosParaDescuento)
            {
                precioSeleccionado += (item.CantidadArticulo * item.ARTICULO.PrecioUnitario);
            }

            float porcentajeARestar = precioSeleccionado * (DescuentoSeleccionado.DescuentoAplicar / 100);

            FacturaDescuento.ImporteTotalConIva = FacturaDescuento.ImporteTotalConIva - porcentajeARestar;
        }
    }
}
