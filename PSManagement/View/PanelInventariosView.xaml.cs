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
    /// Lógica de interacción para PanelInventariosView.xaml
    /// </summary>
    public partial class PanelInventariosView : UserControl
    {
        InventoryCRUDAction invAction;
        ColorCRUDAction colorAction;
        CategoryCRUDAction categoryAction;

        public PanelInventariosView()
        {
            this.DataContext = new PanelInventariosVM();
            InitializeComponent();
            TablaArticulosInventario.DataContext = (this.DataContext as PanelInventariosVM).ListaArticulos;
            TablaNumerosArticuloDataGrid.DataContext = (this.DataContext as PanelInventariosVM).ArticuloSeleccionado;
            TablaTallasArticuloDataGrid.DataContext = (this.DataContext as PanelInventariosVM).ArticuloSeleccionado;
        }

        #region Cambios_En_Tabs
        //Eventos que controlan el cambio en las pestañas de los TabControl de los GroupBox de Inventarios, Colores y Categorias
        //Según qué TabItem esté seleccionado la acción CRUD de cada GroupBox cambia.
        private void InventariosTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddInventarioTabItem.IsSelected)
                invAction = InventoryCRUDAction.Insert_Inventario;
            else if (UpdateInventarioTabItem.IsSelected)
                invAction = InventoryCRUDAction.Update_Inventario;
            else if (DeleteInventarioTabItem.IsSelected)
                invAction = InventoryCRUDAction.Delete_Inventario;
        }

        private void ColoresTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddColorTabItem.IsSelected)
                colorAction = ColorCRUDAction.Insert_Color;
            else if (UpdateColorTabItem.IsSelected)
                colorAction = ColorCRUDAction.Update_Color;
            else if (DeleteColorTabItem.IsSelected)
                colorAction = ColorCRUDAction.Delete_Color;
        }

        private void CategoriasTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddCategoriaTabItem.IsSelected)
                categoryAction = CategoryCRUDAction.Insert_Categoria;
            else if (UpdateCategoriaTabItem.IsSelected)
                categoryAction = CategoryCRUDAction.Update_Categoria;
            else if (DeleteCategoriaTabItem.IsSelected)
                categoryAction = CategoryCRUDAction.Delete_Categoria;
        }
        #endregion Cambios_En_Tabs

        #region GroupBox_Inventarios
        private void InventoryCrudCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (invAction)
            {
                case InventoryCRUDAction.Insert_Inventario:

                    if ((this.DataContext as PanelInventariosVM).InsertarInventario(NombreInventarioAddTextBox.Text) > 0)
                        MessageBox.Show("Inventario insertado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);

                    break;

                case InventoryCRUDAction.Update_Inventario:
                    (this.DataContext as PanelInventariosVM).InventarioSeleccionado.Descripcion = EditarNombreInventarioTextBox.Text;
                    if ((this.DataContext as PanelInventariosVM).SaveChanges() > 0)
                        MessageBox.Show("Cambios guardados", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                case InventoryCRUDAction.Delete_Inventario:
                    if ((this.DataContext as PanelInventariosVM).DeleteInventario() > 0)
                    {
                        MessageBox.Show("Inventario eliminado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show("Error en la operación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                default:
                    break;
            }

            NombreInventarioAddTextBox.Text = "";
            EditarNombreInventarioTextBox.Text = "";
        }

        private void InventoryCrudCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            switch (invAction)
            {
                case InventoryCRUDAction.Insert_Inventario:
                    e.CanExecute = (NombreInventarioAddTextBox.Text != "");
                    break;

                case InventoryCRUDAction.Update_Inventario:
                    e.CanExecute = ((this.DataContext as PanelInventariosVM).InventarioSeleccionado != null) && (EditarNombreInventarioTextBox.Text != "");
                    break;

                case InventoryCRUDAction.Delete_Inventario:
                    e.CanExecute = (this.DataContext as PanelInventariosVM).InventarioSeleccionado != null;
                    break;

                default:
                    MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
        #endregion GroupBox_Inventarios

        #region GroupBox_Categorias
        private void CategoryCrudCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (categoryAction)
            {
                case CategoryCRUDAction.Insert_Categoria:
                    if ((this.DataContext as PanelInventariosVM).InsertarCategoria(NombreCategoriaAddTextBox.Text) > 0)
                        MessageBox.Show("Categoría insertada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case CategoryCRUDAction.Update_Categoria:
                    (this.DataContext as PanelInventariosVM).CategoriaSeleccionada.Categoria = EditarNombreCategoriaTextBox.Text;
                    if ((this.DataContext as PanelInventariosVM).SaveChanges() > 0)
                        MessageBox.Show("Cambios guardados", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case CategoryCRUDAction.Delete_Categoria:
                    if ((this.DataContext as PanelInventariosVM).DeleteCategoria() > 0)
                        MessageBox.Show("Categoría eliminada", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                default:
                    MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
            NombreCategoriaAddTextBox.Text = "";
            EditarNombreCategoriaTextBox.Text = "";
        }

        private void CategoryCrudCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            switch (categoryAction)
            {
                case CategoryCRUDAction.Insert_Categoria:
                    e.CanExecute = (NombreCategoriaAddTextBox.Text != "");
                    break;
                case CategoryCRUDAction.Update_Categoria:
                    e.CanExecute = ((this.DataContext as PanelInventariosVM).CategoriaSeleccionada != null && (EditarNombreCategoriaTextBox.Text != ""));
                    break;
                case CategoryCRUDAction.Delete_Categoria:
                    e.CanExecute = ((this.DataContext as PanelInventariosVM).CategoriaSeleccionada != null);
                    break;
                default:
                    break;
            }
        }
        #endregion GroupBox_Categorias

        #region GroupBox_Colores
        private void ColorCrudCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (colorAction)
            {
                case ColorCRUDAction.Insert_Color:
                    if ((this.DataContext as PanelInventariosVM).InsertarColor(NombreColorAddTextBox.Text) > 0)
                        MessageBox.Show("Color insertado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case ColorCRUDAction.Update_Color:
                    (this.DataContext as PanelInventariosVM).ColorSeleccionado.Color = EditarNombreColorTextBox.Text;
                    if ((this.DataContext as PanelInventariosVM).SaveChanges() > 0)
                        MessageBox.Show("Cambios guardados", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case ColorCRUDAction.Delete_Color:
                    if ((this.DataContext as PanelInventariosVM).DeleteColor() > 0)
                        MessageBox.Show("Color eliminado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                default:
                    MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void ColorCrudCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            switch (colorAction)
            {
                case ColorCRUDAction.Insert_Color:
                    e.CanExecute = (NombreColorAddTextBox.Text != "");
                    break;
                case ColorCRUDAction.Update_Color:
                    e.CanExecute = ((this.DataContext as PanelInventariosVM).ColorSeleccionado != null && EditarNombreColorTextBox.Text != "");
                    break;
                case ColorCRUDAction.Delete_Color:
                    e.CanExecute = ((this.DataContext as PanelInventariosVM).ColorSeleccionado != null);
                    break;
                default:
                    break;
            }
        }
        #endregion GroupBox_Colores

        private void RefreshCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void RefreshCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void ActivarModificacionArticulosButton_Click(object sender, RoutedEventArgs e)
        {

            //FALTA HACER
            if (MessageBox.Show("¿Activar edición de artículos?", "Modo Edición", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
            {
            }
        }
    }
}
