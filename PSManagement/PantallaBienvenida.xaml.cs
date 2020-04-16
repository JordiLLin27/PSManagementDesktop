using MaterialDesignThemes.Wpf;
using PSManagement.View;
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
using System.Windows.Threading;

namespace PSManagement
{
    public enum InventoryCRUDAction
    {
        Insert_Inventario,
        Update_Inventario,
        Delete_Inventario
    }
    public enum ColorCRUDAction
    {
        Insert_Color,
        Update_Color,
        Delete_Color
    }

    public enum CategoryCRUDAction
    {
        Insert_Categoria,
        Update_Categoria,
        Delete_Categoria
    }

    public enum ItemCRUDAction
    {
        Insert_Item,
        Update_Item,
        Delete_Item
    }

    public enum PinConfig
    {
        Insert_Pin,
        Update_Pin
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
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3)
            };
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

        private void CompruebaConfig()
        {

            EsTemaOscuro();

        }

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
