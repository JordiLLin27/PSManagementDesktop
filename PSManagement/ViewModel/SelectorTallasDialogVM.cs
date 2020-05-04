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
    class SelectorTallasDialogVM : INotifyPropertyChanged
    {
        public articulos ArticuloSeleccionado { get; set; }
        public ObservableCollection<detallesfactura> DetallesArticuloEnFactura { get; set; }
        public tallastextiles TallasArticuloSeleccionado { get; set; }
        public numeroscalzado NumerosArticuloSeleccionado { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public SelectorTallasDialogVM(articulos articulo, ObservableCollection<detallesfactura> ListaDetallesArticulo)
        {
            ArticuloSeleccionado = articulo;
            DetallesArticuloEnFactura = ListaDetallesArticulo;

            TallasArticuloSeleccionado = BBDDService.GetArticuloTallasTextiles(ArticuloSeleccionado);
            NumerosArticuloSeleccionado = BBDDService.GetArticuloNumerosCalzado(ArticuloSeleccionado);
        }

        internal bool SelectSizeCanExecute(string tag)
        {
            if (tag.Contains("Talla"))
            {
                if (TallasArticuloSeleccionado is null)
                    return false;
                else
                    return (TallasArticuloSeleccionado.GetCantidadTalla(tag) > 0);
            }
            else if (tag.Contains("Número"))
            {
                if (NumerosArticuloSeleccionado is null)
                    return false;
                else
                    return (NumerosArticuloSeleccionado.GetCantidadNumero(tag) > 0);
            }
            else return false;
        }

        internal bool TieneTallas()
        {
            return !(TallasArticuloSeleccionado is null);
        }

        internal bool TieneNumeros()
        {
            return !(NumerosArticuloSeleccionado is null);
        }

        internal void SelectSizeExecuted(string tallaONum)
        {
            if (tallaONum.Contains("Talla"))
            {
                if (!(TallasArticuloSeleccionado is null))
                {
                    TallasArticuloSeleccionado.RestaTalla(tallaONum);
                    AddItemALista(tallaONum);
                }
            }
            else if (tallaONum.Contains("Número"))
            {
                if (!(NumerosArticuloSeleccionado is null))
                {
                    NumerosArticuloSeleccionado.RestaNumero(tallaONum);
                    AddItemALista(tallaONum);
                }
            }
        }

        private void AddItemALista(string tallaONum)
        {
            detallesfactura detallesItem = ExisteArticuloEnLista(ArticuloSeleccionado, tallaONum);
            if (detallesItem != null)
            {
                detallesItem.CantidadArticulo++;
            }
            else
            {
                detallesfactura detallesArt = new detallesfactura()
                {
                    ARTICULO = ArticuloSeleccionado,
                    CodArticulo = ArticuloSeleccionado.CodArticulo,
                    TallaONum = tallaONum,
                    CantidadArticulo = 1
                };

                DetallesArticuloEnFactura.Add(detallesArt);
            }
        }

        private detallesfactura ExisteArticuloEnLista(articulos articuloSeleccionado, string tallaONum)
        {
            foreach (detallesfactura item in DetallesArticuloEnFactura)
            {
                if (item.ARTICULO.CodArticulo == articuloSeleccionado.CodArticulo && string.Equals(tallaONum, item.TallaONum))
                {
                    return item;
                }
            }
            return null;
        }
    }
}
