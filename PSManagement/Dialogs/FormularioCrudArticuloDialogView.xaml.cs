using Microsoft.Win32;
using PSManagement.ViewModel;
using System;

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

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Seguro que quieres " + mensajeOpcion + " este ítem?", "Información", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
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

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as FormularioCrudArticuloVM).CloseExecuted();
            this.Close();
        }

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

        private void HideExpanderCalzado()
        {
            TablaIntroducirTallasCalzadoExpander.IsEnabled = false;
            TablaIntroducirTallasCalzadoExpander.Visibility = Visibility.Collapsed;
        }
        private void HideExpanderTextil()
        {
            TablaIntroducirTallasTextilExpander.IsEnabled = false;
            TablaIntroducirTallasTextilExpander.Visibility = Visibility.Collapsed;
        }

        private void ShowExpanderCalzado()
        {
            TablaIntroducirTallasCalzadoExpander.IsEnabled = true;
            TablaIntroducirTallasCalzadoExpander.Visibility = Visibility.Visible;
        }
        private void ShowExpanderTextil()
        {
            TablaIntroducirTallasTextilExpander.IsEnabled = true;
            TablaIntroducirTallasTextilExpander.Visibility = Visibility.Visible;
        }

        private void SeleccionTallasToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            HideExpanderTextil();
            ShowExpanderCalzado();
            (DataContext as FormularioCrudArticuloVM).NumerosArticuloNuevo();
        }

        private void SeleccionTallasToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            HideExpanderCalzado();
            ShowExpanderTextil();
            (DataContext as FormularioCrudArticuloVM).TallasArticuloNuevo();
        }

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
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (((TextBox)sender).Tag.ToString() != "Precio")
                (DataContext as FormularioCrudArticuloVM).SetTextBoxActual((TextBox)sender);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(((TextBox)sender).Text) && ((TextBox)sender).Tag.ToString() == "Precio")
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Replace(',', '.');
            }
        }

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

        private void OpenNumPadCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as FormularioCrudArticuloVM).OpenNumPadExecuted();
        }

        private void OpenNumPadCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as FormularioCrudArticuloVM).OpenNumPadCanExecute();
        }
    }
}
