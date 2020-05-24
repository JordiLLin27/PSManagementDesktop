using MaterialDesignThemes.Wpf;
using PSManagement.Dialogs;
using PSManagement.ViewModel;
using System;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;

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
            string ruta = System.IO.Directory.GetCurrentDirectory().ToString() + @"\PSManagementManual.chm";
            System.Diagnostics.Process.Start(ruta);
        }


        //Comando para cargar el panel de opciones del programa
        private void PropertiesCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            BarraCargaProgressBar.Visibility = Visibility.Visible;
            CambiaPanel((this.DataContext as PanelPrincipalVM).CargaPanelOpciones());
        }

        private void PropertiesCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = !((this.DataContext as PanelPrincipalVM).PanelActual is PanelOpcionesVIew);
            }
            catch (Exception)
            {
                MessageBox.Show("No ha sido posible cargar el panel de configuración", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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


        //Comando para el botón de volver al panel de inicio
        private void BrowseBackCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            BarraCargaProgressBar.Visibility = Visibility.Visible;
            CambiaPanel((this.DataContext as PanelPrincipalVM).BotonHome());
        }

        private void BrowseBackCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = !((this.DataContext as PanelPrincipalVM).PanelActual is PanelIntroduccionView);
            }
            catch (Exception)
            {
                MessageBox.Show("No ha sido posible volver al panel de inicio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
            BarraCargaProgressBar.Visibility = Visibility.Visible;
            CambiaPanel((this.DataContext as PanelPrincipalVM).CargaPanelVentas());
        }


        private void SalesCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = !((this.DataContext as PanelPrincipalVM).PanelActual is PanelVentasView);
            }
            catch (Exception)
            {
                MessageBox.Show("No ha sido posible cargar el panel de ventas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Comando para cambiar al panel de mantenimiento de inventarios
        private void InventoryCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            BarraCargaProgressBar.Visibility = Visibility.Visible;
            CambiaPanel((this.DataContext as PanelPrincipalVM).CargaPanelInventarios());
        }

        private void InventoryCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = !((this.DataContext as PanelPrincipalVM).PanelActual is PanelInventariosView);
            }
            catch (Exception)
            {
                MessageBox.Show("No ha sido posible cargar el panel de ventas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Comando para cambiar al panel de gestión de stock
        private void ManageCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                BarraCargaProgressBar.Visibility = Visibility.Visible;
                CambiaPanel((this.DataContext as PanelPrincipalVM).CargaPanelGestion());
            }
            catch (Exception)
            {
                MessageBox.Show("No ha sido posible cargar el panel de gestión", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ManageCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = !((this.DataContext as PanelPrincipalVM).PanelActual is PanelGestionView);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hay un problema intentando cargar el panel de gestión.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
                MessageBox.Show("Se ha cancelado la operación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        //Método que cambia el tipo de UserControl (panel) del contenedor de la vista principal.
        private void CambiaPanel(UserControl userControl)
        {
            try
            {
                PanelDeTrabajoGrid.Children.Clear();
                PanelDeTrabajoGrid.Children.Add(userControl);
                CambiaTituloIconoMenuNav();
                BarraCargaProgressBar.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error y no se ha podido cargar el panel seleccionado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                CambiaPanel((DataContext as PanelPrincipalVM).BotonHome());
            }
        }
    }
}
