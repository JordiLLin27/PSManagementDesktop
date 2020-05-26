using PSManagement.Dialogs;
using PSManagement.Model;
using PSManagement.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace PSManagement.ViewModel
{
    class PanelVentasVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CollectionViewSource ListaArticulosSeleccionados { get; set; }
        public ObservableCollection<categorias> ListaCategorias { get; set; }
        public ObservableCollection<colores> ListaColores { get; set; }
        public ObservableCollection<tallastextiles> ListaTallasTextiles { get; set; }
        public ObservableCollection<numeroscalzado> ListaNumerosCalzado { get; set; }
        public ObservableCollection<detallesfactura> DetallesArticulosFactura { get; set; }

        public colores ColorSeleccionado { get; set; }
        public articulos ArticuloSeleccionado { get; set; }
        public categorias CategoriaSeleccionada { get; set; }

        public detallesfactura DetallesFacturaSeleccionado { get; set; }
        public facturas FacturaFinal { get; set; }

        public int CantidadElementosFactura { get; set; }

        public string FiltroTextBox { get; set; }

        public bool DescuentoAplicado { get; set; }



        public PanelVentasVM()
        {
            ListaCategorias = BbddService.GetCategorias();
            ListaTallasTextiles = BbddService.GetTallasTextiles();
            ListaNumerosCalzado = BbddService.GetNumerosCalzado();
            ListaColores = BbddService.GetColores();

            FacturaFinal = new facturas();
            DetallesArticulosFactura = new ObservableCollection<detallesfactura>();

            CantidadElementosFactura = 0;

        }

        //Filtro para la tabla de de artículos en el panel de ventas
        private void ListaArticulosSeleccionados_Filter(object sender, FilterEventArgs e)
        {
            articulos item = (articulos)e.Item;

            //Si no hay filtros seleccionados
            if (string.IsNullOrEmpty(FiltroTextBox) && ColorSeleccionado == null)
                e.Accepted = true;
            else
            {
                //Si coincide el color seleccionado
                if (string.IsNullOrEmpty(FiltroTextBox) && ColorSeleccionado != null)
                {
                    if (item.COLOR == ColorSeleccionado)
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si coincide nombre/modelo
                else if (!string.IsNullOrEmpty(FiltroTextBox) && ColorSeleccionado == null)
                {
                    if (item.Nombre.ToLower().Contains(FiltroTextBox.ToLower()) || item.CodArticulo.ToLower().Contains(FiltroTextBox.ToLower()))
                    {
                        e.Accepted = true;

                    }
                    else
                        e.Accepted = false;
                }
                //Si coinciden nombre/modelo y color
                else if (!string.IsNullOrEmpty(FiltroTextBox) && ColorSeleccionado != null)
                {
                    if ((item.Nombre.ToLower().Contains(FiltroTextBox.ToLower()) || item.CodArticulo.ToLower().Contains(FiltroTextBox.ToLower())) && item.COLOR == ColorSeleccionado)
                    {
                        e.Accepted = true;
                    }
                    else
                        e.Accepted = false;
                }
            }
        }

        //Elimina detalles de la factura actual
        internal void BorrarItems(detallesfactura detallesfacturaABorrar)
        {
            //Si el artículo a eliminar contiene más de una unidad se resta una a los detalles de la factura
            if (detallesfacturaABorrar.CantidadArticulo > 1)
            {
                detallesfacturaABorrar.CantidadArticulo--;
                //Se comprueba si son tallas o números para sumar una unidad ya que vuelve al stock.
                if (detallesfacturaABorrar.TallaONum.ToLower().Contains("talla"))
                {
                    detallesfacturaABorrar.ARTICULO.TALLASTEXTILES.SumaTalla(detallesfacturaABorrar.TallaONum);
                }
                else
                    detallesfacturaABorrar.ARTICULO.NUMEROSCALZADO.SumaNumero(detallesfacturaABorrar.TallaONum);
            }
            //Si sólo queda una unidad se elimina el registro de los detalles de la factura y revierte los cambios en la base de datos.
            else
            {
                DetallesArticulosFactura.Remove(detallesfacturaABorrar);
                BbddService.RevertChanges();
            }
            //Vuelve a calcular el precio actual
            RecalcularPrecio();
        }


        //Muestra el diálogo para aplicar descuentos y devuelve el resultado de este.
        internal bool DiscountExecuted()
        {
            SeleccionDescuentoDialog seleccionDescuento = new SeleccionDescuentoDialog(FacturaFinal, DetallesArticulosFactura)
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                ShowInTaskbar = false
            };

            DescuentoAplicado = seleccionDescuento.ShowDialog() == true;

            return DescuentoAplicado;
        }

        //Determina si se puede ejecutar el comando para aplicar descuentos.
        internal bool DiscountCanExecute()
        {
            return FacturaFinal.ImporteTotalConIva > 0;
        }


        //Ejecuta el comando para limpiar la factura actual de detalles e inicializa una nueva. Deshace los cambios en la base de datos.
        internal void DeleteExecuted()
        {
            DetallesArticulosFactura = new ObservableCollection<detallesfactura>();
            FacturaFinal = new facturas();
            BbddService.RevertChanges();
        }

        internal bool DeleteCanExecute()
        {
            return DetallesArticulosFactura.Count > 0;
        }

        //Muestra el diálogo para terminar la venta y devuelve el resultado de este.
        internal bool SellExecuted()
        {

            TerminarVentaDialog terminarVenta = new TerminarVentaDialog(FacturaFinal, DetallesArticulosFactura)
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                ShowInTaskbar = false
            };

            //Al terminar una venta se prepara una factura nueva para realizar la siguiente.
            if (terminarVenta.ShowDialog() == true)
            {
                FacturaFinal = new facturas();
                DetallesArticulosFactura = new ObservableCollection<detallesfactura>();
                CantidadElementosFactura = 0;
                BbddService.SaveChanges();
                return true;
            }
            else
                return false;
        }

        internal bool SellCanExecute()
        {
            return FacturaFinal.ImporteTotalConIva > 0;
        }

        //Muestra el diálogo para seleccionar las tallas o números del artículo seleccionado. Añade el artículo a la cantidad de elementos de la factura y calcula el precio.
        internal void SeleccionarTallaVenta()
        {
            SelectorTallasDialog selectorTallas = new SelectorTallasDialog(ArticuloSeleccionado, DetallesArticulosFactura) { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen, ShowInTaskbar = false };

            if (selectorTallas.ShowDialog() == true)
            {
                CantidadElementosFactura++;
                SumarPrecio(ArticuloSeleccionado);
            }
        }

        //Recalcula el precio de la factura actual
        private void RecalcularPrecio()
        {
            DescuentoAplicado = false;
            FacturaFinal.ImporteTotalConIva = 0;
            foreach (detallesfactura item in DetallesArticulosFactura)
            {
                FacturaFinal.ImporteTotalConIva += (item.ARTICULO.PrecioUnitario * item.CantidadArticulo);
            }
        }

        //Suma el precio de los artículos en la factura actual, si no hay ninguno, el precio es 0
        private void SumarPrecio(articulos articulo)
        {
            if (DetallesArticulosFactura.Count > 0)
                FacturaFinal.ImporteTotalConIva += articulo.PrecioUnitario;
            else
                FacturaFinal.ImporteTotalConIva = 0;
        }

        //Aplica el filtro para mostrar los artículos según la categoría seleccionada
        internal void CargaArticulosCategoriaSeleccionada()
        {
            if (CategoriaSeleccionada != null)
            {
                ListaArticulosSeleccionados = BbddService.GetArticulosPorCategoria(CategoriaSeleccionada.IdCategoria);
                ListaArticulosSeleccionados.Filter += ListaArticulosSeleccionados_Filter;
            }
        }

        internal void FindExecuted() => ListaArticulosSeleccionados.View.Refresh();

        //Limpia los filtros. Muestra la tabla original.
        internal void CleanFiltersExecuted()
        {
            FiltroTextBox = null;
            ColorSeleccionado = null;
            FindExecuted();
        }

        internal bool FindCanExecute() => !(FiltroTextBox is null) || !(ColorSeleccionado is null);
    }
}
