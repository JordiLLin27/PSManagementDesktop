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
        public PanelPrincipalView()
        {
            this.DataContext = new PanelPrincipalViewModel();
            InitializeComponent();

            CargaFechaYHora();

            PanelDeTrabajoGrid.Children.Add((this.DataContext as PanelPrincipalViewModel).CargaPanelBienvenida());
            CambiaTituloIconoMenuNav();
        }

        private void HoraTimer_Tick(object sender, EventArgs e)
        {
            HoraTextBlock.Text = DateTime.Now.ToLongTimeString();
        }

        private void CargaFechaYHora()
        {

            FechaTextBlock.Text = DateTime.Now.ToLongDateString();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += HoraTimer_Tick;
            timer.Start();
        }

        private void VentasButton_Click(object sender, RoutedEventArgs e)
        {
        }



        private void HelpCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }


        //Comando para cargar el panel de opciones del programa

        private void PropertiesCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PanelDeTrabajoGrid.Children.Clear();
            PanelDeTrabajoGrid.Children.Add((this.DataContext as PanelPrincipalViewModel).CargaPanelOpciones());
            CambiaTituloIconoMenuNav();

        }

        //Comando para mostrar el diálogo para salir de la aplicación
        private void ExitCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (this.DataContext as PanelPrincipalViewModel).ExitDialog
                (
                    new ExitDialog
                    {
                        Owner = this,
                        ShowInTaskbar = false,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    }
                );
        }

        private void ExitCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        //Comando para el botón de volver al panel anterior
        private void BrowseBackCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PanelDeTrabajoGrid.Children.Clear();
            PanelDeTrabajoGrid.Children.Add((this.DataContext as PanelPrincipalViewModel).ComandoBotonAtrás());
            CambiaTituloIconoMenuNav();
        }

        private void BrowseBackCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !((this.DataContext as PanelPrincipalViewModel).PanelActual is TextoBienvenidaPorDefectoView);
        }

        private void CambiaTituloIconoMenuNav()
        {

            string texto = "";
            PackIcon icon = new PackIcon();

            if ((this.DataContext as PanelPrincipalViewModel).PanelActual is PanelOpcionesVIew)
            {
                texto = "Opciones";
                icon.Kind = PackIconKind.Settings;

            }
            else if ((this.DataContext as PanelPrincipalViewModel).PanelActual is TextoBienvenidaPorDefectoView)
            {
                texto = "Panel Principal";
                icon.Kind = PackIconKind.HomeCircle;
            }


            TituloMenuTrabajoTextBlock.Text = texto;
            IconoTituloMaterialDIcon.Kind = icon.Kind;
        }
    }
}
