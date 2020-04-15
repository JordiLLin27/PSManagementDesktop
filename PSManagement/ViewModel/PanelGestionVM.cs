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
        public ObservableCollection<tallastextiles> ListaTallasArticulos { get; set; }
        public ObservableCollection<numeroscalzado> ListaNumerosArticulos { get; set; }
        public ObservableCollection<inventarios> ListaInventarios { get; set; }

        public inventarios InventarioSeleccionado { get; set; }

        public PanelGestionVM()
        {
            ListaNumerosArticulos = BBDDService.GetNumerosCalzado();
            ListaTallasArticulos = BBDDService.GetTallasTextiles();
            ListaInventarios = BBDDService.GetInventarios();
        }

        


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
