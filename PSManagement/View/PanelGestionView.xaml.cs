﻿using PSManagement.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PSManagement.View
{
    /// <summary>
    /// Lógica de interacción para PanelGestionView.xaml
    /// </summary>
    public partial class PanelGestionView : UserControl
    {
        public PanelGestionView()
        {
            this.DataContext = new PanelGestionVM();
            InitializeComponent();
        }

       
    }
}
