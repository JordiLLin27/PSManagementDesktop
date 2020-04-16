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
using System.Windows.Shapes;

namespace PSManagement.Dialogs
{
    /// <summary>
    /// Lógica de interacción para PinDialog.xaml
    /// </summary>
    public partial class PinDialog : Window
    {
        private readonly PinConfig pinActionConfig;

        public PinDialog(PinConfig action)
        {
            pinActionConfig = action;
            InitializeComponent();
        }

        private void SavePinCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void SavePinCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }
    }
}
