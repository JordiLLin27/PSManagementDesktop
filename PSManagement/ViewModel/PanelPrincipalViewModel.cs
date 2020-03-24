using PSManagement.Dialogs;
using PSManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PSManagement.ViewModel
{
    class PanelPrincipalViewModel
    {
        public UIElement PanelAnterior { get; set; }
        public UIElement PanelActual { get; set; }

        public PanelPrincipalViewModel()
        {
            PanelAnterior = new UIElement();
            PanelActual = new UIElement();
        }

        public TextoBienvenidaPorDefectoView CargaPanelBienvenida()
        {
            TextoBienvenidaPorDefectoView textoBienvenida = new TextoBienvenidaPorDefectoView
            {
                Margin = new Thickness(10)
            };

            PanelAnterior = textoBienvenida;
            PanelActual = textoBienvenida;

            return textoBienvenida;
        }


        public PanelOpcionesVIew CargaPanelOpciones()
        {

            PanelActual = new PanelOpcionesVIew();

            return (PanelOpcionesVIew)PanelActual;
        }

        public void ExitDialog(ExitDialog ventanaSalir)
        {

            if (ventanaSalir.ShowDialog() == true)
            {
                Application.Current.Shutdown();
            }
        }

        public UIElement ComandoBotonAtrás()
        {
            PanelActual = PanelAnterior;
            return PanelActual;
        }
    }
}
