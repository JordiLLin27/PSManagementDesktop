using PSManagement.ViewModel;
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
            (DataContext as PanelIntroduccionVM).HelpVentas();
        }

        private void HelpGestionCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            (DataContext as PanelIntroduccionVM).HelpGestion();
        }

        private void HelpInventariosCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            (DataContext as PanelIntroduccionVM).HelpInventarios();
        }
    }
}
