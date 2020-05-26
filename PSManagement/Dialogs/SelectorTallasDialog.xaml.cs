using PSManagement.Model;
using PSManagement.ViewModel;
using System;
using System.Collections.ObjectModel;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;


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

        //Comando para seleccionar una talla o número según el botón pulsado
        private void SelectSizeCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //El tag de cada botón indica qué talla o número se va a escoger.
            string tag = ((Button)e.Source).Tag.ToString();
            (DataContext as SelectorTallasDialogVM).SelectSizeExecuted(tag);

            this.DialogResult = true;
        }

        //Función que muestra los botones de selección de tallas o números según si el artículo tiene tallas o números.
        private void MuestraBotones()
        {
            //Se comprueba en la vista-modelo
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
            //se comprueba si el tag es correcto para poder ejecutar el comando.
            string tag = ((Button)e.Source).Tag.ToString();
            e.CanExecute = (DataContext as SelectorTallasDialogVM).SelectSizeCanExecute(tag);
        }
    }
}
