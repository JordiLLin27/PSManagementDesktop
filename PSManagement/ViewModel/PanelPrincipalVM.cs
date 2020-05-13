using PSManagement.Dialogs;
using PSManagement.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PSManagement.ViewModel
{
    class PanelPrincipalVM : INotifyPropertyChanged
    {
        public UIElement PanelActual { get; set; }


        public PanelPrincipalVM()
        {
            CargaPanelBienvenida();
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        public PanelGestionView CargaPanelGestion()
        {
            PanelGestionView panelGest = new PanelGestionView();

            PanelActual = panelGest;

            return panelGest;
        }

        public PanelVentasView CargaPanelVentas()
        {
            PanelVentasView panelVentas = new PanelVentasView();

            PanelActual = panelVentas;

            return panelVentas;
        }

        public void ExitDialog(ExitDialog ventanaSalir)
        {

            if (ventanaSalir.ShowDialog() == true)
            {
                Application.Current.Shutdown();
            }
        }

        internal PanelIntroduccionView BotonHome()
        {
            return CargaPanelBienvenida();
        }

        internal bool DiscountExecuted(DiscountAction action)
        {
            FormularioDescuentosDialog formularioDescuentos = new FormularioDescuentosDialog(action)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ShowInTaskbar = false
            };

            return formularioDescuentos.ShowDialog() == true ? true : false;
        }
    }
}
