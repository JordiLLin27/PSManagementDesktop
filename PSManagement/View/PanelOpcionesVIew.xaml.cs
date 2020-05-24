
using PSManagement.ViewModel;
using System;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;


namespace PSManagement.View
{
    /// <summary>
    /// Lógica de interacción para PanelOpcionesVIew.xaml
    /// </summary>
    public partial class PanelOpcionesVIew : UserControl
    {
        public PanelOpcionesVIew()
        {
            this.DataContext = new PanelOpcionesVM();
            InitializeComponent();

            if (Properties.Settings.Default.TemaOscuro)
                CambiaTemaToggleButton.IsChecked = true;
            else
                CambiaTemaToggleButton.IsChecked = false;
        }

        //Método para cambiar entre el tema claro y oscuro
        private void CambiaTemaToggleButton_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as PanelOpcionesVM).CambiaTema();
        }

        //Método para comprobar si la conexión a la base de datos es correcta.
        private void CheckConnectionCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if ((DataContext as PanelOpcionesVM).CompruebaConexionBD())
            {
                MessageBox.Show("Conexión con la base de datos realizada correctamente", "Conectado", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
                MessageBox.Show("La conexión no se ha podido establecer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CheckConnectionCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Método para llamar a las funciones de cambiar el pin o guardar la ruta predeterminada para guardar las facturas.
        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (((Button)e.OriginalSource).Tag.ToString() == "Pin")
            {
                (DataContext as PanelOpcionesVM).CambiaPINSeguridad();
            }
            else if (((Button)e.OriginalSource).Tag.ToString() == "Ruta")
            {
                try
                {
                    (DataContext as PanelOpcionesVM).CambiarRutaFacturas();
                    MessageBox.Show("Ruta guardada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error intentando guardar la ruta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Método para abrir el diálogo para elegir una ruta para guardar las facturas en txt.
        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelOpcionesVM).ElegirRutaFacturas();

            }
            catch (Exception)
            {

                MessageBox.Show("No ha sido posible elegir una ruta válida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
