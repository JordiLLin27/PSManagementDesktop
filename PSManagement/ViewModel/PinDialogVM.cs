using System;
using System.ComponentModel;


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

        /// <summary>
        /// Función que comprueba el pinactual con el almacenado en las propiedades.
        /// </summary>
        /// <returns>Verdadero si el pin coincide, falso en cualquier otro caso</returns>
        public bool CompruebaPIN()
        {
            if (!string.IsNullOrEmpty(PinActual) && string.Equals(PinActual, Properties.Settings.Default.PinSeguridad))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Actualiza el pin actual y lo guarda en las propiedades de la aplicación.
        /// </summary>
        /// <returns>Verdadero si se ha guardadado la información, falso en otro caso</returns>
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


        //Determina si se puede ejecutar el comando para actualizar el pin actual
        internal bool PINUpdateCanExecute()
        {
            PinUpdate = PrimerDigitoUpdate + SegundoDigitoUpdate + TercerDigitoUpdate + CuartoDigitoUpdate;
            return !string.IsNullOrEmpty(PinUpdate) && PinUpdate.Length == 4;
        }

        //Determina si se puede ejecutar el comando para introducir el pin actual
        internal bool SavePinCanExecute()
        {
            PinActual = PrimerDigito + SegundoDigito + TercerDigito + CuartoDigito;
            return !string.IsNullOrEmpty(PinActual) && PinActual.Length == 4;
        }
    }
}
