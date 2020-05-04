using PSManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSManagement.ViewModel
{
    class PanelNumericoDialogVM : INotifyPropertyChanged
    {
        public numeroscalzado Numeroscalzado { get; set; }
        public tallastextiles Tallastextiles { get; set; }
        public string TallaONum { get; set; }

        public PanelNumericoDialogVM(object articulo, string tallaONumSeleccionado)
        {
            if (articulo is tallastextiles)
                Tallastextiles = (tallastextiles)articulo;
            else
                Numeroscalzado = (numeroscalzado)articulo;

            this.TallaONum = tallaONumSeleccionado;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal bool SetTallasONumeros(int cantidad)
        {
            if (Tallastextiles != null)
                return Tallastextiles.SetTallas(TallaONum, cantidad);
            else
                return Numeroscalzado.SetNumeros(TallaONum, cantidad);
        }

        internal int GetCantidadNumTalla()
        {
            if (Numeroscalzado != null)
                return Numeroscalzado.GetCantidadNumero(TallaONum);
            else
                return Tallastextiles.GetCantidadTalla(TallaONum);
        }
    }
}
