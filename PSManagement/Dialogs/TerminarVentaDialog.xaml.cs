using PSManagement.Model;
using PSManagement.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PSManagement.Dialogs
{
    /// <summary>
    /// Lógica de interacción para TerminarVentaDialog.xaml
    /// </summary>
    public partial class TerminarVentaDialog : Window
    {
        public TerminarVentaDialog(facturas factura, ObservableCollection<detallesfactura> detallesArticulosFactura)
        {
            DataContext = new TerminarVentaDialogVM(factura, detallesArticulosFactura);
            InitializeComponent();
        }

        private void PagoEnEfectivoButton_Click(object sender, RoutedEventArgs e)
        {
            PanelTerminarVentaEfectivoGrid.Visibility = Visibility.Visible;
            PanelTerminarVentaTarjetaGrid.Visibility = Visibility.Collapsed;
            (DataContext as TerminarVentaDialogVM).SetTipoPagoEfectivo();
        }

        private void PagoEnTarjetaButton_Click(object sender, RoutedEventArgs e)
        {
            PanelTerminarVentaTarjetaGrid.Visibility = Visibility.Visible;
            PanelTerminarVentaEfectivoGrid.Visibility = Visibility.Collapsed;
            (DataContext as TerminarVentaDialogVM).SetTipoPagoTarjeta();
        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = (DataContext as TerminarVentaDialogVM).SaveExecuted();
        }

        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as TerminarVentaDialogVM).SaveCanExecute();
        }

        private void CantidadAbonadaClienteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as TextBox).Text))
            {
                try
                {
                    if ((sender as TextBox).Text.Substring((sender as TextBox).Text.Length - 1, 1) == ".")
                    {
                        (sender as TextBox).Text = (sender as TextBox).Text.Replace(".", ",");
                        (sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;
                    }

                    (DataContext as TerminarVentaDialogVM).CalcularCambio();
                }
                catch (Exception)
                {
                    MessageBox.Show("Introduce una cantidad correcta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
