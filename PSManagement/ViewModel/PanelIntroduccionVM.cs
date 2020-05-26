
using System.ComponentModel;

using System.Windows.Forms;

namespace PSManagement.ViewModel
{
    class PanelIntroduccionVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PanelIntroduccionVM()
        {
        }

        //Abre el manual de ayuda por el apartado de Inventarios
        internal void HelpInventarios()
        {
            Help.ShowHelp(null, System.IO.Directory.GetCurrentDirectory().ToString() + @"\Manual\ManualPSManagement.chm", HelpNavigator.KeywordIndex, "Inventarios");
        }

        //Abre el manual de ayuda por el apartado de Gestión
        internal void HelpGestion()
        {
            Help.ShowHelp(null, System.IO.Directory.GetCurrentDirectory().ToString() + @"\Manual\ManualPSManagement.chm", HelpNavigator.KeywordIndex, "Gestion");
        }

        //Abre el manual de ayuda por el apartado de Ventas
        internal void HelpVentas()
        {
            Help.ShowHelp(null, System.IO.Directory.GetCurrentDirectory().ToString() + @"\Manual\ManualPSManagement.chm", HelpNavigator.KeywordIndex, "Ventas");
        }
    }
}
