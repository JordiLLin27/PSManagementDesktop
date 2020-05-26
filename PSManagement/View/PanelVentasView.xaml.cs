using PSManagement.Model;
using PSManagement.ViewModel;
using System;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;

namespace PSManagement.View
{
    /// <summary>
    /// Lógica de interacción para VentasView.xaml
    /// </summary>
    public partial class PanelVentasView : UserControl
    {
        public PanelVentasView()
        {
            try
            {
                DataContext = new PanelVentasVM();
            }
            catch (Exception)
            {
                throw new OperationCanceledException("La conexión a la base de datos ha fallado.");
            }
            InitializeComponent();
        }

        //Comando para limpiar los filtros de búsqeda.
        private void CleanFiltersCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelVentasVM).CleanFiltersExecuted();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void CleanFiltersCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (DataContext as PanelVentasVM).FindCanExecute();
            }
            catch (Exception)
            {
                MessageBox.Show("No hay filtros que limpiar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Comando para realizar el filtro de los artículos que se están mostrando ahora mismo.
        private void FindCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelVentasVM).FindExecuted();
            }
            catch (Exception)
            {
                MessageBox.Show("No hay elementos para filtrar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                FiltroPorNombreOModeloTextBox.Text = null;
                SeleccionarColorFiltroComboBox.SelectedItem = null;
            }

        }

        private void FindCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (DataContext as PanelVentasVM).FindCanExecute();
            }
            catch (Exception)
            {
                MessageBox.Show("No hay elementos para filtrar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Manejador de evento para controlar si el usuario quiere cambiar la categoría seleccionada.
        private void ListaCategoriasListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                (DataContext as PanelVentasVM).CargaArticulosCategoriaSeleccionada();
            }
            catch (Exception)
            {

                MessageBox.Show("No se ha podido seleccionar ninguna categoría", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Manejador de evento doble click en cada elemento que sea un artículo disponible para su venta. No funciona en los botones de categoría o detalles de factura.
        //Si es un artículo se abrirá el panel para seleccionar la talla deseada.
        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((ListBoxItem)sender).DataContext is detallesfactura || ((ListBoxItem)sender).DataContext is categorias)
            {
                e.Handled = true;
            }
            else
                (DataContext as PanelVentasVM).SeleccionarTallaVenta();
        }

        //Comando para abrir el panel de terminar la venta.
        private void SellCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if ((DataContext as PanelVentasVM).SellExecuted())
            {
                MessageBox.Show("Venta realizada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
                MessageBox.Show("No se ha podido realizar la venta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SellCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (DataContext as PanelVentasVM).SellCanExecute();
            }
            catch (Exception)
            {
                MessageBox.Show("No es posible realizar ventas en este momento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Este comando es para limpiar todos los registros de la venta actual
        private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as PanelVentasVM).DeleteExecuted();
        }

        private void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (DataContext as PanelVentasVM).DeleteCanExecute();
            }
            catch (Exception)
            {
                MessageBox.Show("No es posible cancelar la venta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Manejador de evento click en cada botón de los detalles de la factura, llama al método para borrar el ítem seleccionado de la factura actual
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

        //Comando para abrir el formulario para aplicar descuentos
        private void DiscountCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if ((DataContext as PanelVentasVM).DiscountExecuted())
            {
                MessageBox.Show("Descuento aplicado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
                MessageBox.Show("No se ha aplicado ningún descuento", "Sin descuento", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DiscountCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (DataContext as PanelVentasVM).DiscountCanExecute();
            }
            catch (Exception)
            {
                MessageBox.Show("No es posible aplicar descuentos", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
