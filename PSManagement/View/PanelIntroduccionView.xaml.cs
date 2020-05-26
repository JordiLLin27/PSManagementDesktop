using PSManagement.ViewModel;
using System.Windows;
using System.Windows.Controls;


namespace PSManagement.View
{
    /// <summary>
    /// Lógica de interacción para TextoBienvenidaPorDefecto.xaml
    /// </summary>
    public partial class PanelIntroduccionView : UserControl
    {
        public PanelIntroduccionView()
        {
            this.DataContext = new PanelIntroduccionVM();
            InitializeComponent();
        }

        private void HelpVentasCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelIntroduccionVM).HelpVentas();
            }
            catch (System.Exception)
            {
                MessageBox.Show("No se ha podido abrir el manual por la sección de ventas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void HelpGestionCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelIntroduccionVM).HelpGestion();
            }
            catch (System.Exception)
            {
                MessageBox.Show("No se ha podido abrir el manual por la sección de gestión", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void HelpInventariosCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            try
            {
                (DataContext as PanelIntroduccionVM).HelpInventarios();

            }
            catch (System.Exception)
            {

                MessageBox.Show("No se ha podido abrir el manual por la sección de inventarios", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
