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

        public tallastextiles TallasTextiles { get; set; }
        public numeroscalzado NumerosCalzado { get; set; }


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
                TallasTextiles = ArticuloCrud.TALLASTEXTILES;
                NumerosCalzado = ArticuloCrud.NUMEROSCALZADO;
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
            }
            else
                ActualizarInfo();
        }

        public void RegistrarTallas()
        {
            if (TextilOCalzado)
            {
                BBDDService.AddNumerosCalzado(NumerosCalzado);
            }
            else
            {
                BBDDService.AddTallasTextiles(TallasTextiles);
            }
        }

        public void ActualizarInfo()
        {
            BBDDService.SaveChanges();
        }

        public bool MinimoAlcanzado(int cantidad)
        {
            return cantidad <= ArticuloCrud.StockMinimo;
        }

        public string EsTextilOCalzado()
        {

            if (TallasTextiles != null && NumerosCalzado == null)
                return "Textil";
            else if (TallasTextiles == null && NumerosCalzado != null)
                return "Calzado";
            else
                return "Nuevo";
        }

        public void TallasArticuloNuevo()
        {
            TextilOCalzado = false;
            NumerosCalzado = null;
            TallasTextiles = new tallastextiles() { ARTICULO = ArticuloCrud, CodArticulo = ArticuloCrud.CodArticulo };
        }

        public void NumerosArticuloNuevo()
        {
            TextilOCalzado = true;
            TallasTextiles = null;
            NumerosCalzado = new numeroscalzado() { ARTICULO = ArticuloCrud, CodArticulo = ArticuloCrud.CodArticulo };
        }

        public void SeleccionarImagenArticulo(string rutaImagen)
        {
            
            string[] rutaSplit = rutaImagen.Split('\\');
            string nombreImgURL = rutaSplit[rutaSplit.Length - 1];
            string blobStorageRuta = BlobStorage.GuardarImagen(rutaImagen, nombreImgURL);
            ArticuloCrud.UrlImagen = blobStorageRuta;
            
            //ArticuloCrud.UrlImagen = rutaImagen;
        }
    }
}