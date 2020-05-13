using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using PSManagement.Dialogs;
using PSManagement.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace PSManagement.ViewModel
{
    class PanelOpcionesVM : INotifyPropertyChanged
    {

        public string PinIntroducido { get; set; }
        public string RutaFacturas { get; set; }

        public PanelOpcionesVM()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CambiaTema()
        {
            BundledTheme bundled = (BundledTheme)App.Current.Resources.MergedDictionaries.First();

            if (bundled.BaseTheme == BaseTheme.Dark)
            {
                bundled.BaseTheme = BaseTheme.Light;
                Properties.Settings.Default.TemaOscuro = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                bundled.BaseTheme = BaseTheme.Dark;
                Properties.Settings.Default.TemaOscuro = true;
                Properties.Settings.Default.Save();
            }
        }

        //Más opciones de configuración

        public bool CompruebaConexionBD()
        {
            try
            {
                BbddService.GetDescuentos();
                return true;
            }
            catch (Exception) { return false; }
        }

        public void CambiaPINSeguridad()
        {
            PinDialog dialogoPin = new PinDialog(PinConfig.Update_Pin)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ShowInTaskbar = false
            };

            dialogoPin.ShowDialog();
        }

        internal void CambiarRutaFacturas()
        {
            Properties.Settings.Default.Save();
        }

        internal void ElegirRutaFacturas()
        {
            WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog() { SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory, ShowNewFolderButton = false, Description = "Selecciona un directorio para guardar las facturas" };

            if (dialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                RutaFacturas = dialog.SelectedPath;
                Properties.Settings.Default.RutaFacturasDefault = RutaFacturas;
            }
        }
    }
}
