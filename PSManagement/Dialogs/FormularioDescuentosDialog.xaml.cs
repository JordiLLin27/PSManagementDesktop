using PSManagement.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;


namespace PSManagement.Dialogs
{
    /// <summary>
    /// Lógica de interacción para FormularioDescuentosDialog.xaml
    /// </summary>
    public partial class FormularioDescuentosDialog : Window
    {
        public FormularioDescuentosDialog(DiscountAction accion)
        {
            DataContext = new FormularioDescuentoVM(accion);
            InitializeComponent();

            //Según la acción a realizar se cambia el contenido del botón principal de la ventana (con el que se aplican los cambios)
            if (accion == DiscountAction.Add_Discount)
                AceptarEliminarButton.Content = "Aceptar";

            else if (accion == DiscountAction.Update_Discount)
            {
                //Si se editan descuentos aparece un combobox con los descuentos disponibles para editar
                AceptarEliminarButton.Content = "Actualizar";
                SeleccionarDescuentoComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                //Si se eliminan descuentos aparece un combobox con los descuentos disponibles para eliminar
                AceptarEliminarButton.Content = "Eliminar";
                SeleccionarDescuentoComboBox.Visibility = Visibility.Visible;
                //Se aplica el método Sólo para que el usuario no pueda modificar ningún campo en la ventana.
                SoloLectura();
            }
        }

        //Comando para aplicar los cambios en la base de datos. Por pantalla se muestra un mensaje indicando el número de cambios realizados
        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                int cambios = (DataContext as FormularioDescuentoVM).SaveExecuted();
                if (cambios > 0)
                {
                    MessageBox.Show("Se ha producido " + cambios + " cambio/s con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error y no se ha podido realizar la acción", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.DialogResult = false;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ha ocurrido un error y no se ha podido realizar la acción", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
            }

        }

        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as FormularioDescuentoVM).SaveCanExecute();
        }

        //Comando para deshacer los cambios que no se hayan guardado en la base de datos.
        private void RevertCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as FormularioDescuentoVM).RevertChanges();
        }

        //Se controla el cambio en los checkbox para establecer una condicion u otra en el objeto de la base de datos.
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            int condicion;
            if (((CheckBox)sender).Name.Equals("UdsDeterminadasCheckBox"))
                condicion = 1;
            else
                condicion = 2;

            (DataContext as FormularioDescuentoVM).SetCondicion(condicion);

        }

        //Se aplica cuando no se quiere que el usuario realice cambios.
        private void SoloLectura()
        {
            NombreDescuentoTextBox.IsEnabled = false;
            PorcentajeDescuentoTextBox.IsEnabled = false;
            APartirDeUdsCheckBox.IsEnabled = false;
            UdsDeterminadasCheckBox.IsEnabled = false;
            UnidadesParaCondicionSlider.IsEnabled = false;
        }

        //El botón cancelar revierte cambios también
        private void CancelarDelButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as FormularioDescuentoVM).RevertChanges();
        }

        //Se controla el descuento seleccionado e internamente se cambia el tipo de condición de descuento para que se refleje en los checkbox de la ventana.
        private void SeleccionarDescuentoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as FormularioDescuentoVM).CambiaTipoCondicion();
        }
    }
}
