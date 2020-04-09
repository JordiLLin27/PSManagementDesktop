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
    class PanelGestionVM : INotifyPropertyChanged
    {
        public ObservableCollection<cantidadportallas> ListaTallasArticulos { get; set; }
        public ObservableCollection<cantidadpornumeros> ListaNumerosArticulos { get; set; }
        public ObservableCollection<inventarios> ListaInventarios { get; set; }

        public inventarios InventarioSeleccionado { get; set; }

        public PanelGestionVM()
        {
            ListaNumerosArticulos = BBDDService.GetCantidadpornumeros();
            ListaTallasArticulos = BBDDService.GetCantidadportallas();
            ListaInventarios = BBDDService.GetInventarios();
        }

        


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
