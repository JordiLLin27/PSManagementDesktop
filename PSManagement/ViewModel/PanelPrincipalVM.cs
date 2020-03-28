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
    class PanelPrincipalVM
    {
        public UIElement PanelAnterior { get; set; }
        public UIElement PanelActual { get; set; }

        public PanelPrincipalVM()
        {
            PanelAnterior = new UIElement();
            PanelActual = new UIElement();
        }

        public PanelIntroduccionView CargaPanelBienvenida()
        {
            PanelIntroduccionView textoBienvenida = new PanelIntroduccionView
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

        public PanelInventariosView CargaPanelInventarios()
        {
            PanelActual = new PanelInventariosView();

            return (PanelInventariosView)PanelActual;
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
