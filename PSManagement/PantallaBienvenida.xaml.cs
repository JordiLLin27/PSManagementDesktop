using MaterialDesignThemes.Wpf;
using PSManagement.View;
using System;

using System.Linq;

using System.Windows;

using System.Windows.Threading;

namespace PSManagement
{
    /// <summary>
    /// Enumeracion que indica las acciones CRUD sobre la tabla inventarios
    /// </summary>
    public enum InventoryCRUDAction
    {
        Insert_Inventario,
        Update_Inventario,
        Delete_Inventario
    }

    /// <summary>
    /// Enumeracion que indica las acciones CRUD sobre la tabla colores
    /// </summary>
    public enum ColorCRUDAction
    {
        Insert_Color,
        Update_Color,
        Delete_Color
    }

    /// <summary>
    /// Enumeración que indica las acciones CRUD sobre la tabla categorías
    /// </summary>
    public enum CategoryCRUDAction
    {
        Insert_Categoria,
        Update_Categoria,
        Delete_Categoria
    }

    /// <summary>
    /// Enumeración que indica las acciones CRUD sobre la tabla artículos
    /// </summary>
    public enum ItemCRUDAction
    {
        Insert_Item,
        Update_Item,
        Delete_Item
    }

    /// <summary>
    /// Enumeración que indica las acciones a realizar sobre el pin de seguridad
    /// </summary>
    public enum PinConfig
    {
        Insert_Pin,
        Update_Pin
    }

    /// <summary>
    /// Enumeración que indica las acciones CRUD sobre la tabla descuentos
    /// </summary>
    public enum DiscountAction
    {
        Add_Discount,
        Update_Discount,
        Delete_Discount
    }

    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            PanelPrincipalView ventanaPrincipal = new PanelPrincipalView();
            CompruebaConfig();
            ventanaPrincipal.Show();
            timer.Stop();
            Close();
        }

        //Comprueba la configuración inicial
        private void CompruebaConfig()
        {

            EsTemaOscuro();
        }

        //Comprueba el tema actual
        private void EsTemaOscuro()
        {
            BundledTheme bundled = (BundledTheme)App.Current.Resources.MergedDictionaries.First();

            if (Properties.Settings.Default.TemaOscuro)
            {
                bundled.BaseTheme = BaseTheme.Dark;
            }
            else
                bundled.BaseTheme = BaseTheme.Light;
        }
    }
}
