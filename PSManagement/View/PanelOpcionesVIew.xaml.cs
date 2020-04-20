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

        }

        private void CheckConnectionCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void SavePINCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PinDialog dialogoPin = new PinDialog(PinConfig.Update_Pin)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ShowInTaskbar = false
            };

            dialogoPin.ShowDialog();
        }

        private void SavePINCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
