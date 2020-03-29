using PSManagement.Dialogs;
using PSManagement.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PSManagement.ViewModel
{
    class PanelPrincipalVM
    {
        public UIElement PanelActual { get; set; }


        public PanelPrincipalVM()
        {
            CargaPanelBienvenida();
        }

        public PanelIntroduccionView CargaPanelBienvenida()
        {

            PanelIntroduccionView panelIntro = new PanelIntroduccionView()
            {
                Margin = new Thickness(10)
            };

            PanelActual = panelIntro;

            return panelIntro;
        }


        public PanelOpcionesVIew CargaPanelOpciones()
        {

            PanelOpcionesVIew panelOpt = new PanelOpcionesVIew();

            PanelActual = panelOpt;

            return panelOpt;
        }

        public PanelInventariosView CargaPanelInventarios()
        {
            PanelInventariosView panelInv = new PanelInventariosView();

            PanelActual = panelInv;

            return panelInv;
        }

        public void ExitDialog(ExitDialog ventanaSalir)
        {

            if (ventanaSalir.ShowDialog() == true)
            {
                Application.Current.Shutdown();
            }
        }

        internal UIElement BotonHome()
        {
            return CargaPanelBienvenida();
        }
    }
}
