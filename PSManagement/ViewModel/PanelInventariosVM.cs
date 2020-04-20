using PSManagement.Model;
using PSManagement.Service;
using PSManagement.View;
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

        public inventarios FiltroInventarioSeleccionado { get; set; }
        public categorias FiltroCategoriaSeleccionada { get; set; }
        public colores FiltroColorSeleccionado { get; set; }
        public string FiltroArticuloPorNombreOModelo { get; set; }

        public PanelInventariosVM()
        {
            ListaArticulos = new CollectionViewSource { Source = BBDDService.GetArticulos(), IsLiveFilteringRequested = true };
            ListaInventarios = BBDDService.GetInventarios();
            ListaCategorias = BBDDService.GetCategorias();
            ListaColores = BBDDService.GetColores();

            ListaArticulos.Filter += FiltroTablaArticulos;
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

        public void FiltroArticulos()
        {
            ListaArticulos.View.Refresh();
        }

        //Manejador del evento Filter
        private void FiltroTablaArticulos(object sender, FilterEventArgs e)
        {
            articulos item = (articulos)e.Item;

            e.Accepted = false;

            if (string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado == null && FiltroCategoriaSeleccionada == null && FiltroColorSeleccionado == null)
            {
                e.Accepted = true;
            }
            else
            {
                if (!(string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo)) && (item.Nombre.ToLower().Contains(FiltroArticuloPorNombreOModelo.Trim().ToLower()) || item.CodArticulo.ToLower().Contains(FiltroArticuloPorNombreOModelo.Trim().ToLower())))
                    e.Accepted = true;
                else if (item.INVENTARIO == FiltroInventarioSeleccionado)
                    e.Accepted = true;
                else if (item.CATEGORIA == FiltroCategoriaSeleccionada)
                    e.Accepted = true;
                else if (item.COLOR == FiltroColorSeleccionado)
                    e.Accepted = true;
            }
        }

        public bool Filter_CanExecute()
        {
            return (FiltroInventarioSeleccionado != null || FiltroCategoriaSeleccionada != null || FiltroColorSeleccionado != null || !string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo));
        }

        public void CleanFilters_Execute()
        {
            FiltroInventarioSeleccionado = null;
            FiltroCategoriaSeleccionada = null;
            FiltroColorSeleccionado = null;
            FiltroArticuloPorNombreOModelo = "";
        }

        public bool CantidadMinimaAlcanzada(int cantidadActual)
        {
            return cantidadActual <= ArticuloSeleccionado.StockMinimo;
        }

        public FormularioCrudArticuloDialogView ItemCrud_Execute(ItemCRUDAction action)
        {
            FormularioCrudArticuloDialogView formularioCrudArticulo;

            if (action == ItemCRUDAction.Insert_Item)
                formularioCrudArticulo = new FormularioCrudArticuloDialogView(action, new articulos());
            else
                formularioCrudArticulo = new FormularioCrudArticuloDialogView(action, ArticuloSeleccionado);

            //Pedir pin

            return formularioCrudArticulo;
        }

        public bool ItemCrudCommandBinding_CanExecute(string nombreControl)
        {
            switch (nombreControl)
            {
                case "NuevoArticuloButton":
                    return true;


                case "UpdateArticuloButton":
                    return (ArticuloSeleccionado != null);


                case "DeleteArticuloButton":
                    return (ArticuloSeleccionado != null);

                default:
                    return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
