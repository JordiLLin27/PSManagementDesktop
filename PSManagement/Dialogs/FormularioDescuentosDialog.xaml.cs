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

            if (accion == DiscountAction.Add_Discount)
                AceptarEliminarButton.Content = "Aceptar";

            else if (accion == DiscountAction.Update_Discount)
            {
                AceptarEliminarButton.Content = "Actualizar";
                SeleccionarDescuentoComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                AceptarEliminarButton.Content = "Eliminar";
                SeleccionarDescuentoComboBox.Visibility = Visibility.Visible;
                SoloLectura();
            }
        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int cambios = (DataContext as FormularioDescuentoVM).SaveExecuted();
            if (cambios > 0)
            {
                MessageBox.Show("Se ha producido " + cambios + " cambio/s con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error, no se ha podido realizar la acción", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
            }
        }

        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as FormularioDescuentoVM).SaveCanExecute();
        }

        private void RevertCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as FormularioDescuentoVM).RevertChanges();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            int condicion;
            if (((CheckBox)sender).Name.Equals("UdsDeterminadasCheckBox"))
                condicion = 1;
            else
                condicion = 2;

            (DataContext as FormularioDescuentoVM).SetCondicion(condicion);

        }

        private void SoloLectura()
        {
            NombreDescuentoTextBox.IsEnabled = false;
            PorcentajeDescuentoTextBox.IsEnabled = false;
            APartirDeUdsCheckBox.IsEnabled = false;
            UdsDeterminadasCheckBox.IsEnabled = false;
            UnidadesParaCondicionSlider.IsEnabled = false;
        }

        private void CancelarDelButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as FormularioDescuentoVM).RevertChanges();
        }

        private void SeleccionarDescuentoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as FormularioDescuentoVM).CambiaTipoCondicion();
        }
    }
}
