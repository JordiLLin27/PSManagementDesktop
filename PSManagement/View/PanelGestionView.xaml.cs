using PSManagement.ViewModel;
using System;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;

namespace PSManagement.View
{
    /// <summary>
    /// Lógica de interacción para PanelGestionView.xaml
    /// </summary>
    public partial class PanelGestionView : UserControl
    {
        public PanelGestionView()
        {
            try
            {
                this.DataContext = new PanelGestionVM();
            }
            catch (Exception)
            {
                throw new OperationCanceledException("La conexión a la base de datos ha fallado.");
            }

            InitializeComponent();
        }

        //Comando para filtrar las tablas
        private void FindCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as PanelGestionVM).FindExecuted();
        }

        private void FindCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (DataContext as PanelGestionVM).FindCanExecute();

            }
            catch (Exception)
            {

                e.CanExecute = false;
            }
        }

        //Comando para limpiar los filtros
        private void CleanFiltersCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelGestionVM).CleanFIlters();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CleanFiltersCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (DataContext as PanelGestionVM).CleanFilterCanExecuted();

            }
            catch (Exception)
            {

                e.CanExecute = false;
            }
        }

        #region ZONA_TALLAS_TEXTILES
        //Manejador del evento click en los botones con el símbolo '+' para amuentar en una unidad las tallas cada vez que se pulse.
        private void AumentarTallaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelGestionVM).AumentaTalla();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Manejador del evento click en los botones con el símbolo '-' para disminuir en una unidad las tallas cada vez que se pulse.
        private void DisminuirTallaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelGestionVM).DisminuyeTalla();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Manejador de evento de cambio de selección de celda para controlar qué celda está pulsada (talla) y de qué artículo.
        private void TablaTallasArticulos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if ((DataGrid)sender != null && ((DataGrid)sender).CurrentCell.Column != null)
                {
                    string tallaSeleccionada = ((DataGrid)sender).CurrentCell.Column.Header.ToString();
                    (DataContext as PanelGestionVM).ActualizaItemTallasSeleccionado(((DataGrid)sender).CurrentCell.Item);
                    (DataContext as PanelGestionVM).ActualizaTallaSeleccionada(tallaSeleccionada);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        #endregion ZONA_TALLAS_TEXTILES


        #region ZONA_NUMEROS_CALZADO
        //Manejador del evento click en los botones con el símbolo '+' para amuentar en una unidad los números cada vez que se pulse.
        private void AumentaNumerosCalzadoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelGestionVM).AumentaNumero();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Manejador del evento click en los botones con el símbolo '-' para disminuir en una unidad los números cada vez que se pulse.
        private void DisminuyeNumerosCalzadoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelGestionVM).DisminuyeNumero();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Manejador de evento de cambio de selección de celda para controlar qué celda está pulsada (número calzado) y de qué artículo.
        private void TablaNumerosArticulos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if ((DataGrid)sender != null && ((DataGrid)sender).CurrentCell.Column != null)
                {

                    string numeroCalzadoSeleccionado = ((DataGrid)sender).CurrentCell.Column.Header.ToString();
                    (DataContext as PanelGestionVM).ActualizaItemNumerosSeleccionado(((DataGrid)sender).CurrentCell.Item);
                    (DataContext as PanelGestionVM).ActualizaNumeroSeleccionado(numeroCalzadoSeleccionado);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        #endregion ZONA_NUMEROS_CALZADO

        //Comando para ejecutar el guardado de los cambios realizados en cualquiera de las dos tablas (tallas o números)
        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                //Se pide un pin de confirmación por seguridad
                if ((DataContext as PanelGestionVM).PidePIN())
                {
                    //Se intentan guardar los cambios en la base de datos y se devuelve el número de cambios
                    int cambios = (DataContext as PanelGestionVM).GuardarStock();
                    //Si hay más de 0 cambios --> Ok
                    if (cambios > 0)
                        MessageBox.Show("Cambios guardados con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else if (cambios == 0)
                        //Si hay 0 cambios 
                        MessageBox.Show("No se ha producido ningún cambio", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //Cualquier otro caso
                    else
                        MessageBox.Show("No se han podido guardar los cambios", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //Si no se introduce bien el pin o se cancela la introducción del pin
                else
                    MessageBox.Show("No se ha producido ningún cambio", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Comando para deshacer los cambios que no hayan sido guardados.
        private void UnDoChangesCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelGestionVM).UnDoChangesExecuted();
                MessageBox.Show("Cambios revertidos", "Información", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Manejador de evento doble click para que cada vez que se produzca en una de las celdas donde se pueda introducir tallas o números se muestre un panel númerico en caso de ayuda al usuario.
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (((DataGridCell)sender).Column.Header.ToString() != "Total" && ((DataGridCell)sender).Column.Header.ToString() != "Artículo")
                    (DataContext as PanelGestionVM).DataGridCell_MouseDoubleClick(((DataGridCell)sender).DataContext, ((DataGridCell)sender).Column.Header.ToString());
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
