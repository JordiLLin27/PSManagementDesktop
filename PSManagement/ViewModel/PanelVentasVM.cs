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

        private void ListaArticulosSeleccionados_Filter(object sender, FilterEventArgs e)
        {
            articulos item = (articulos)e.Item;

            if (string.IsNullOrEmpty(FiltroTextBox) && ColorSeleccionado == null)
                e.Accepted = true;
            else
            {
                if (string.IsNullOrEmpty(FiltroTextBox) && ColorSeleccionado != null)
                {
                    if (item.COLOR == ColorSeleccionado)
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                else if (!string.IsNullOrEmpty(FiltroTextBox) && ColorSeleccionado == null)
                {
                    if (item.Nombre.ToLower().Contains(FiltroTextBox.ToLower()) || item.CodArticulo.ToLower().Contains(FiltroTextBox.ToLower()))
                    {
                        e.Accepted = true;

                    }
                    else
                        e.Accepted = false;
                }
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

        internal void BorrarItems(detallesfactura detallesfacturaABorrar)
        {
            if (detallesfacturaABorrar.CantidadArticulo > 1)
            {
                detallesfacturaABorrar.CantidadArticulo--;
                if (detallesfacturaABorrar.TallaONum.ToLower().Contains("talla"))
                {
                    detallesfacturaABorrar.ARTICULO.TALLASTEXTILES.SumaTalla(detallesfacturaABorrar.TallaONum);
                }
                else
                    detallesfacturaABorrar.ARTICULO.NUMEROSCALZADO.SumaNumero(detallesfacturaABorrar.TallaONum);
            }
            else
            {
                DetallesArticulosFactura.Remove(detallesfacturaABorrar);
                BbddService.RevertChanges();
            }
            RecalcularPrecio();
        }

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

        internal bool DiscountCanExecute()
        {
            return FacturaFinal.ImporteTotalConIva > 0;
        }

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

        internal bool SellExecuted()
        {

            TerminarVentaDialog terminarVenta = new TerminarVentaDialog(FacturaFinal, DetallesArticulosFactura)
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                ShowInTaskbar = false
            };

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

        internal void SeleccionarTallaVenta()
        {
            SelectorTallasDialog selectorTallas = new SelectorTallasDialog(ArticuloSeleccionado, DetallesArticulosFactura) { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen, ShowInTaskbar = false };

            if (selectorTallas.ShowDialog() == true)
            {
                CantidadElementosFactura++;
                SumarPrecio(ArticuloSeleccionado);
            }
        }

        private void RecalcularPrecio()
        {
            DescuentoAplicado = false;
            FacturaFinal.ImporteTotalConIva = 0;
            foreach (detallesfactura item in DetallesArticulosFactura)
            {
                FacturaFinal.ImporteTotalConIva += (item.ARTICULO.PrecioUnitario * item.CantidadArticulo);
            }
        }

        private void SumarPrecio(articulos articulo)
        {
            if (DetallesArticulosFactura.Count > 0)
                FacturaFinal.ImporteTotalConIva += articulo.PrecioUnitario;
            else
                FacturaFinal.ImporteTotalConIva = 0;
        }

        internal void CargaArticulosCategoriaSeleccionada()
        {
            if (CategoriaSeleccionada != null)
            {
                ListaArticulosSeleccionados = BbddService.GetArticulosPorCategoria(CategoriaSeleccionada.IdCategoria);
                ListaArticulosSeleccionados.Filter += ListaArticulosSeleccionados_Filter;
            }
        }

        internal void FindExecuted() => ListaArticulosSeleccionados.View.Refresh();

        internal void CleanFiltersExecuted()
        {
            FiltroTextBox = null;
            ColorSeleccionado = null;
            FindExecuted();
        }

        internal bool FindCanExecute() => !(FiltroTextBox is null) || !(ColorSeleccionado is null);
    }
}
