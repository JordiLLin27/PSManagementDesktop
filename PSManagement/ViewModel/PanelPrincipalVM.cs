using PSManagement.Dialogs;
using PSManagement.View;
using System;
using System.ComponentModel;

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

        //Cambia el panel actual por el panel de bienvenida.
        public PanelIntroduccionView CargaPanelBienvenida()
        {

            PanelIntroduccionView panelIntro = new PanelIntroduccionView()
            {
                Margin = new Thickness(10)
            };

            PanelActual = panelIntro;

            return panelIntro;
        }

        //Cambia el panel actual por el panel de configuración.
        public PanelOpcionesVIew CargaPanelOpciones()
        {

            PanelOpcionesVIew panelOpt = new PanelOpcionesVIew();

            PanelActual = panelOpt;

            return panelOpt;
        }

        //Cambia el panel actual por el panel de inventarios.
        public PanelInventariosView CargaPanelInventarios()
        {
            PanelInventariosView panelInv = new PanelInventariosView();

            PanelActual = panelInv;

            return panelInv;
        }

        //Cambia el panel actual por el panel de gestión.
        public PanelGestionView CargaPanelGestion()
        {
            PanelGestionView panelGest = new PanelGestionView();

            PanelActual = panelGest;

            return panelGest;
        }

        //Cambia el panel actual por el panel de ventas.
        public PanelVentasView CargaPanelVentas()
        {
            PanelVentasView panelVentas = new PanelVentasView();

            PanelActual = panelVentas;

            return panelVentas;
        }

        //Abre el manual de ayuda al usuario
        internal void AbreManual()
        {
            string ruta = System.IO.Directory.GetCurrentDirectory().ToString() + @"\Manual\ManualPSManagement.chm";
            System.Diagnostics.Process.Start(ruta);
        }

        //Muestra el diálogo para salir de la aplicación
        public void ExitDialog(ExitDialog ventanaSalir)
        {

            if (ventanaSalir.ShowDialog() == true)
            {
                Application.Current.Shutdown();
            }
        }

        //Vuelve al panel de bienvenida.
        internal PanelIntroduccionView BotonHome()
        {
            return CargaPanelBienvenida();
        }

        //Muestra el formulario para mantenimiento de descuento.
        internal bool DiscountExecuted(DiscountAction action)
        {
            FormularioDescuentosDialog formularioDescuentos = new FormularioDescuentosDialog(action)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ShowInTaskbar = false
            };

            return formularioDescuentos.ShowDialog() == true;
        }
    }
}
