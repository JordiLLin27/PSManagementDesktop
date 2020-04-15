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

        public tallastextiles tallasTextiles { get; set; }
        public numeroscalzado numerosCalzado { get; set; }


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
                tallasTextiles = ArticuloCrud.TALLASTEXTILES;
                numerosCalzado = ArticuloCrud.NUMEROSCALZADO;
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

                if (numerosCalzado != null)
                    BBDDService.DeleteNumerosCalzado(numerosCalzado);
                else
                    BBDDService.DeleteTallasTextiles(tallasTextiles);
            }
            else
                BBDDService.SaveChanges();
        }

        public void RegistrarTallas()
        {
            if (TextilOCalzado)
            {
                BBDDService.AddNumerosCalzado(numerosCalzado);
            }
            else
            {
                BBDDService.AddTallasTextiles(tallasTextiles);
            }
        }



        public bool MinimoAlcanzado(int cantidad)
        {
            return cantidad <= ArticuloCrud.StockMinimo;
        }

        public string EsTextilOCalzado()
        {

            if (tallasTextiles != null && numerosCalzado == null)
                return "Textil";
            else if (tallasTextiles == null && numerosCalzado != null)
                return "Calzado";
            else
                return "Nuevo";
        }

        public void TallasArticuloNuevo()
        {
            TextilOCalzado = false;
            numerosCalzado = null;
            tallasTextiles = new tallastextiles() { ARTICULO = ArticuloCrud, CodArticulo = ArticuloCrud.CodArticulo };
        }

        public void NumerosArticuloNuevo()
        {
            TextilOCalzado = true;
            tallasTextiles = null;
            numerosCalzado = new numeroscalzado() { ARTICULO = ArticuloCrud, CodArticulo = ArticuloCrud.CodArticulo };
        }
    }
}