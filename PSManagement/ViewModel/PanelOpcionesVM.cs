using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSManagement.ViewModel
{
    class PanelOpcionesVM
    {
        public PanelOpcionesVM()
        {

        }


        public void CambiaTema()
        {
            BundledTheme bundled = (BundledTheme)App.Current.Resources.MergedDictionaries.First();

            if (bundled.BaseTheme == BaseTheme.Dark)
            {
                bundled.BaseTheme = BaseTheme.Light;
            }
            else
            {
                bundled.BaseTheme = BaseTheme.Dark;
            }
        }

        //Más opciones de configuración
    }
}
