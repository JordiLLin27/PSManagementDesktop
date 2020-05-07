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
            this.DataContext = new PanelPrincipalVM();
            InitializeComponent();

            CargaFechaYHora();

            PanelDeTrabajoGrid.Children.Add((this.DataContext as PanelPrincipalVM).PanelActual);
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

        //Comando para cargar el manual de ayuda
        private void HelpCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }


        //Comando para cargar el panel de opciones del programa

        private void PropertiesCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PanelDeTrabajoGrid.Children.Clear();
            PanelDeTrabajoGrid.Children.Add((this.DataContext as PanelPrincipalVM).CargaPanelOpciones());
            CambiaTituloIconoMenuNav();

        }

        private void PropertiesCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !((this.DataContext as PanelPrincipalVM).PanelActual is PanelOpcionesVIew);
        }

        //Comando para mostrar el diálogo para salir de la aplicación
        private void ExitCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (this.DataContext as PanelPrincipalVM).ExitDialog
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
            PanelDeTrabajoGrid.Children.Add((this.DataContext as PanelPrincipalVM).BotonHome());
            CambiaTituloIconoMenuNav();
        }

        private void BrowseBackCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !((this.DataContext as PanelPrincipalVM).PanelActual is PanelIntroduccionView);
        }

        //Método que cambia el titulo del menu de navegación según el panel.
        private void CambiaTituloIconoMenuNav()
        {

            string texto = "";
            PackIcon icon = new PackIcon();

            if ((this.DataContext as PanelPrincipalVM).PanelActual is PanelOpcionesVIew)
            {
                texto = "Opciones";
                icon.Kind = PackIconKind.Settings;

            }
            else if ((this.DataContext as PanelPrincipalVM).PanelActual is PanelIntroduccionView)
            {
                texto = "Panel Principal";
                icon.Kind = PackIconKind.HomeCircle;
            }
            else if ((this.DataContext as PanelPrincipalVM).PanelActual is PanelInventariosView)
            {
                texto = "Inventarios";
                icon.Kind = PackIconKind.Bookshelf;
            }
            else if ((this.DataContext as PanelPrincipalVM).PanelActual is PanelVentasView)
            {
                texto = "Ventas";
                icon.Kind = PackIconKind.CashRegister;
            }
            else if ((this.DataContext as PanelPrincipalVM).PanelActual is PanelGestionView)
            {
                texto = "Gestión de Stock";
                icon.Kind = PackIconKind.TableEdit;
            }


            TituloMenuTrabajoTextBlock.Text = texto;
            IconoTituloMaterialDIcon.Kind = icon.Kind;
        }

        //Comando para cambiar al panel de ventas y facturación
        private void SalesCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PanelDeTrabajoGrid.Children.Clear();
            PanelDeTrabajoGrid.Children.Add((this.DataContext as PanelPrincipalVM).CargaPanelVentas());
            CambiaTituloIconoMenuNav();
        }


        private void SalesCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !((this.DataContext as PanelPrincipalVM).PanelActual is PanelVentasView);
        }

        //Comando para cambiar al panel de mantenimiento de inventarios
        private void InventoryCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PanelDeTrabajoGrid.Children.Clear();
            PanelDeTrabajoGrid.Children.Add((this.DataContext as PanelPrincipalVM).CargaPanelInventarios());
            CambiaTituloIconoMenuNav();
        }

        private void InventoryCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !((this.DataContext as PanelPrincipalVM).PanelActual is PanelInventariosView);
        }

        //Comando para cambiar al panel de gestión de stock
        private void ManageCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PanelDeTrabajoGrid.Children.Clear();
            PanelDeTrabajoGrid.Children.Add((this.DataContext as PanelPrincipalVM).CargaPanelGestion());
            CambiaTituloIconoMenuNav();
        }

        private void ManageCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !((this.DataContext as PanelPrincipalVM).PanelActual is PanelGestionView);
        }

        //Evento que maneja el click de los items del menú 'Tienda' para añadir, editar o eliminar un descuento.
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DiscountAction action;
            MenuItem menuItem = (MenuItem)sender;

            if (menuItem.Tag.ToString().Equals("Add"))
                action = DiscountAction.Add_Discount;
            else if (menuItem.Tag.ToString().Equals("Update"))
                action = DiscountAction.Update_Discount;
            else
                action = DiscountAction.Delete_Discount;

            if ((DataContext as PanelPrincipalVM).DiscountExecuted(action))
            {
                MessageBox.Show("Operación realizada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
                MessageBox.Show("Se ha cancelado la operación", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
