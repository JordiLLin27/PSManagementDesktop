using PSManagement.Model;
using PSManagement.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                ListaDescuentos = BBDDService.GetDescuentos();
            }
            else
            {
                Accion = "eliminar";
                ListaDescuentos = BBDDService.GetDescuentos();
            }
        }

        internal bool SaveCanExecute()
        {
            if (Action == DiscountAction.Add_Discount)
                return !string.IsNullOrEmpty(DescuentoSeleccionado.NombreDescuento) && DescuentoSeleccionado.DescuentoAplicar >= 0 && DescuentoSeleccionado.UnidadesCondicionAplicar >= 0 && !string.IsNullOrEmpty(DescuentoSeleccionado.TipoCondicion);
            else
                return !(DescuentoSeleccionado is null);

        }

        internal int SaveExecuted()
        {
            try
            {
                if (Action == DiscountAction.Add_Discount)
                    return BBDDService.AddDescuento(DescuentoSeleccionado);
                else if (Action == DiscountAction.Update_Discount)
                    return BBDDService.SaveChanges();
                else
                    return BBDDService.DeleteDescuentos(DescuentoSeleccionado);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        internal void RevertChanges()
        {
            BBDDService.RevertChanges();
        }

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
