using PSManagement.Model;
using PSManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para SeleccionDescuentoDialog.xaml
    /// </summary>
    public partial class SeleccionDescuentoDialog : Window
    {
        public SeleccionDescuentoDialog(facturas facturaDescuento, ObservableCollection<detallesfactura> detallesfactura)
        {
            DataContext = new SeleccionDescuentoVM(facturaDescuento, detallesfactura);
            InitializeComponent();
        }

        private void DiscountCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = (DataContext as SeleccionDescuentoVM).DiscountExecuted();
        }

        private void DiscountCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as SeleccionDescuentoVM).DiscountCanExecute;
        }

        private void ListBox_Selected(object sender, RoutedEventArgs e)
        {
            (DataContext as SeleccionDescuentoVM).AddListaDescuentos((detallesfactura)((ListBoxItem)sender).Content);
        }

        private void ListBox_Unselected(object sender, RoutedEventArgs e)
        {
            (DataContext as SeleccionDescuentoVM).RemoveListaDescuentos((detallesfactura)((ListBoxItem)sender).Content);
        }

        private void SeleccionDescuentoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((DataContext as SeleccionDescuentoVM).PuedeSeleccionarArticulos())
            {
                TextoAyudaTextBox.Visibility = Visibility.Visible;
                ListaDetallesArticulosFacturaListBox.IsEnabled = true;
            }
        }
    }
}
