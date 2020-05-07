using MaterialDesignThemes.Wpf;
using PSManagement.Dialogs;
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

        private void CambiaTemaToggleButton_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as PanelOpcionesVM).CambiaTema();
        }

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

        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as PanelOpcionesVM).ElegirRutaFacturas();
        }

        private void OpenCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
