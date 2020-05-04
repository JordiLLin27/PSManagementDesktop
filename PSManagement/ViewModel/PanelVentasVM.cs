using PSManagement.Dialogs;
using PSManagement.Model;
using PSManagement.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public double PrecioTotal { get; set; }

        public string FiltroTextBox { get; set; }


        public PanelVentasVM()
        {
            ListaCategorias = BBDDService.GetCategorias();
            ListaTallasTextiles = BBDDService.GetTallasTextiles();
            ListaNumerosCalzado = BBDDService.GetNumerosCalzado();
            ListaColores = BBDDService.GetColores();

            FacturaFinal = new facturas() { IdFactura = BBDDService.GetIdFactura() + 1 };
            DetallesArticulosFactura = new ObservableCollection<detallesfactura>();

            PrecioTotal = 0.00;

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

        internal void DeleteExecuted()
        {
            DetallesArticulosFactura.Clear();
            PrecioTotal = 0.00;
        }

        internal bool DeleteCanExecute()
        {
            return DetallesArticulosFactura.Count > 0;
        }

        internal void SellExecuted()
        {

        }

        internal bool SellCanExecute()
        {
            return PrecioTotal > 0;
        }

        internal void SeleccionarTallaVenta()
        {
            SelectorTallasDialog selectorTallas = new SelectorTallasDialog(ArticuloSeleccionado, DetallesArticulosFactura) { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen, ShowInTaskbar = false };

            selectorTallas.ShowDialog();

            CalculaPrecio();
        }

        private void CalculaPrecio()
        {
            PrecioTotal = 0d;
            foreach (detallesfactura item in DetallesArticulosFactura)
            {
                PrecioTotal += item.ARTICULO.PrecioUnitario;
            }
        }

        internal void CargaArticulosCategoriaSeleccionada()
        {
            if (CategoriaSeleccionada != null)
            {
                ListaArticulosSeleccionados = BBDDService.GetArticulosPorCategoria(CategoriaSeleccionada.IdCategoria);
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
