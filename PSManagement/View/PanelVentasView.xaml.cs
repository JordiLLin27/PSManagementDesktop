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
    }
}
