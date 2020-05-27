using Microsoft.Win32;
using PSManagement.ViewModel;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using System.Windows.Media;


namespace PSManagement.View
{
    /// <summary>
    /// Lógica de interacción para FormularioNuevoArticulo.xaml
    /// </summary>
    public partial class FormularioCrudArticuloDialogView : Window
    {
        string mensajeOpcion;
        public FormularioCrudArticuloDialogView(ItemCRUDAction action, Object articulo = null)
        {
            this.DataContext = new FormularioCrudArticuloVM(action, articulo);
            InitializeComponent();

            //Según la acción se cambian textos y títulos descriptivos y si la acción es de eliminar los campos se desactivan para que no se puedan modificar.
            if (action == ItemCRUDAction.Insert_Item)
            {
                VentanaCrudItemsGroupBox.Header = "Añadir artículo";
                AceptarButton.Content = "Añadir artículo";
                mensajeOpcion = "añadir";
            }
            else if (action == ItemCRUDAction.Update_Item)
            {
                VentanaCrudItemsGroupBox.Header = "Modificar artículo";
                CompruebaExpanders();
                AceptarButton.Content = "Modificar artículo";
                mensajeOpcion = "modificar";
            }
            else if (action == ItemCRUDAction.Delete_Item)
            {
                VentanaCrudItemsGroupBox.Header = "Eliminar artículo";
                AceptarButton.Content = "Eliminar artículo";
                mensajeOpcion = "eliminar";
                CompruebaExpanders();
                SoloLecturaDeleteItem();
            }
        }

        //Comando para aplicar los cambios y guardarlos en la base de datos
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                //Según la acción a realizar el mensaje se mostrará acorde a esta.
                if (MessageBox.Show("¿Seguro que quieres " + mensajeOpcion + " este ítem?", "Información", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    //Para las acciones de modificar y eliminar se pide el pin por seguridad
                    if ((DataContext as FormularioCrudArticuloVM).GetAction() != ItemCRUDAction.Insert_Item)
                    {
                        if ((DataContext as FormularioCrudArticuloVM).PidePin())
                        {
                            (this.DataContext as FormularioCrudArticuloVM).Save_Execute();
                            MessageBox.Show("Datos actualizados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        (this.DataContext as FormularioCrudArticuloVM).Save_Execute();
                        MessageBox.Show("Datos actualizados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Comando para cerrar el formulario y revertir los cambios no guardados en la base de datos.
        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as FormularioCrudArticuloVM).CloseExecuted();
            this.Close();
        }

        //Manejador de evento en los cambios texto en los textbox de las tablas de tallas y números para establecer el fondo de un color según si han alcanzado el stock mínimo o no.
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                try
                {
                    int cantidadActual = int.Parse(((TextBox)sender).Text);

                    if ((DataContext as FormularioCrudArticuloVM).MinimoAlcanzado(cantidadActual))
                        ((TextBox)sender).Background = Brushes.DarkRed;
                    else
                        ((TextBox)sender).Background = Brushes.White;
                }
                catch (Exception)
                {
                    e.Handled = true;
                }

            }
        }

        //Función que comprueba si el artículo contiene tallas o números para mostrar un expander para introducir tallas o un expander para introducir números.
        private void CompruebaExpanders()
        {
            if ((DataContext as FormularioCrudArticuloVM).EsTextilOCalzado().Contains("Textil"))
            {
                HideExpanderCalzado();
                ShowExpanderTextil();
            }
            else if ((DataContext as FormularioCrudArticuloVM).EsTextilOCalzado().Contains("Calzado"))
            {
                HideExpanderTextil();
                ShowExpanderCalzado();
            }
        }

        //Colapsa el expander de números de calzado
        private void HideExpanderCalzado()
        {
            TablaIntroducirTallasCalzadoExpander.IsEnabled = false;
            TablaIntroducirTallasCalzadoExpander.Visibility = Visibility.Collapsed;
        }

        //Colapsa el expander de tallas textiles
        private void HideExpanderTextil()
        {
            TablaIntroducirTallasTextilExpander.IsEnabled = false;
            TablaIntroducirTallasTextilExpander.Visibility = Visibility.Collapsed;
        }

        //Hace visible el expander de números de calzado
        private void ShowExpanderCalzado()
        {
            TablaIntroducirTallasCalzadoExpander.IsEnabled = true;
            TablaIntroducirTallasCalzadoExpander.Visibility = Visibility.Visible;
        }

        //Hace visible el expander de tallas textiles
        private void ShowExpanderTextil()
        {
            TablaIntroducirTallasTextilExpander.IsEnabled = true;
            TablaIntroducirTallasTextilExpander.Visibility = Visibility.Visible;
        }

        //Si el togglebutton se activa hace que el artículo contenga números de calzado  y se muestra el expander para introducir el stock si se desea.
        private void SeleccionTallasToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            HideExpanderTextil();
            ShowExpanderCalzado();
            (DataContext as FormularioCrudArticuloVM).NumerosArticuloNuevo();
        }

        //Si el togglebutton se desactiva hace que el artículo contenga tallas textiles y se muestra el expander para introducir el stock si se desea.
        private void SeleccionTallasToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            HideExpanderCalzado();
            ShowExpanderTextil();
            (DataContext as FormularioCrudArticuloVM).TallasArticuloNuevo();
        }

        //Desactivación de los controles para que nos e realicen cambios en ellos.
        private void SoloLecturaDeleteItem()
        {
            NombreArticuloTextBox.IsReadOnly = true;
            CodigoArticuloTextBox.IsReadOnly = true;
            PrecioUnitarioTextBox.IsReadOnly = true;
            StockMinimoSlider.IsEnabled = false;
            ListaCategoriasComboBox.IsEnabled = false;
            ListaInventariosComboBox.IsEnabled = false;
            ListaColoresComboBox.IsEnabled = false;
            VentaDisponibleCheckBox.IsEnabled = false;
            AbrirPanelNumericoButton.IsEnabled = false;
            SeleccionTallasToggleButton.IsEnabled = false;
            TablaTallasGrid.IsEnabled = false;
            TablaNumerosGrid.IsEnabled = false;
        }

        //Se utiliza para establecer a qué talla o número asignarle la cantidad escrita. Se controla que no sea el textbox con el tag 'Precio'
        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if ((DataContext as FormularioCrudArticuloVM).itemAction != ItemCRUDAction.Delete_Item)
            {
                ((TextBox)sender).Text = "";
                if (((TextBox)sender).Tag.ToString() != "Precio")
                    (DataContext as FormularioCrudArticuloVM).SetTextBoxActual((TextBox)sender);
            }
        }

        //Se utiliza siempre que sea el textbox con tag 'Precio' para reemplazar el caracter ',' por '.' si lo contiene.
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(((TextBox)sender).Text) && ((TextBox)sender).Tag.ToString() == "Precio" && ((TextBox)sender).Text.Contains(","))
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Replace(',', '.');
            }
        }

        //Comando para abrir un diálogo para seleccionar un archivo de imagen para el artículo
        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Todos los archivos|*.*|Archivos de imagen JPG/JPEG|*.jpg|Archivos de imagen BMP|*.bmp|Archivos de imagen PNG|*.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == true)
            {
                (DataContext as FormularioCrudArticuloVM).SeleccionarImagenArticulo(dialog.FileName);
            }
        }

        private void OpenCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as FormularioCrudArticuloVM).itemAction == ItemCRUDAction.Insert_Item || (DataContext as FormularioCrudArticuloVM).itemAction == ItemCRUDAction.Update_Item;
        }

        //Abre el panel numérico
        private void OpenNumPadCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as FormularioCrudArticuloVM).OpenNumPadExecuted();
        }

        private void OpenNumPadCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as FormularioCrudArticuloVM).OpenNumPadCanExecute();
        }

        //Controla que sólo se puedan pulsar las teclas numéricas, de coma o punto, borrar y tab. El resto no hace nada.
        private void PrecioUnitarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.D0 && e.Key != Key.NumPad0 &&
                e.Key != Key.D1 && e.Key != Key.NumPad1 &&
                e.Key != Key.D2 && e.Key != Key.NumPad2 &&
                e.Key != Key.D3 && e.Key != Key.NumPad3 &&
                e.Key != Key.D4 && e.Key != Key.NumPad4 &&
                e.Key != Key.D5 && e.Key != Key.NumPad5 &&
                e.Key != Key.D6 && e.Key != Key.NumPad6 &&
                e.Key != Key.D7 && e.Key != Key.NumPad7 &&
                e.Key != Key.D8 && e.Key != Key.NumPad8 &&
                e.Key != Key.D9 && e.Key != Key.NumPad9 &&
                e.Key != Key.OemComma && e.Key != Key.OemPeriod &&
                e.Key != Key.Delete && e.Key != Key.Decimal && e.Key != Key.Tab)
            {
                e.Handled = true;
            }
        }
    }
}
