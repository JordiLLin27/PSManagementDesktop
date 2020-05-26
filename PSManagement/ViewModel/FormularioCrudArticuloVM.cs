using PSManagement.Dialogs;
using PSManagement.Model;
using PSManagement.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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


        //Constructor
        public FormularioCrudArticuloVM(ItemCRUDAction action, Object articuloCrud = null)
        {
            itemAction = action;

            ListaInventarios = BbddService.GetInventarios();
            ListaCategorias = BbddService.GetCategorias();
            ListaColores = BbddService.GetColores();


            //Si se va a insertar un nuevo artículo
            if (itemAction == ItemCRUDAction.Insert_Item)
            {
                //Se inicializa un artículo y por defecto tendrá tallas textiles
                ArticuloCrud = new articulos();
                TallasArticuloNuevo();
            }
            else
            {
                ArticuloCrud = (articulos)articuloCrud;
                //Se asignan las tallas y los números del artículo actual (aunque uno de los dos será null, se evaluará más tarde)
                TallasTextiles = ArticuloCrud.TALLASTEXTILES;
                NumerosCalzado = ArticuloCrud.NUMEROSCALZADO;
            }
        }

        //Ejecuta la acción a realizar en el mantenimiento de artículos
        public void Save_Execute()
        {
            if (itemAction == ItemCRUDAction.Insert_Item)
            {
                NormalizarArticulo();
                BbddService.AddArticulo(ArticuloCrud);
                RegistrarTallas();
            }
            else if (itemAction == ItemCRUDAction.Delete_Item)
            {
                //Al borrar un artículo también se borran las referencias a su imagen en el servicio BlobStorage de Azure
                BlobStorage.EliminarImagen(ArticuloCrud.UrlImagen);
                BbddService.DeleteArticulo(ArticuloCrud);
            }
            else
            {
                NormalizarArticulo();
                ActualizarInfo();
            }
        }

        //El modelo del artículo se convierte a mayúsculas y si no se le ha asignado ninguna imagen se le asigna una por defecto.
        public void NormalizarArticulo()
        {
            ArticuloCrud.CodArticulo = ArticuloCrud.CodArticulo.ToUpper();

            if (string.IsNullOrEmpty(ArticuloCrud.UrlImagen))
            {
                ArticuloCrud.UrlImagen = @"\Assets\no-image-available.png";
            }
        }

        //Añade las referencias a las tallas o a los números del artículo actual.
        public void RegistrarTallas()
        {
            if (TextilOCalzado)
            {
                NumerosCalzado.TotalCantidadArticulo = NumerosCalzado.GetTotalItems();
                BbddService.AddNumerosCalzado(NumerosCalzado);
            }
            else
            {
                TallasTextiles.TotalCantidadArticulo = TallasTextiles.GetTotalItems();
                BbddService.AddTallasTextiles(TallasTextiles);
            }
        }

        //Guarda la información en la base de datos.
        public void ActualizarInfo()
        {
            BbddService.SaveChanges();
        }

        //Comprueba si la cantidad indicada ha superado el mínimo de stock establecido en artículo actual.
        public bool MinimoAlcanzado(int cantidad)
        {
            return cantidad <= ArticuloCrud.StockMinimo;
        }

        //Al cerrar el diálogo sin guardar los cambios se dehacen los cambios en la base de datos.
        internal void CloseExecuted()
        {
            BbddService.RevertChanges();
        }

        /// <summary>
        /// Comprueba si el artículo actual contiene tallas textiles o números de calzado
        /// </summary>
        /// <returns>Cadena de texto indicanto si es un producto textil o de calzado, nuevo si se va a insertar un nuevo artículo</returns>
        public string EsTextilOCalzado()
        {

            if (TallasTextiles != null && NumerosCalzado == null)
                return "Textil";
            else if (TallasTextiles == null && NumerosCalzado != null)
                return "Calzado";
            else
                return "Nuevo";
        }

        //Inicializa las tallas textiles en el artículo a insertar y establece a null los números de calzado.
        public void TallasArticuloNuevo()
        {
            TextilOCalzado = false;
            NumerosCalzado = null;
            TallasTextiles = new tallastextiles() { ARTICULO = ArticuloCrud, CodArticulo = ArticuloCrud.CodArticulo };
        }

        //Inicializa los números de calzadoº en el artículo a insertar y establece a null las tallas textiles.
        public void NumerosArticuloNuevo()
        {
            TextilOCalzado = true;
            TallasTextiles = null;
            NumerosCalzado = new numeroscalzado() { ARTICULO = ArticuloCrud, CodArticulo = ArticuloCrud.CodArticulo };
        }

        /// <summary>
        /// Ejecuta el comando para guardar la imagen seleccionada en el servicio BlobStorage de Azure.
        /// </summary>
        /// <param name="rutaImagen"></param>
        public void SeleccionarImagenArticulo(string rutaImagen)
        {
            string[] rutaSplit = rutaImagen.Split('\\');
            string nombreImgURL = rutaSplit[rutaSplit.Length - 1];
            string blobStorageRuta = BlobStorage.GuardarImagen(rutaImagen, nombreImgURL);
            ArticuloCrud.UrlImagen = blobStorageRuta;
        }

        //Muestra el diálogo para introducir el pin de seguridad.
        internal bool PidePin()
        {
            PinDialog pinDialog = new PinDialog(PinConfig.Insert_Pin) { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen };

            return pinDialog.ShowDialog() == true;
        }

        //Devuelve la acción que se está realizando en el formulario.
        internal ItemCRUDAction GetAction() { return itemAction; }

        //Muestra un panel numérico para introducir unidades de stock.
        internal bool OpenNumPadExecuted()
        {
            PanelNumericoDialog numPad;
            if (TallasTextiles != null && NumerosCalzado == null)
            {
                numPad = new PanelNumericoDialog(TallasTextiles, TextBoxSeleccionadoActual.Tag.ToString()) { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen };
                return (numPad.ShowDialog() == true);
            }
            else if (TallasTextiles == null && NumerosCalzado != null)
            {
                numPad = new PanelNumericoDialog(NumerosCalzado, TextBoxSeleccionadoActual.Tag.ToString()) { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen };
                return (numPad.ShowDialog() == true);
            }
            else
                return false;
        }

        //Establece qué textbox está seleccionado para introducir unidades de stock mediante panel numérico.
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