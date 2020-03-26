using MaterialDesignThemes.Wpf;
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
    }
}
