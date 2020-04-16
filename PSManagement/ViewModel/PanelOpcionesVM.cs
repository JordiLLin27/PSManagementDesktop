using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSManagement.ViewModel
{
    class PanelOpcionesVM : INotifyPropertyChanged
    {

        public string PinIntroducido { get; set; }

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
            return true;
        }

        public void CambiaPINSeguridad()
        {

        }
    }
}
