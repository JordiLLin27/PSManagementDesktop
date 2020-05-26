using PSManagement.Model;
using PSManagement.ViewModel;

using System.Collections.ObjectModel;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;


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

        //Comando para aplicar los descuentos a la factura actual
        private void DiscountCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.DialogResult = (DataContext as SeleccionDescuentoVM).DiscountExecuted();
            }
            catch (System.Exception)
            {
                MessageBox.Show("No ha sido posible aplicar el descuento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
            }
        }

        private void DiscountCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as SeleccionDescuentoVM).DiscountCanExecute;
        }

        //Manejador de evento para seleccionar los artículos de la lista a los que se va a aplicar algún descuento
        private void ListBox_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as SeleccionDescuentoVM).AddListaDescuentos((detallesfactura)((ListBoxItem)sender).Content);
            }
            catch (System.Exception)
            {
                MessageBox.Show("No se ha podido seleccionar para su descuento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Si el usuario se ha equivocado se puede deseleccionar los artículos de la lista y el manejador de este evento elimina el artículo no deseado
        private void ListBox_Unselected(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as SeleccionDescuentoVM).RemoveListaDescuentos((detallesfactura)((ListBoxItem)sender).Content);
            }
            catch (System.Exception)
            {

                MessageBox.Show("No se ha podido eliminar el artículo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        //Se comprueba si se ha seleccionado algún tipo de descuento antes de poder seleccionar los artículos de la factura a los que se va a aplicar.
        private void SeleccionDescuentoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((DataContext as SeleccionDescuentoVM).PuedeSeleccionarArticulos())
            {
                ListaDetallesArticulosFacturaListBox.IsEnabled = true;
            }
        }
    }
}
