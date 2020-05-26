using System.Windows;


namespace PSManagement.Dialogs
{
    /// <summary>
    /// Lógica de interacción para ExitDialog.xaml
    /// </summary>
    public partial class ExitDialog : Window
    {
        public ExitDialog()
        {
            InitializeComponent();
        }

        private void BotonYesButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
