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
        public ObservableCollection<tallastextiles> ListaTallasTextiles { get; set; }
        public ObservableCollection<numeroscalzado> ListaNumerosCalzado { get; set; }
        public ObservableCollection<detallesfactura> DetallesArticulosFactura { get; set; }

        public articulos ArticuloSeleccionado { get; set; }
        public categorias CategoriaSeleccionada { get; set; }

        public detallesfactura DetallesDeUnArticuloEnFactura { get; set; }
        public facturas FacturaFinal { get; set; }

        public double PrecioTotal { get; set; }

        public string FiltroTextBox { get; set; }


        public PanelVentasVM()
        {
            ListaCategorias = BBDDService.GetCategorias();
            ListaTallasTextiles = BBDDService.GetTallasTextiles();
            ListaNumerosCalzado = BBDDService.GetNumerosCalzado();           

            DetallesArticulosFactura = new ObservableCollection<detallesfactura>();
            PrecioTotal = 0.00;

        }

        private void ListaArticulosSeleccionados_Filter(object sender, FilterEventArgs e)
        {
            articulos item = (articulos)e.Item;

            if (string.IsNullOrEmpty(FiltroTextBox))
                e.Accepted = true;
            else
            {
                if (item.Nombre.ToLower().Contains(FiltroTextBox.ToLower()) || item.CodArticulo.ToLower().Contains(FiltroTextBox.ToLower()))
                {
                    e.Accepted = true;
                }
                else
                    e.Accepted = false;
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

        internal void CleanFiltersExecuted() => FiltroTextBox = null;

        internal bool FindCanExecute() => !(FiltroTextBox is null);
    }
}
