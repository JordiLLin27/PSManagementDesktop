using MaterialDesignThemes.Wpf;
using PSManagement.Dialogs;
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
using System.Windows.Threading;

namespace PSManagement.View
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipalView.xaml
    /// </summary>
    public partial class PanelPrincipalView : Window
    {
        DispatcherTimer timer;
        UIElement panelAnterior;
        public PanelPrincipalView()
        {
            InitializeComponent();
            FechaTextBlock.Text = DateTime.Now.ToLongDateString();
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += HoraTimer_Tick;
            timer.Start();

            TextoBienvenidaPorDefectoView textoBienvenida = new TextoBienvenidaPorDefectoView
            {
                Margin = new Thickness(10)
            };
            panelAnterior = textoBienvenida;
            PanelDeTrabajoGrid.Children.Add(textoBienvenida);
        }

        private void HoraTimer_Tick(object sender, EventArgs e)
        {
            HoraTextBlock.Text = DateTime.Now.ToLongTimeString();
        }

        private void VentasButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ExitCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ExitDialog ventanaSalir = new ExitDialog
            {
                Owner = this,
                ShowInTaskbar = false,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };


            if (ventanaSalir.ShowDialog() == true)
            {
                Application.Current.Shutdown();
            }
        }

        private void ConfiguracionButton_Click(object sender, RoutedEventArgs e)
        {
            BundledTheme bundled = (BundledTheme)App.Current.Resources.MergedDictionaries.First();

            
            if (bundled.BaseTheme == BaseTheme.Dark)
            {
                bundled.BaseTheme = BaseTheme.Light;
            }
            else
            {
                bundled.BaseTheme = BaseTheme.Dark;
            }
            
        }
    }
}
