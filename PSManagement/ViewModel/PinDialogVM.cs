using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSManagement.ViewModel
{
    class PinDialogVM : INotifyPropertyChanged
    {
        public string PrimerDigito { get; set; }
        public string SegundoDigito { get; set; }
        public string TercerDigito { get; set; }
        public string CuartoDigito { get; set; }

        public string PrimerDigitoUpdate { get; set; }
        public string SegundoDigitoUpdate { get; set; }
        public string TercerDigitoUpdate { get; set; }
        public string CuartoDigitoUpdate { get; set; }


        public string PinActual { get; set; }
        public string PinUpdate { get; set; }

        public PinDialogVM()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool CompruebaPIN()
        {
            if (!string.IsNullOrEmpty(PinActual))
                if (string.Equals(PinActual, Properties.Settings.Default.PinSeguridad))
                    return true;

            return false;
        }

        public bool UpdatePIN()
        {
            if (!PinUpdate.Equals(PinActual))
            {
                Properties.Settings.Default.PinSeguridad = PinUpdate;
                Properties.Settings.Default.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool PINUpdateCanExecute()
        {
            PinUpdate = PrimerDigitoUpdate + SegundoDigitoUpdate + TercerDigitoUpdate + CuartoDigitoUpdate;
            return !string.IsNullOrEmpty(PinUpdate) && PinUpdate.Length == 4;
        }

        internal bool SavePinCanExecute()
        {
            PinActual = PrimerDigito + SegundoDigito + TercerDigito + CuartoDigito;
            return !string.IsNullOrEmpty(PinActual) && PinActual.Length == 4;
        }
    }
}
