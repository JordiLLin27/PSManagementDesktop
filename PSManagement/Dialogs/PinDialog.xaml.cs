using PSManagement.Commands;
using PSManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                if ((DataContext as PinDialogVM).CompruebaPIN())
                {
                    PinTituloTextBlock.Visibility = Visibility.Collapsed;
                    TablaNumerosPinGrid.Visibility = Visibility.Collapsed;
                    NuevoPinTituloTextBlock.Visibility = Visibility.Visible;
                    TablaUpdatePinGrid.Visibility = Visibility.Visible;
                    UpdatePrimerNumPinTextBox.Focus();
                    AceptarButton.Content = "Guardar PIN";
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

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumPinTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex patron = new Regex(@"[0-9]{1}");

            if (!patron.IsMatch((sender as TextBox).Text))
                (sender as TextBox).Text = "";
            else
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);


                if (Keyboard.FocusedElement is UIElement elementWithFocus)
                {
                    elementWithFocus.MoveFocus(request);
                }
            }
        }

        private void AceptarButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender as Button).IsEnabled == true)
                (sender as Button).Focus();
        }

        private void PinTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
    }
}
