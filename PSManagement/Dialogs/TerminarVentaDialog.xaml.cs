using PSManagement.Model;
using PSManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            try
            {
                (DataContext as TerminarVentaDialogVM).CalcularCambio();
            }
            catch (Exception)
            {
                MessageBox.Show("Introduce sólo números", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
