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

        public PanelGestionVM()
        {
            ListaTallasArticulos = new CollectionViewSource() { Source = BBDDService.GetNumerosCalzado(), IsLiveFilteringRequested = true };
            ListaTallasArticulos = new CollectionViewSource() { Source = BBDDService.GetTallasTextiles(), IsLiveFilteringRequested = true };
            ListaInventarios = BBDDService.GetInventarios();
        }




        public event PropertyChangedEventHandler PropertyChanged;
    }
}
