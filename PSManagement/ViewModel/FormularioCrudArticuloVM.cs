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
    class FormularioCrudArticuloVM : INotifyPropertyChanged
    {
        public readonly ItemCRUDAction itemAction;

        public articulos ArticuloCrud { get; set; }

        public cantidadportallas Cantidadportallas { get; set; }
        public cantidadpornumeros Cantidadpornumeros { get; set; }


        public ObservableCollection<inventarios> ListaInventarios { get; set; }
        public ObservableCollection<categorias> ListaCategorias { get; set; }
        public ObservableCollection<colores> ListaColores { get; set; }

        public inventarios InventarioSeleccionado { get; set; }
        public categorias CategoriaSeleccionada { get; set; }
        public colores ColorSeleccionado { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public bool TextilOCalzado { get; set; }


        public FormularioCrudArticuloVM(ItemCRUDAction action, Object articuloCrud = null)
        {
            itemAction = action;

            ListaInventarios = BBDDService.GetInventarios();
            ListaCategorias = BBDDService.GetCategorias();
            ListaColores = BBDDService.GetColores();

            if (itemAction == ItemCRUDAction.Insert_Item)
            {
                ArticuloCrud = new articulos();
                TallasArticuloNuevo();

            }
            else
            {
                ArticuloCrud = (articulos)articuloCrud;
                Cantidadportallas = ArticuloCrud.CANTIDADTALLA;
                Cantidadpornumeros = ArticuloCrud.CANTIDADNUM;
            }
        }

        public void Save_Execute()
        {
            if (itemAction == ItemCRUDAction.Insert_Item)
            {
                BBDDService.AddArticulo(ArticuloCrud);
                RegistrarTallas();
            }
            else if (itemAction == ItemCRUDAction.Delete_Item)
            {
                BBDDService.DeleteArticulo(ArticuloCrud);

                if (Cantidadpornumeros != null)
                    BBDDService.DeleteCantidadPorNumeros(Cantidadpornumeros);
                else
                    BBDDService.DeleteCantidadPorTallas(Cantidadportallas);
            }
            else
                BBDDService.SaveChanges();
        }

        public void RegistrarTallas()
        {
            if (TextilOCalzado)
            {
                BBDDService.AddCantidadPorNumeros(Cantidadpornumeros);
            }
            else
            {
                BBDDService.AddCantidadPorTallas(Cantidadportallas);
            }
        }



        public bool MinimoAlcanzado(int cantidad)
        {
            return cantidad <= ArticuloCrud.StockMinimo;
        }

        public string EsTextilOCalzado()
        {

            if (Cantidadportallas != null && Cantidadpornumeros == null)
                return "Textil";
            else if (Cantidadportallas == null && Cantidadpornumeros != null)
                return "Calzado";
            else
                return "Nuevo";
        }

        public void TallasArticuloNuevo()
        {
            TextilOCalzado = false;
            Cantidadpornumeros = null;
            Cantidadportallas = new cantidadportallas() { ARTICULO = ArticuloCrud };
        }

        public void NumerosArticuloNuevo()
        {
            TextilOCalzado = true;
            Cantidadportallas = null;
            Cantidadpornumeros = new cantidadpornumeros() { ARTICULO = ArticuloCrud };
        }
    }
}