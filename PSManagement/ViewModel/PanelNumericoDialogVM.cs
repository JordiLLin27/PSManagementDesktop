using PSManagement.Model;

using System.ComponentModel;


namespace PSManagement.ViewModel
{
    class PanelNumericoDialogVM : INotifyPropertyChanged
    {
        public numeroscalzado Numeroscalzado { get; set; }
        public tallastextiles Tallastextiles { get; set; }
        public string TallaONum { get; set; }

        //Constructor
        public PanelNumericoDialogVM(object articulo, string tallaONumSeleccionado)
        {
            //Comprueba si el artículo hace referencia a sus tallas textiles o a sus números de calzado.
            if (articulo is tallastextiles art)
            {
                Tallastextiles = art;
            }
            else
                Numeroscalzado = (numeroscalzado)articulo;

            this.TallaONum = tallaONumSeleccionado;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //Establece la cantidad a las tallas o números actuales.
        internal bool SetTallasONumeros(int cantidad)
        {
            if (Tallastextiles != null)
                return Tallastextiles.SetTallas("Talla " + TallaONum, cantidad);
            else
                return Numeroscalzado.SetNumeros(TallaONum, cantidad);
        }

        //Obtiene la cantidad en stock de tallas o números del artículo actual.
        internal int GetCantidadNumTalla()
        {
            if (Numeroscalzado != null)
                return Numeroscalzado.GetCantidadNumero(TallaONum);
            else
                return Tallastextiles.GetCantidadTalla("Talla " + TallaONum);
        }
    }
}
