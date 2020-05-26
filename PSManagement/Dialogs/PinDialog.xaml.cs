using PSManagement.Commands;
using PSManagement.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PSManagement.Dialogs
{
    /// <summary>
    /// Lógica de interacción para PinDialog.xaml
    /// </summary>
    public partial class PinDialog : Window
    {
        private readonly PinConfig pinActionConfig;

        public PinDialog(PinConfig action)
        {
            DataContext = new PinDialogVM();
            pinActionConfig = action;
            InitializeComponent();
            PrimerNumPinTextBox.Focus();
        }

        //Comando para comprobar el pin introducido o cambiarlo por otro.
        private void SavePinCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (pinActionConfig == PinConfig.Insert_Pin)
            {
                if ((DataContext as PinDialogVM).CompruebaPIN())
                {
                    MessageBox.Show("PIN Correcto", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("El pin introducido no es correcto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    PrimerNumPinTextBox.Focus();
                }
            }
            else
            {
                //Si la acción es la de actualizar se muestran otros textbox con otro título y se aplica el foco al primero de éstos.
                if ((DataContext as PinDialogVM).CompruebaPIN())
                {
                    PinTituloTextBlock.Visibility = Visibility.Collapsed;
                    TablaNumerosPinGrid.Visibility = Visibility.Collapsed;

                    NuevoPinTituloTextBlock.Visibility = Visibility.Visible;
                    TablaUpdatePinGrid.Visibility = Visibility.Visible;

                    UpdatePrimerNumPinTextBox.Focus();

                    AceptarButton.Content = "Guardar PIN";
                    //Se cambia el comando del botón de aceptar por el de actualizar el pin.
                    AceptarButton.Command = CustomCommands.UpdatePIN;
                }
                else
                {
                    MessageBox.Show("El pin introducido no es correcto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    UpdatePrimerNumPinTextBox.Focus();
                }
            }
        }

        private void SavePinCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as PinDialogVM).SavePinCanExecute();
        }

        //Comando para actualizar el pin
        private void UpdatePINCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if ((DataContext as PinDialogVM).UpdatePIN())
            {
                MessageBox.Show("PIN actualizado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                this.DialogResult = true;
            }
            else
                MessageBox.Show("El pin introducido no es correcto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UpdatePINCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as PinDialogVM).PINUpdateCanExecute();
        }

        //MManejador de evento click para el botón cancelar que cierra el diálogo
        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        //Manejador de evento TextChanged para los textbox donde se introduce cada número del pin
        private void NumPinTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Se comprueba si sólo se introducen dígitos del 0 al 9
            Regex patron = new Regex(@"[0-9]{1}");

            //Si no coincide no se escribe nada.
            if (!patron.IsMatch((sender as TextBox).Text))
                (sender as TextBox).Text = "";
            else
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);

                //Si se escribe un digito el foco y el cursor pasan al textbox siguiente
                if (Keyboard.FocusedElement is UIElement elementWithFocus)
                {
                    elementWithFocus.MoveFocus(request);
                }
            }
        }


        //Cuando todos los dígitos se han introducido el botón aceptar se activa y entonces se le aplica el foco.
        private void AceptarButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender as Button).IsEnabled)
                (sender as Button).Focus();
        }

        //Manejador de evento cuando para seleccionar el contenido del textbox cada vez que se le aplica el foco
        private void PinTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
    }
}
