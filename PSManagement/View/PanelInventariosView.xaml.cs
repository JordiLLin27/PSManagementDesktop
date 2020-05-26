using PSManagement.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;


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
        ItemCRUDAction itemAction;

        public PanelInventariosView()
        {
            try
            {
                this.DataContext = new PanelInventariosVM();
            }
            catch (Exception)
            {
                throw new OperationCanceledException("La conexión a la base de datos a fallado.");
            }
            InitializeComponent();
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

        //Comando para manejar las diferentes acciones de mantenimiento de inventarios
        private void InventoryCrudCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (invAction)
            {
                //insertar un inventario
                case InventoryCRUDAction.Insert_Inventario:

                    if ((this.DataContext as PanelInventariosVM).InsertarInventario(NombreInventarioAddTextBox.Text) > 0)
                        MessageBox.Show("Inventario insertado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                //Modificar un inventario existente   
                case InventoryCRUDAction.Update_Inventario:
                    //Pongo el nombre del inventario en el textbox para que no se actualize directamente.
                    (this.DataContext as PanelInventariosVM).InventarioSeleccionado.Descripcion = EditarNombreInventarioTextBox.Text;
                    if ((this.DataContext as PanelInventariosVM).SaveChanges() > 0)
                        MessageBox.Show("Cambios guardados", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                //Eliminar un inventario existente    
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

            //Después de realizar la acción los textbox se resetean.
            NombreInventarioAddTextBox.Text = "";
            EditarNombreInventarioTextBox.Text = "";
        }

        //Determina si se ejecuta el comando para realizar las acciones de mantenimiento de inventarios según la acción indicada
        private void InventoryCrudCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                if (InventariosTabControl != null)
                {
                    switch (invAction)
                    {
                        case InventoryCRUDAction.Insert_Inventario:
                            e.CanExecute = (!string.IsNullOrEmpty(NombreInventarioAddTextBox.Text));
                            break;

                        case InventoryCRUDAction.Update_Inventario:
                            e.CanExecute = ((this.DataContext as PanelInventariosVM).InventarioSeleccionado != null) && (!string.IsNullOrEmpty(EditarNombreInventarioTextBox.Text));
                            break;

                        case InventoryCRUDAction.Delete_Inventario:
                            e.CanExecute = (this.DataContext as PanelInventariosVM).InventarioSeleccionado != null;
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {

                e.CanExecute = false;
            }
        }

        //Comando para limpiar los campos de la pestaña seleccionada del groupbox de mantenimiento de inventarios
        private void LimpiarInventarioCrudCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (invAction)
            {
                case InventoryCRUDAction.Insert_Inventario:
                    NombreInventarioAddTextBox.Text = "";
                    break;
                case InventoryCRUDAction.Update_Inventario:
                    EditarNombreInventarioTextBox.Text = "";
                    (DataContext as PanelInventariosVM).InventarioSeleccionado = null;
                    break;
                case InventoryCRUDAction.Delete_Inventario:
                    (DataContext as PanelInventariosVM).InventarioSeleccionado = null;
                    break;
                default:
                    break;
            }
        }
        #endregion GroupBox_Inventarios

        #region GroupBox_Categorias
        //Comando para manejar las diferentes acciones de mantenimiento de inventarios
        private void CategoryCrudCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (categoryAction)
            {
                //Insertar una nueva categoría
                case CategoryCRUDAction.Insert_Categoria:
                    if ((this.DataContext as PanelInventariosVM).InsertarCategoria(NombreCategoriaAddTextBox.Text) > 0)
                        MessageBox.Show("Categoría insertada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                //Modificar una categoría existente
                case CategoryCRUDAction.Update_Categoria:
                    //No se hace a través de Binding para que no se modifique el original mientras se está editando.
                    (this.DataContext as PanelInventariosVM).CategoriaSeleccionada.Categoria = EditarNombreCategoriaTextBox.Text;
                    if ((this.DataContext as PanelInventariosVM).SaveChanges() > 0)
                        MessageBox.Show("Cambios guardados", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                //Eliminar una categoría existente
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
            //Después de realizar una acción los TextBox se resetean.
            NombreCategoriaAddTextBox.Text = "";
            EditarNombreCategoriaTextBox.Text = "";
        }

        //Determina si se ejecuta el coando para el mantenimiento de categorías según la accíon determinada
        private void CategoryCrudCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                if (CategoriasTabControl != null)
                {
                    switch (categoryAction)
                    {
                        case CategoryCRUDAction.Insert_Categoria:
                            e.CanExecute = (!string.IsNullOrEmpty(NombreCategoriaAddTextBox.Text));
                            break;
                        case CategoryCRUDAction.Update_Categoria:
                            e.CanExecute = ((this.DataContext as PanelInventariosVM).CategoriaSeleccionada != null && (!string.IsNullOrEmpty(EditarNombreCategoriaTextBox.Text)));
                            break;
                        case CategoryCRUDAction.Delete_Categoria:
                            e.CanExecute = ((this.DataContext as PanelInventariosVM).CategoriaSeleccionada != null);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {

                e.CanExecute = false;
            }
        }

        //Comando para resetear todos los campos del groupbox de mantenimiento de categorías
        private void LimpiarCategoriaCrudCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (categoryAction)
            {
                case CategoryCRUDAction.Insert_Categoria:
                    NombreCategoriaAddTextBox.Text = "";
                    break;
                case CategoryCRUDAction.Update_Categoria:
                    EditarNombreCategoriaTextBox.Text = "";
                    (DataContext as PanelInventariosVM).CategoriaSeleccionada = null;
                    break;
                case CategoryCRUDAction.Delete_Categoria:
                    (DataContext as PanelInventariosVM).CategoriaSeleccionada = null;
                    break;
                default:
                    break;
            }
        }

        #endregion GroupBox_Categorias

        #region GroupBox_Colores
        //Comando para manejar las diferentes acciones de mantenimiento de colores
        private void ColorCrudCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (colorAction)
            {
                //Insertar un nuevo color
                case ColorCRUDAction.Insert_Color:
                    if ((this.DataContext as PanelInventariosVM).InsertarColor(NombreColorAddTextBox.Text) > 0)
                        MessageBox.Show("Color insertado con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                //Editar un color existente
                case ColorCRUDAction.Update_Color:
                    (this.DataContext as PanelInventariosVM).ColorSeleccionado.Color = EditarNombreColorTextBox.Text;
                    if ((this.DataContext as PanelInventariosVM).SaveChanges() > 0)
                        MessageBox.Show("Cambios guardados", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Error en la operación", "Error en la operación", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                //Eliminar un color existente        
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
            EditarNombreColorTextBox.Text = "";
            NombreColorAddTextBox.Text = "";
        }

        //Determina si se ejecuta el comando para el mantenimiento de colores según la acción indicada
        private void ColorCrudCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                if (ColoresTabControl != null)
                {
                    switch (colorAction)
                    {
                        case ColorCRUDAction.Insert_Color:
                            e.CanExecute = (!string.IsNullOrEmpty(NombreColorAddTextBox.Text));
                            break;
                        case ColorCRUDAction.Update_Color:
                            e.CanExecute = ((this.DataContext as PanelInventariosVM).ColorSeleccionado != null && (!string.IsNullOrEmpty(EditarNombreColorTextBox.Text)));
                            break;
                        case ColorCRUDAction.Delete_Color:
                            e.CanExecute = ((this.DataContext as PanelInventariosVM).ColorSeleccionado != null);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {

                e.CanExecute = false;
            }

        }

        //Comando para limpiar los campos del groupbox de mantenimiento de colores.
        private void LimpiarColorCrudCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (colorAction)
            {
                case ColorCRUDAction.Insert_Color:
                    NombreColorAddTextBox.Text = "";
                    break;
                case ColorCRUDAction.Update_Color:
                    EditarNombreColorTextBox.Text = "";
                    (DataContext as PanelInventariosVM).ColorSeleccionado = null;
                    break;
                case ColorCRUDAction.Delete_Color:
                    (DataContext as PanelInventariosVM).ColorSeleccionado = null;
                    break;
                default:
                    break;
            }
        }

        #endregion GroupBox_Colores

        #region GroupBox_Items
        //Manejador del comando para cambiar la acción que se va a realizar sobre los articulos del inventario según el botón que se haya pulsado y abrir el formulario de mantenimiento de artículos.
        private void ItemCrudCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                switch ((e.OriginalSource as Button).Name)
                {
                    case "NuevoArticuloButton":
                        itemAction = ItemCRUDAction.Insert_Item;

                        break;

                    case "UpdateArticuloButton":
                        itemAction = ItemCRUDAction.Update_Item;

                        break;

                    case "DeleteArticuloButton":
                        itemAction = ItemCRUDAction.Delete_Item;

                        break;

                    //La acción por defecto es la de insertar un nuevo artículo
                    default:
                        itemAction = ItemCRUDAction.Insert_Item;

                        break;
                }

                //Después de seleccionar la acción se muestra el diálogo con el formulario de mantenimiento de artículos
                FormularioCrudArticuloDialogView formularioCrudArticulo = (DataContext as PanelInventariosVM).ItemCrud_Execute(itemAction);

                if (formularioCrudArticulo != null)
                {
                    formularioCrudArticulo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    formularioCrudArticulo.ShowInTaskbar = false;

                    formularioCrudArticulo.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("ha ocurrido un error mostrando el formulario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void ItemCrudCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (DataContext as PanelInventariosVM).ItemCrudCommandBinding_CanExecute((e.OriginalSource as Button).Name);

            }
            catch (Exception)
            {

                e.CanExecute = false;
            }
        }

        #region Filtrado_de_datos

        //Comando para filtrar la tabla de artículos
        private void FilterCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as PanelInventariosVM).FiltroArticulos();
        }

        private void FilterCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (DataContext as PanelInventariosVM).Filter_CanExecute();
            }
            catch (Exception)
            {
                e.CanExecute = false;
            }
        }

        //Comando para borrar los filtros y mostrar toda la tabla
        private void CleanFiltersCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as PanelInventariosVM).CleanFilters_Execute();
            (DataContext as PanelInventariosVM).FiltroArticulos();
        }

        private void CleanFiltersCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                e.CanExecute = (DataContext as PanelInventariosVM).Filter_CanExecute();
            }
            catch (Exception)
            {

                e.CanExecute = false;
            }
        }

        //Manejador de evento para que se aplique el filtro de nombre o modelo a la vez que se va escribiendo.
        private void BarraFiltroTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                (DataContext as PanelInventariosVM).FiltroArticulos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Evento que indica si la cantidad de una talla del artículo seleccionado ha alcanzado o está por debajo del stock mínimo
        private void TextBlock_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            int cantidadActual = (string.IsNullOrEmpty(((TextBlock)sender).Text) ? -1 : int.Parse(((TextBlock)sender).Text));

            if (cantidadActual >= 0)
            {
                if ((DataContext as PanelInventariosVM).CantidadMinimaAlcanzada(cantidadActual))
                {
                    ((TextBlock)sender).Background = Brushes.Red;
                }
                else
                {
                    ((TextBlock)sender).Background = null;
                }
            }
        }

        #endregion Filtrado_de_datos

        #endregion GroupBox_Items  
    }
}
