using PSManagement.Model;
using PSManagement.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PSManagement.View
{
    /// <summary>
    /// Lógica de interacción para VentasView.xaml
    /// </summary>
    public partial class PanelVentasView : UserControl
    {
        public PanelVentasView()
        {
            DataContext = new PanelVentasVM();
            InitializeComponent();

        }

        private void CleanFiltersCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as PanelVentasVM).CleanFiltersExecuted();
        }

        private void CleanFiltersCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as PanelVentasVM).FindCanExecute();
        }

        private void FindCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelVentasVM).FindExecuted();
            }
            catch (Exception)
            {
                MessageBox.Show("No hay elementos para filtrar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void FindCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as PanelVentasVM).FindCanExecute();
        }

        private void ListaCategoriasListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as PanelVentasVM).CargaArticulosCategoriaSeleccionada();
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((ListBoxItem)sender).DataContext is detallesfactura || ((ListBoxItem)sender).DataContext is categorias)
            {
                e.Handled = true;
            }
            else
                (DataContext as PanelVentasVM).SeleccionarTallaVenta();
        }

        private void SellCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if ((DataContext as PanelVentasVM).SellExecuted())
            {
                MessageBox.Show("Venta realizada", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
                MessageBox.Show("No se ha podido realizar la venta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SellCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as PanelVentasVM).SellCanExecute();
        }

        private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as PanelVentasVM).DeleteExecuted();
        }

        private void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as PanelVentasVM).DeleteCanExecute();
        }

        private void EliminarArticuloDetallesFacturaButton_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as PanelVentasVM).DescuentoAplicado)
            {
                if (MessageBox.Show("Hay descuentos aplicados, si borras un elemento tendrás que volver a aplicarlos ¿Continuar eliminando artículo?", "Aviso", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    (DataContext as PanelVentasVM).BorrarItems(((Button)sender).DataContext as detallesfactura);
                }
                else
                    e.Handled = true;
            }
            else
                (DataContext as PanelVentasVM).BorrarItems(((Button)sender).DataContext as detallesfactura);
        }

        private void DiscountCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if ((DataContext as PanelVentasVM).DiscountExecuted())
            {
                MessageBox.Show("Descuento aplicado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
                MessageBox.Show("No es posible aplicar ese descuento", "Sin descuento", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DiscountCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as PanelVentasVM).DiscountCanExecute();
        }
    }
}
