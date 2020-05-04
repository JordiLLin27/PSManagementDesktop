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
            try
            {
                DialogResult = (DataContext as PanelNumericoDialogVM).SetTallasONumeros(int.Parse(ResultadoTextBlock.Text));
            }
            catch (OverflowException)
            {
                MessageBox.Show("Has intentado introducir un número muy grande", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void BackSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            BackSpace();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            string teclaPulsada = "";
            if (ResultadoTextBlock.Text == "0" && e.Key != Key.Back)
                ResultadoTextBlock.Text = "";
            switch (e.Key)
            {

                case Key.D0:
                case Key.NumPad0:
                    teclaPulsada = "0";
                    if (ResultadoTextBlock.Text == "0")
                        e.Handled = true;
                    break;
                case Key.D1:
                case Key.NumPad1:
                    teclaPulsada = "1";
                    break;
                case Key.D2:
                case Key.NumPad2:
                    teclaPulsada = "2";
                    break;
                case Key.D3:
                case Key.NumPad3:
                    teclaPulsada = "3";
                    break;
                case Key.D4:
                case Key.NumPad4:
                    teclaPulsada = "4";
                    break;
                case Key.D5:
                case Key.NumPad5:
                    teclaPulsada = "5";
                    break;
                case Key.D6:
                case Key.NumPad6:
                    teclaPulsada = "6";
                    break;
                case Key.D7:
                case Key.NumPad7:
                    teclaPulsada = "7";
                    break;
                case Key.D8:
                case Key.NumPad8:
                    teclaPulsada = "8";
                    break;
                case Key.D9:
                case Key.NumPad9:
                    teclaPulsada = "9";
                    break;

                case Key.Back:
                    BackSpace();
                    break;

                default:
                    e.Handled = true;
                    break;
            }



            if (!string.IsNullOrEmpty(teclaPulsada))
                ResultadoTextBlock.Text += teclaPulsada;
        }

        private void BackSpace()
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
