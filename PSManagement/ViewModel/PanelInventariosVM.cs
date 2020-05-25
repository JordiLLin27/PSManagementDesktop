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
            ListaArticulos = new CollectionViewSource { Source = BbddService.GetArticulos(), IsLiveFilteringRequested = true };
            ListaInventarios = BbddService.GetInventarios();
            ListaCategorias = BbddService.GetCategorias();
            ListaColores = BbddService.GetColores();

            ListaArticulos.Filter += FiltroTablaArticulos;
        }

        public int InsertarInventario(string nombreInventario)
        {
            inventarios nuevoInventario = new inventarios()
            {
                Descripcion = nombreInventario,
                FechaCreacion = DateTime.Now
            };

            return BbddService.AddInventario(nuevoInventario);
        }

        public int InsertarCategoria(string nombreCategoria)
        {
            categorias nuevaCategoria = new categorias()
            {
                Categoria = nombreCategoria
            };

            return BbddService.AddCategoria(nuevaCategoria);
        }

        /// <summary>
        /// Añade un color a la base de datos
        /// </summary>
        /// <param name="nombreColor"></param>
        /// <returns>Devuelve el número de cambios realizados en la base de datos.</returns>
        public int InsertarColor(string nombreColor)
        {
            colores nuevoColor = new colores()
            {
                Color = nombreColor
            };

            return BbddService.AddColor(nuevoColor);
        }

        /// <summary>
        /// Guarda los cambios realizados en la base de datos
        /// </summary>
        /// <returns>Devuelve el número de cambios realizados en la base de datos.</returns>
        public int SaveChanges()
        {
            return BbddService.SaveChanges();
        }

        /// <summary>
        /// Borra el inventario seleccionado en el GroupBox de mantenimiento de invetarios
        /// </summary>
        /// <returns>Devuelve el número de cambios realizados en la base de datos.</returns>
        public int DeleteInventario()
        {
            return BbddService.DeleteInventario(InventarioSeleccionado);
        }

        /// <summary>
        /// Borra la categoría seleccionado en el GroupBox de mantenimiento de categorías
        /// </summary>
        /// <returns>Devuelve el número de cambios realizados en la base de datos.</returns>
        public int DeleteCategoria()
        {
            return BbddService.DeleteCategoria(CategoriaSeleccionada);
        }

        /// <summary>
        /// Borra el color seleccionado en el GroupBox de mantenimiento de colores
        /// </summary>
        /// <returns>Devuelve el número de cambios realizados en la base de datos.</returns>
        public int DeleteColor()
        {
            return BbddService.DeleteColor(ColorSeleccionado);
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

            //Se aceptan todos los artículos si no hay filtros seleccionados o si se limpian. 0000
            if (string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado == null && FiltroCategoriaSeleccionada == null && FiltroColorSeleccionado == null)
            {
                e.Accepted = true;
            }
            else
            {
                //Si sólo el nombre o código coinciden: 0001
                if (!string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado == null && FiltroCategoriaSeleccionada == null && FiltroColorSeleccionado == null)
                {
                    if (CoincideNombreOCodigo(item, FiltroArticuloPorNombreOModelo))
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                }
                //Si coincide sólo el inventario: 0010
                else if (string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado != null && FiltroCategoriaSeleccionada == null && FiltroColorSeleccionado == null)
                {
                    if (CoincideInventario(item, FiltroInventarioSeleccionado))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si coinciden nombre/código e inventario: 0011
                else if (!string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado != null && FiltroCategoriaSeleccionada == null && FiltroColorSeleccionado == null)
                {
                    if (CoincideNombreOCodigo(item, FiltroArticuloPorNombreOModelo) && CoincideInventario(item, FiltroInventarioSeleccionado))
                    {
                        e.Accepted = true;
                    }
                    else
                        e.Accepted = false;
                }
                //Si coincide sólo la categoría: 0100
                else if (string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado == null && FiltroCategoriaSeleccionada != null && FiltroColorSeleccionado == null)
                {
                    if (CoincideCategoria(item, FiltroCategoriaSeleccionada))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si coinciden categoría y nombre/modelo: 0101
                else if (!string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado == null && FiltroCategoriaSeleccionada != null && FiltroColorSeleccionado == null)
                {
                    if (CoincideCategoria(item, FiltroCategoriaSeleccionada) && CoincideInventario(item, FiltroInventarioSeleccionado))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si coinciden inventario y categoría: 0110
                else if (string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado != null && FiltroCategoriaSeleccionada != null && FiltroColorSeleccionado == null)
                {
                    if (CoincideInventario(item, FiltroInventarioSeleccionado) && CoincideCategoria(item, FiltroCategoriaSeleccionada))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si coinciden inventario, categoría y nombre: 0111
                else if (!string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado != null && FiltroCategoriaSeleccionada != null && FiltroColorSeleccionado == null)
                {
                    if (CoincideNombreOCodigo(item, FiltroArticuloPorNombreOModelo) && CoincideInventario(item, FiltroInventarioSeleccionado) && CoincideCategoria(item, FiltroCategoriaSeleccionada))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si sólo coincide el color: 1000
                else if (string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado == null && FiltroCategoriaSeleccionada == null && FiltroColorSeleccionado != null)
                {
                    if (CoincideColor(item, FiltroColorSeleccionado))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si coinciden color y nombre/codigo: 1001
                else if (!string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado == null && FiltroCategoriaSeleccionada == null && FiltroColorSeleccionado != null)
                {
                    if (CoincideNombreOCodigo(item, FiltroArticuloPorNombreOModelo) && CoincideColor(item, FiltroColorSeleccionado))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si coinciden color e inventario: 1010
                else if (string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado != null && FiltroCategoriaSeleccionada == null && FiltroColorSeleccionado != null)
                {
                    if (CoincideColor(item, FiltroColorSeleccionado) && CoincideInventario(item, FiltroInventarioSeleccionado))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si coinciden color, inventario y nombre/código: 1011
                else if (!string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado != null && FiltroCategoriaSeleccionada == null && FiltroColorSeleccionado != null)
                {
                    if (CoincideColor(item, FiltroColorSeleccionado) && CoincideInventario(item, FiltroInventarioSeleccionado) && CoincideNombreOCodigo(item, FiltroArticuloPorNombreOModelo))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si coinciden color y categoría: 1100
                else if (string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado == null && FiltroCategoriaSeleccionada != null && FiltroColorSeleccionado != null)
                {
                    if (CoincideColor(item, FiltroColorSeleccionado) && CoincideCategoria(item, FiltroCategoriaSeleccionada))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si coinciden color, categoría y nombre/código: 1101
                else if (!string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado == null && FiltroCategoriaSeleccionada != null && FiltroColorSeleccionado != null)
                {
                    if (CoincideColor(item, FiltroColorSeleccionado) && CoincideCategoria(item, FiltroCategoriaSeleccionada) && CoincideNombreOCodigo(item, FiltroArticuloPorNombreOModelo))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si coinciden color, categoría e inventario: 1110
                else if (string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado != null && FiltroCategoriaSeleccionada != null && FiltroColorSeleccionado != null)
                {
                    if (CoincideColor(item, FiltroColorSeleccionado) && CoincideCategoria(item, FiltroCategoriaSeleccionada) && CoincideInventario(item, FiltroInventarioSeleccionado))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
                //Si coinciden todos los filtros: 1111
                else if (!string.IsNullOrEmpty(FiltroArticuloPorNombreOModelo) && FiltroInventarioSeleccionado != null && FiltroCategoriaSeleccionada != null && FiltroColorSeleccionado != null)
                {
                    if (CoincideNombreOCodigo(item, FiltroArticuloPorNombreOModelo) && CoincideInventario(item, FiltroInventarioSeleccionado) && CoincideCategoria(item, FiltroCategoriaSeleccionada) && CoincideColor(item, FiltroColorSeleccionado))
                        e.Accepted = true;
                    else
                        e.Accepted = false;
                }
            }
        }

        /// <summary>
        /// función que devuelve verdadero si la cadena de texto pasaada por parámetro coincide con el nombre o con el código del artículo, falso en otro caso.
        /// </summary>
        /// <param name="articulo"></param>
        /// <param name="nombreOCodigo"></param>
        /// <returns></returns>
        private bool CoincideNombreOCodigo(articulos articulo, string nombreOCodigo)
        {
            return (articulo.Nombre.ToLower().Contains(nombreOCodigo.Trim().ToLower()) || articulo.CodArticulo.ToLower().Contains(nombreOCodigo.Trim().ToLower()));
        }


        /// <summary>
        /// Función que devuelve verdadero si el inventario pasado por parámetro coincide con el del artículo introducido por parámetro.
        /// </summary>
        /// <param name="articulo"></param>
        /// <param name="inventario"></param>
        /// <returns></returns>
        private bool CoincideInventario(articulos articulo, inventarios inventario)
        {
            return articulo.INVENTARIO.Equals(inventario);
        }

        /// <summary>
        /// Función que devuelve verdadero si la categoría pasada por parámetro coincide con la del artículo introducido por parámetro.
        /// </summary>
        /// <param name="articulo"></param>
        /// <param name="categoria"></param>
        /// <returns></returns>
        private bool CoincideCategoria(articulos articulo, categorias categoria)
        {
            return articulo.CATEGORIA.Equals(categoria);
        }


        /// <summary>
        /// Función que devuelve verdadero si el color pasado por parámetro coincide con el del artículo introducido por parámetro.
        /// </summary>
        /// <param name="articulo"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool CoincideColor(articulos articulo, colores color)
        {
            return articulo.COLOR.Equals(color);
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
