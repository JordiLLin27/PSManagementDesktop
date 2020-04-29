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
using System.Windows.Controls;

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

        public TextBox TextBoxSeleccionadoActual { get; set; }

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
                NormalizarArticulo();
                BBDDService.AddArticulo(ArticuloCrud);
                RegistrarTallas();
            }
            else if (itemAction == ItemCRUDAction.Delete_Item)
            {
                BlobStorage.EliminarImagen(ArticuloCrud.UrlImagen);
                BBDDService.DeleteArticulo(ArticuloCrud);
            }
            else
            {
                NormalizarArticulo();
                ActualizarInfo();
            }
        }

        public void NormalizarArticulo()
        {
            ArticuloCrud.CodArticulo = ArticuloCrud.CodArticulo.ToUpper();

            if (string.IsNullOrEmpty(ArticuloCrud.UrlImagen))
            {
                ArticuloCrud.UrlImagen = @"\Assets\no-image-available.png";
            }
        }

        public void RegistrarTallas()
        {
            if (TextilOCalzado)
            {
                NumerosCalzado.TotalCantidadArticulo = NumerosCalzado.GetTotalItems();
                BBDDService.AddNumerosCalzado(NumerosCalzado);
            }
            else
            {
                TallasTextiles.TotalCantidadArticulo = TallasTextiles.GetTotalItems();
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

        internal void CloseExecuted()
        {
            BBDDService.RevertChanges();
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
        }

        internal bool PidePin()
        {
            PinDialog pinDialog = new PinDialog(PinConfig.Insert_Pin) { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen };

            return pinDialog.ShowDialog() == true ? true : false;
        }

        internal ItemCRUDAction GetAction() { return itemAction; }

        internal bool OpenNumPadExecuted()
        {
            PanelNumericoDialog numPad;
            if (TallasTextiles != null && NumerosCalzado == null)
            {
                numPad = new PanelNumericoDialog(TallasTextiles, TextBoxSeleccionadoActual.Tag.ToString()) { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen };
                return (numPad.ShowDialog() == true ? true : false);
            }
            else if (TallasTextiles == null && NumerosCalzado != null)
            {
                numPad = new PanelNumericoDialog(NumerosCalzado, TextBoxSeleccionadoActual.Tag.ToString()) { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen };
                return (numPad.ShowDialog() == true ? true : false);
            }
            else
                return false;
        }

        internal void SetTextBoxActual(TextBox sender)
        {
            TextBoxSeleccionadoActual = sender;
        }

        internal bool OpenNumPadCanExecute()
        {
            return TextBoxSeleccionadoActual != null;
        }
    }
}