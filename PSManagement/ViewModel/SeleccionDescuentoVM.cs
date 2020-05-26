using PSManagement.Model;
using PSManagement.Service;

using System.Collections.ObjectModel;
using System.ComponentModel;


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

        //Constructor
        public SeleccionDescuentoVM(facturas factura, ObservableCollection<detallesfactura> detallesfactura)
        {
            ListaDescuentos = BbddService.GetDescuentos();
            FacturaDescuento = factura;
            DetallesEnFactura = detallesfactura;
            DetallesSeleccionadosParaDescuento = new ObservableCollection<detallesfactura>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal bool DiscountCanExecute => !(DescuentoSeleccionado is null);

        /// <summary>
        /// Función para ejecutar el comando para aplicar un descuento.
        /// </summary>
        /// <returns>Verdadero si se ha aplicado el descuento, falso en cualquier otro caso</returns>
        internal bool DiscountExecuted()
        {

            //Si las unidades necesarias para aplicar un descuento es 0 el descuento se aplica automáticamente. En otro caso se evalúan las condiciones.
            if (DescuentoSeleccionado.UnidadesCondicionAplicar != 0)
            {
                //UDExactas significa que se tienen que haber seleccionado las unidades exactas que el descuento exge para aplicarse
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
                //El otro caso es si se cumple un mínimo de unidades para que el descuento se aplique.
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

        /// <summary>
        /// Función que determina si se pueden seleccionar artículos si se ha seleccionado un descuento antes.
        /// </summary>
        /// <returns>Verdadero si se ha seleccionado un descuento, falso en cualquier otro caso</returns>
        internal bool PuedeSeleccionarArticulos()
        {
            return DescuentoSeleccionado != null;
        }

        //Añade un elemento de la lista de elementos a los que se va a aplicar el descuento
        internal void AddListaDescuentos(detallesfactura content)
        {
            DetallesSeleccionadosParaDescuento.Add(content);
            CantidadArticulosEnFactura += content.CantidadArticulo;
        }

        //Elimina un elemento de la lista de elementos a los que se va a aplicar el descuento
        internal void RemoveListaDescuentos(detallesfactura content)
        {
            DetallesSeleccionadosParaDescuento.Remove(content);
            CantidadArticulosEnFactura -= content.CantidadArticulo;
        }

        //Aplica el descuento y lo resta al precio de la factura actual
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
