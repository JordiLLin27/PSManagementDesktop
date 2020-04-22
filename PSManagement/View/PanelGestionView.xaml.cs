﻿using PSManagement.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PSManagement.View
{
    /// <summary>
    /// Lógica de interacción para PanelGestionView.xaml
    /// </summary>
    public partial class PanelGestionView : UserControl
    {
        public PanelGestionView()
        {
            this.DataContext = new PanelGestionVM();
            InitializeComponent();
        }

        private void FindCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as PanelGestionVM).FindExecuted();
        }

        private void FindCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as PanelGestionVM).FindCanExecute();
        }

        private void CleanFiltersCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as PanelGestionVM).CleanFIlters();
        }

        private void CleanFiltersCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as PanelGestionVM).FindCanExecute();
        }

        #region ZONA_TALLAS_TEXTILES
        private void AumentarTallaButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PanelGestionVM).AumentaTalla();
        }

        private void DisminuirTallaButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PanelGestionVM).DisminuyeTalla();
        }

        private void TablaTallasArticulos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if ((DataGrid)sender != null)
            {
                if (((DataGrid)sender).CurrentCell.Column != null)
                {
                    string tallaSeleccionada = ((DataGrid)sender).CurrentCell.Column.Header.ToString();
                    (DataContext as PanelGestionVM).ActualizaItemTallasSeleccionado(((DataGrid)sender).CurrentCell.Item);
                    (DataContext as PanelGestionVM).ActualizaTallaSeleccionada(tallaSeleccionada);
                }
            }
        }
        #endregion ZONA_TALLAS_TEXTILES


        #region ZONA_NUMEROS_CALZADO
        private void AumentaNumerosCalzadoButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PanelGestionVM).AumentaNumero();
        }

        private void DisminuyeNumerosCalzadoButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PanelGestionVM).DisminuyeNumero();
        }

        private void TablaNumerosArticulos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if ((DataGrid)sender != null)
            {
                if (((DataGrid)sender).CurrentCell.Column != null)
                {
                    string numeroCalzadoSeleccionado = ((DataGrid)sender).CurrentCell.Column.Header.ToString();
                    (DataContext as PanelGestionVM).ActualizaItemNumerosSeleccionado(((DataGrid)sender).CurrentCell.Item);
                    (DataContext as PanelGestionVM).ActualizaNumeroSeleccionado(numeroCalzadoSeleccionado);
                }
            }
        }
        #endregion ZONA_NUMEROS_CALZADO

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if ((DataContext as PanelGestionVM).PidePIN())
            {
                int cambios = (DataContext as PanelGestionVM).GuardarStock();
                if (cambios > 0)
                    MessageBox.Show("Cambios guardados con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (cambios == 0)
                    MessageBox.Show("No se ha producido ningún cambio", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    MessageBox.Show("No se han podido guardar los cambios", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("No se ha producido ningún cambio", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
