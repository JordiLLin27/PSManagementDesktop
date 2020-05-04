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
    /// Lógica de interacción para SelectorTallasDialog.xaml
    /// </summary>
    public partial class SelectorTallasDialog : Window
    {
        public SelectorTallasDialog(articulos articulo, ObservableCollection<detallesfactura> lista)
        {
            DataContext = new SelectorTallasDialogVM(articulo, lista);
            InitializeComponent();
            MuestraBotones();
        }

        private void SelectSizeCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string tag = ((Button)e.Source).Tag.ToString();
            string talla = ((Button)e.Source).Content.ToString();

            (DataContext as SelectorTallasDialogVM).SelectSizeExecuted(tag);

            this.DialogResult = true;
        }

        private void MuestraBotones()
        {
            if ((DataContext as SelectorTallasDialogVM).TieneTallas())
            {
                PanelTallasStackPanel.Visibility = Visibility.Visible;
                PanelNumerosWrapPanel.Visibility = Visibility.Collapsed;
            }
            else if ((DataContext as SelectorTallasDialogVM).TieneNumeros())
            {
                PanelNumerosWrapPanel.Visibility = Visibility.Visible;
                PanelTallasStackPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void SelectSizeCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            string tag = ((Button)e.Source).Tag.ToString();
            string talla = ((Button)e.Source).Content.ToString();
            e.CanExecute = (DataContext as SelectorTallasDialogVM).SelectSizeCanExecute(tag);
        }
    }
}
