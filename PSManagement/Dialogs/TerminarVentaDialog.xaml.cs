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

        //Manejador del evento click, cuando se produce, muestra el apartado de pago en efectivo en el otro lado de la vista
        private void PagoEnEfectivoButton_Click(object sender, RoutedEventArgs e)
        {
            PanelTerminarVentaEfectivoGrid.Visibility = Visibility.Visible;
            PanelTerminarVentaTarjetaGrid.Visibility = Visibility.Collapsed;
            (DataContext as TerminarVentaDialogVM).SetTipoPagoEfectivo();
        }

        //Manejador del evento click, cuando se produce, muestra el apartado de pago con tarjeta en el otro lado de la vista
        private void PagoEnTarjetaButton_Click(object sender, RoutedEventArgs e)
        {
            PanelTerminarVentaTarjetaGrid.Visibility = Visibility.Visible;
            PanelTerminarVentaEfectivoGrid.Visibility = Visibility.Collapsed;
            (DataContext as TerminarVentaDialogVM).SetTipoPagoTarjeta();
        }

        //Comando para guardar los cambios en la base de datos y cerrar la venta. Se guarda un fichero .txt con la información de la factura
        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = (DataContext as TerminarVentaDialogVM).SaveExecuted();
        }

        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as TerminarVentaDialogVM).SaveCanExecute();
        }

        //Manejador de evento TextChanged para el textbox donde se introduce la cantidad abonada por el cliente
        private void CantidadAbonadaClienteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as TextBox).Text))
            {
                try
                {   //Si se ha introducido el caracter '.' se reemplazará por el caracter ',' ya que produce error en los calculos en la vista-modelo si no se hace así
                    if ((sender as TextBox).Text.Substring((sender as TextBox).Text.Length - 1, 1) == ".")
                    {
                        (sender as TextBox).Text = (sender as TextBox).Text.Replace(".", ",");
                        //El cursor se mueve a la última posición para seguir escribiendo.
                        (sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;
                    }
                    //Se calcula el cambio para el cliente
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
