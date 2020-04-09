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
        public categorias CategoriaSeleccionada { get; set; }
        public colores ColorSeleccionado { get; set; }

        public PanelInventariosVM()
        {
            ListaArticulos = new CollectionViewSource
            {
                Source = BBDDService.GetArticulos()
            };

            ListaInventarios = BBDDService.GetInventarios();
            ListaCategorias = BBDDService.GetCategorias();
            ListaColores = BBDDService.GetColores();
        }

        public int InsertarInventario(string nombreInventario)
        {
            inventarios nuevoInventario = new inventarios()
            {
                Descripcion = nombreInventario,
                FechaCreacion = DateTime.Now
            };

            return BBDDService.AddInventario(nuevoInventario);
        }

        public int InsertarCategoria(string nombreCategoria)
        {
            categorias nuevaCategoria = new categorias()
            {
                Categoria = nombreCategoria
            };

            return BBDDService.AddCategoria(nuevaCategoria);
        }

        public int InsertarColor(string nombreColor)
        {
            colores nuevoColor = new colores()
            {
                Color = nombreColor
            };

            return BBDDService.AddColor(nuevoColor);
        }

        public int SaveChanges()
        {
            return BBDDService.SaveChanges();
        }

        public int DeleteInventario()
        {
            return BBDDService.DeleteInventario(InventarioSeleccionado);
        }

        public int DeleteCategoria()
        {
            return BBDDService.DeleteCategoria(CategoriaSeleccionada);
        }

        public int DeleteColor()
        {
            return BBDDService.DeleteColor(ColorSeleccionado);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
