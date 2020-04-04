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
    class PanelInventariosVM : INotifyPropertyChanged
    {

        public CollectionViewSource ListaArticulos { get; set; }
        public ObservableCollection<inventarios> ListaInventarios { get; set; }
        public ObservableCollection<categorias> ListaCategorias { get; set; }
        public ObservableCollection<colores> ListaColores { get; set; }

        public articulos ArticuloSeleccionado { get; set; }


        public inventarios InventarioSeleccionado { get; set; }
        public inventarios NuevoInventario { get; set; }

        public categorias CategoriaSeleccionada { get; set; }
        public categorias NuevaCategoria { get; set; }

        public colores NuevoColor { get; set; }
        public colores ColorSeleccionado { get; set; }

        public PanelInventariosVM()
        {
            ListaArticulos = new CollectionViewSource() { Source = BBDDService.GetArticulos() };
            ListaInventarios = BBDDService.GetInventarios();
            ListaCategorias = BBDDService.GetCategorias();
            ListaColores = BBDDService.GetColores();

            /*
            NuevoInventario = new inventarios();
            NuevaCategoria = new categorias();
            NuevoColor = new colores();
            */
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
