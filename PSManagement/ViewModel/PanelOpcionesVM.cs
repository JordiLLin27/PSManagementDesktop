using MaterialDesignThemes.Wpf;
using PSManagement.Dialogs;
using PSManagement.Service;
using System;
using System.ComponentModel;
using System.Linq;
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

        //Cambia el tema actual. Si el tema es el oscuro, lo cambia al claro y viceversa.
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

        //Ejecuta una consulta a la base de datos. Devuelve verdadero si la consulta se ha ejecutado correctamente, falso en otro caso.
        public bool CompruebaConexionBD()
        {
            try
            {
                BbddService.GetDescuentos();
                return true;
            }
            catch (Exception) { return false; }
        }

        //Muestra el diálogo para cambiar el pin actual.
        public void CambiaPINSeguridad()
        {
            PinDialog dialogoPin = new PinDialog(PinConfig.Update_Pin)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ShowInTaskbar = false
            };

            dialogoPin.ShowDialog();
        }

        //Guarda la ruta seleccionada para guardar los archivos .txt de las facturas.
        internal void CambiarRutaFacturas()
        {
            Properties.Settings.Default.Save();
        }

        //Muestra el diálogo para elegir la ruta para guardar los archivos .txt de las facturas.
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
