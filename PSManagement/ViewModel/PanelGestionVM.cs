using PSManagement.Dialogs;
using PSManagement.Model;
using PSManagement.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PSManagement.ViewModel
{
    class PanelGestionVM : INotifyPropertyChanged
    {
        public CollectionViewSource ListaTallasArticulos { get; set; }
        public CollectionViewSource ListaNumerosArticulos { get; set; }
        public ObservableCollection<inventarios> ListaInventarios { get; set; }

        public inventarios InventarioSeleccionado { get; set; }

        public numeroscalzado NumerosArticuloSeleccionado { get; set; }
        public tallastextiles TallasArticuloSeleccionado { get; set; }

        public string TallaActualSeleccionada { get; set; }
        public string NumeroCalzadoActualSeleccionado { get; set; }


        public string FiltroTextBox { get; set; }

        public PanelGestionVM()
        {
            ListaNumerosArticulos = new CollectionViewSource() { Source = BBDDService.GetNumerosCalzado(), IsLiveFilteringRequested = true };
            ListaTallasArticulos = new CollectionViewSource() { Source = BBDDService.GetTallasTextiles(), IsLiveFilteringRequested = true };
            ListaInventarios = BBDDService.GetInventarios();

            ListaNumerosArticulos.Filter += FilterTablaNumeros;
            ListaTallasArticulos.Filter += FilterTablaTallas;
        }

        internal void CleanFIlters()
        {
            InventarioSeleccionado = null;
            FiltroTextBox = "";
            FindExecuted();
        }

        private void FilterTablaTallas(object sender, FilterEventArgs e)
        {
            tallastextiles articulo = (tallastextiles)e.Item;

            if (string.IsNullOrEmpty(FiltroTextBox) && InventarioSeleccionado == null)
            {
                e.Accepted = true;
            }
            else
            {
                if (string.IsNullOrEmpty(FiltroTextBox) && InventarioSeleccionado != null)
                {
                    if (articulo.ARTICULO.INVENTARIO.Equals(InventarioSeleccionado))
                    {
                        e.Accepted = true;
                    }
                    else
                        e.Accepted = false;
                }
                else if (!string.IsNullOrEmpty(FiltroTextBox) && InventarioSeleccionado == null)
                {
                    if (articulo.ARTICULO.Nombre.ToLower().Contains(FiltroTextBox.ToLower()) || articulo.ARTICULO.CodArticulo.ToLower().Contains(FiltroTextBox.ToLower()))
                    {
                        e.Accepted = true;
                    }
                    else
                        e.Accepted = false;
                }
                else if ((!string.IsNullOrEmpty(FiltroTextBox) && InventarioSeleccionado != null))
                {
                    if (articulo.ARTICULO.INVENTARIO.Equals(InventarioSeleccionado) && (articulo.ARTICULO.Nombre.ToLower().Contains(FiltroTextBox.ToLower()) || articulo.CodArticulo.ToLower().Contains(FiltroTextBox.ToLower())))
                    {
                        e.Accepted = true;
                    }
                    else
                        e.Accepted = false;
                }

            }
        }

        private void FilterTablaNumeros(object sender, FilterEventArgs e)
        {
            numeroscalzado articulo = (numeroscalzado)e.Item;

            if (string.IsNullOrEmpty(FiltroTextBox) && InventarioSeleccionado == null)
            {
                e.Accepted = true;
            }
            else
            {
                if (string.IsNullOrEmpty(FiltroTextBox) && InventarioSeleccionado != null)
                {
                    if (articulo.ARTICULO.INVENTARIO.Equals(InventarioSeleccionado))
                    {
                        e.Accepted = true;
                    }
                    else
                        e.Accepted = false;
                }
                else if (!string.IsNullOrEmpty(FiltroTextBox) && InventarioSeleccionado == null)
                {
                    if (articulo.ARTICULO.Nombre.ToLower().Contains(FiltroTextBox.ToLower()) || articulo.ARTICULO.CodArticulo.ToLower().Contains(FiltroTextBox.ToLower()))
                    {
                        e.Accepted = true;
                    }
                    else
                        e.Accepted = false;
                }
                else if ((!string.IsNullOrEmpty(FiltroTextBox) && InventarioSeleccionado != null))
                {
                    if (articulo.ARTICULO.INVENTARIO.Equals(InventarioSeleccionado) && (articulo.ARTICULO.Nombre.ToLower().Contains(FiltroTextBox.ToLower()) || articulo.CodArticulo.ToLower().Contains(FiltroTextBox.ToLower())))
                    {
                        e.Accepted = true;
                    }
                    else
                        e.Accepted = false;
                }

            }
        }

        internal bool DataGridCell_MouseDoubleClick(object articulo, string talla)
        {
            PanelNumericoDialog numPad = new PanelNumericoDialog(articulo, talla) { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen };
            return (numPad.ShowDialog() == true ? true : false);
        }

        internal void UnDoChangesExecuted()
        {
            BBDDService.RevertChanges();
        }

        internal bool FindCanExecute()
        {
            return (!string.IsNullOrEmpty(FiltroTextBox) || InventarioSeleccionado != null);
        }

        internal bool CleanFilterCanExecuted()
        {
            return (!string.IsNullOrEmpty(FiltroTextBox) || InventarioSeleccionado != null);
        }

        internal void FindExecuted()
        {
            ListaNumerosArticulos.View.Refresh();
            ListaTallasArticulos.View.Refresh();
        }

        internal int GuardarStock()
        {
            return BBDDService.SaveChanges();
        }

        internal bool PidePIN()
        {
            PinDialog pinDialog = new PinDialog(PinConfig.Insert_Pin) { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen };

            return pinDialog.ShowDialog() == true ? true : false;

        }

        #region TALLAS
        internal void ActualizaItemTallasSeleccionado(object item)
        {
            TallasArticuloSeleccionado = (tallastextiles)item;
        }

        internal void ActualizaTallaSeleccionada(string nuevaTallaSeleccionada)
        {
            TallaActualSeleccionada = nuevaTallaSeleccionada;
        }

        internal bool CompruebaStockMinimo()
        {
            if (TallasArticuloSeleccionado != null)
            {
                int tallaComprobar = -1;

                switch (TallaActualSeleccionada)
                {
                    case "XXS":
                        tallaComprobar = TallasArticuloSeleccionado.XXS;
                        break;

                    case "XS":
                        tallaComprobar = TallasArticuloSeleccionado.XS;
                        break;

                    case "S":
                        tallaComprobar = TallasArticuloSeleccionado.S;
                        break;

                    case "M":
                        tallaComprobar = TallasArticuloSeleccionado.M;
                        break;

                    case "L":
                        tallaComprobar = TallasArticuloSeleccionado.L;
                        break;

                    case "XL":
                        tallaComprobar = TallasArticuloSeleccionado.XL;
                        break;

                    case "XXL":
                        tallaComprobar = TallasArticuloSeleccionado.XXL;
                        break;

                    default:
                        break;
                }

                return (tallaComprobar < TallasArticuloSeleccionado.ARTICULO.StockMinimo);
            }
            return false;
        }

        internal bool AumentaTalla()
        {
            if (TallasArticuloSeleccionado != null)
            {
                switch (TallaActualSeleccionada)
                {
                    case "XXS":
                        TallasArticuloSeleccionado.XXS++;
                        break;

                    case "XS":
                        TallasArticuloSeleccionado.XS++;
                        break;

                    case "S":
                        TallasArticuloSeleccionado.S++;
                        break;

                    case "M":
                        TallasArticuloSeleccionado.M++;
                        break;

                    case "L":
                        TallasArticuloSeleccionado.L++;
                        break;

                    case "XL":
                        TallasArticuloSeleccionado.XL++;
                        break;

                    case "XXL":
                        TallasArticuloSeleccionado.XXL++;
                        break;

                    default:
                        break;
                }

                ActualizaTotalTallasItemSeleccionado();
                return CompruebaStockMinimo();
            }
            return false;
        }

        internal bool DisminuyeTalla()
        {
            if (TallasArticuloSeleccionado != null)
            {
                switch (TallaActualSeleccionada)
                {
                    case "XXS":
                        TallasArticuloSeleccionado.XXS--;
                        break;

                    case "XS":
                        TallasArticuloSeleccionado.XS--;
                        break;

                    case "S":
                        TallasArticuloSeleccionado.S--;
                        break;

                    case "M":
                        TallasArticuloSeleccionado.M--;
                        break;

                    case "L":
                        TallasArticuloSeleccionado.L--;
                        break;

                    case "XL":
                        TallasArticuloSeleccionado.XL--;
                        break;

                    case "XXL":
                        TallasArticuloSeleccionado.XXL--;
                        break;

                    default:
                        break;
                }
                ActualizaTotalTallasItemSeleccionado();
                return CompruebaStockMinimo();
            }
            return false;
        }

        private void ActualizaTotalTallasItemSeleccionado()
        {
            TallasArticuloSeleccionado.TotalCantidadArticulo = TallasArticuloSeleccionado.GetTotalItems();
        }
        #endregion TALLAS

        #region NUMEROS
        internal void ActualizaItemNumerosSeleccionado(object item)
        {
            NumerosArticuloSeleccionado = (numeroscalzado)item;
        }

        internal void ActualizaNumeroSeleccionado(string nuevoNumeroCalzadoSeleccionado)
        {
            NumeroCalzadoActualSeleccionado = nuevoNumeroCalzadoSeleccionado;
        }

        internal void AumentaNumero()
        {
            if (NumerosArticuloSeleccionado != null)
            {
                switch (NumeroCalzadoActualSeleccionado)
                {
                    case "Número 36":
                        NumerosArticuloSeleccionado.C36++;
                        break;

                    case "Número 37":
                        NumerosArticuloSeleccionado.C37++;
                        break;

                    case "Número 38":
                        NumerosArticuloSeleccionado.C38++;
                        break;

                    case "Número 39":
                        NumerosArticuloSeleccionado.C39++;
                        break;

                    case "Número 40":
                        NumerosArticuloSeleccionado.C40++;
                        break;

                    case "Número 41":
                        NumerosArticuloSeleccionado.C41++;
                        break;

                    case "Número 42":
                        NumerosArticuloSeleccionado.C42++;
                        break;

                    case "Número 43":
                        NumerosArticuloSeleccionado.C43++;
                        break;

                    case "Número 44":
                        NumerosArticuloSeleccionado.C44++;
                        break;

                    case "Número 45":
                        NumerosArticuloSeleccionado.C45++;
                        break;

                    case "Número 46":
                        NumerosArticuloSeleccionado.C46++;
                        break;

                    case "Número 47":
                        NumerosArticuloSeleccionado.C47++;
                        break;

                    default:
                        break;
                }
                ActualizarTotalNumerosItemSeleccionado();
            }
        }

        internal void DisminuyeNumero()
        {
            if (NumerosArticuloSeleccionado != null)
            {
                switch (NumeroCalzadoActualSeleccionado)
                {
                    case "Número 36":
                        NumerosArticuloSeleccionado.C36--;
                        break;

                    case "Número 37":
                        NumerosArticuloSeleccionado.C37--;
                        break;

                    case "Número 38":
                        NumerosArticuloSeleccionado.C38--;
                        break;

                    case "Número 39":
                        NumerosArticuloSeleccionado.C39--;
                        break;

                    case "Número 40":
                        NumerosArticuloSeleccionado.C40--;
                        break;

                    case "Número 41":
                        NumerosArticuloSeleccionado.C41--;
                        break;

                    case "Número 42":
                        NumerosArticuloSeleccionado.C42--;
                        break;

                    case "Número 43":
                        NumerosArticuloSeleccionado.C43--;
                        break;

                    case "Número 44":
                        NumerosArticuloSeleccionado.C44--;
                        break;

                    case "Número 45":
                        NumerosArticuloSeleccionado.C45--;
                        break;

                    case "Número 46":
                        NumerosArticuloSeleccionado.C46--;
                        break;

                    case "Número 47":
                        NumerosArticuloSeleccionado.C47--;
                        break;

                    default:
                        break;
                }
                ActualizarTotalNumerosItemSeleccionado();
            }
        }

        private void ActualizarTotalNumerosItemSeleccionado()
        {
            this.NumerosArticuloSeleccionado.TotalCantidadArticulo = this.NumerosArticuloSeleccionado.GetTotalItems();
        }
        #endregion NUMEROS

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
