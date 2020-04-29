using PSManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PSManagement.Dialogs
{
    /// <summary>
    /// Lógica de interacción para PanelNumericoDialog.xaml
    /// </summary>
    public partial class PanelNumericoDialog : Window
    {
        public PanelNumericoDialog(object articuloNumerosTallas, string talla)
        {
            this.DataContext = new PanelNumericoDialogVM(articuloNumerosTallas, talla);
            InitializeComponent();
            CreacionBotonesDinamicos();
            ResultadoTextBlock.Text = (DataContext as PanelNumericoDialogVM).GetCantidadNumTalla().ToString();
        }

        private void CreacionBotonesDinamicos()
        {
            for (int fila = 1; fila < 4; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    //Calculamos el número correspondiente a este botón
                    int n = (fila - 1) * 3 + columna + 1;

                    //Creamos y configuramos el botón
                    Button boton = new Button();
                    Grid.SetRow(boton, fila);
                    Grid.SetColumn(boton, columna);
                    boton.Margin = new Thickness(5);
                    boton.Height = 60;
                    boton.Click += Button_Click;
                    boton.Tag = n;

                    //Creamos y configuramos el contenido del botón
                    TextBlock texto = new TextBlock
                    {
                        Text = n.ToString()
                    };

                    Viewbox box = new Viewbox
                    {
                        Child = texto
                    };

                    boton.Content = box;

                    //Introducimos el botón en el Grid
                    PrincipalGrid.Children.Add(boton);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag.ToString() == "0" && ResultadoTextBlock.Text == "0")
                e.Handled = true;
            else if (((Button)sender).Tag.ToString() != "0" && ResultadoTextBlock.Text == "0")
                ResultadoTextBlock.Text = "";


            ResultadoTextBlock.Text += ((Button)sender).Tag.ToString();
        }

        private void IntroButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = (DataContext as PanelNumericoDialogVM).SetTallasONumeros(int.Parse(ResultadoTextBlock.Text));
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void BackSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultadoTextBlock.Text.Length == 1)
                ResultadoTextBlock.Text = "0";
            else
            {
                ResultadoTextBlock.Text = ResultadoTextBlock.Text.Substring(0, ResultadoTextBlock.Text.Length - 1);
            }
        }
    }
}
