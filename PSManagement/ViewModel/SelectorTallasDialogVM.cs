using PSManagement.Model;
using PSManagement.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


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

            TallasArticuloSeleccionado = BbddService.GetArticuloTallasTextiles(ArticuloSeleccionado);
            NumerosArticuloSeleccionado = BbddService.GetArticuloNumerosCalzado(ArticuloSeleccionado);
        }

        //Determina si el comando para seleccionar talla o número se puede ejecutar
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
            if (tallaONum.Contains("Talla") && !(TallasArticuloSeleccionado is null))
            {
                TallasArticuloSeleccionado.RestaTalla(tallaONum);
                AddItemALista(tallaONum);
            }
            else if (tallaONum.Contains("Número") && !(NumerosArticuloSeleccionado is null))
            {
                NumerosArticuloSeleccionado.RestaNumero(tallaONum);
                AddItemALista(tallaONum);
            }
        }

        //Añade un registro a los detalles de la factura
        private void AddItemALista(string tallaONum)
        {
            //Si ya existe, incrementa las unidades de ese registro
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

        /// <summary>
        /// Función que busca detalles de una factura de un artículo en concreto y una talla en concreto.
        /// </summary>
        /// <param name="articuloSeleccionado">Artículo a buscar en los detalles</param>
        /// <param name="tallaONum">Talla del artículo</param>
        /// <returns>Detalle de la factura de ese artículo si existe, null en cualquier otro caso</returns>
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
