using PSManagement.Model;
using PSManagement.Service;
using System;

using System.Collections.ObjectModel;
using System.ComponentModel;


namespace PSManagement.ViewModel
{
    class FormularioDescuentoVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<descuentos> ListaDescuentos { get; set; }
        public descuentos DescuentoSeleccionado { get; set; }

        public string Accion { get; set; }
        public bool CondicionAPartirDe { get; set; }
        public bool CondicionUDSExactas { get; set; }
        public DiscountAction Action { get; set; }

        //Constructor
        public FormularioDescuentoVM(DiscountAction action)
        {
            this.Action = action;
            CondicionAPartirDe = false;
            CondicionUDSExactas = false;
            if (Action == DiscountAction.Add_Discount)
            {
                DescuentoSeleccionado = new descuentos();
                Accion = "nuevo";

            }
            else if (Action == DiscountAction.Update_Discount)
            {
                Accion = "actualizar";
                ListaDescuentos = BbddService.GetDescuentos();
            }
            else
            {
                Accion = "eliminar";
                ListaDescuentos = BbddService.GetDescuentos();
            }
        }

        //Determina si se ejecuta el comando para realizar la acción de mantenimiento de descuentos según la misma.
        internal bool SaveCanExecute()
        {
            if (Action == DiscountAction.Add_Discount)
                return !string.IsNullOrEmpty(DescuentoSeleccionado.NombreDescuento) && DescuentoSeleccionado.DescuentoAplicar >= 0 && DescuentoSeleccionado.UnidadesCondicionAplicar >= 0 && !string.IsNullOrEmpty(DescuentoSeleccionado.TipoCondicion);
            else
                return !(DescuentoSeleccionado is null);

        }

        //Comando que realiza la acción a realizar en el mantenimiento de descuentos. Devuelve el número de cambios en la base de datos realizados.
        internal int SaveExecuted()
        {
            try
            {
                if (Action == DiscountAction.Add_Discount)
                    return BbddService.AddDescuento(DescuentoSeleccionado);
                else if (Action == DiscountAction.Update_Discount)
                    return BbddService.SaveChanges();
                else
                    return BbddService.DeleteDescuentos(DescuentoSeleccionado);
            }
            //-1 si se ha producido algún error.
            catch (Exception)
            {
                return -1;
            }
        }

        //Deshace los cambios en la base de datos.
        internal void RevertChanges()
        {
            BbddService.RevertChanges();
        }

        //Cambia la condición que se va a exigir para aplicar el descuento. (si se está editando algún descuento o al insertar uno nuevo si se cambia de decisión)
        public void CambiaTipoCondicion()
        {
            if (!(DescuentoSeleccionado is null))
            {
                if (DescuentoSeleccionado.TipoCondicion == "UDExactas")
                {
                    CondicionUDSExactas = true;
                    CondicionAPartirDe = false;
                }
                else if (DescuentoSeleccionado.TipoCondicion == "APartiDeUD")
                {
                    CondicionAPartirDe = true;
                    CondicionUDSExactas = false;
                }
            }
        }

        //Establece la condición (al insertar un descuento)
        public void SetCondicion(int condicion)
        {
            if (condicion == 1)
            {
                DescuentoSeleccionado.TipoCondicion = "UDExactas";
            }
            else
            {
                DescuentoSeleccionado.TipoCondicion = "APartiDeUD";
            }
            CambiaTipoCondicion();
        }
    }
}
