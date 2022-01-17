﻿using Paint.Adorner;
using Paint.Lib;
using Paint.ViewModels;
using PluginContract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paint.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<NodeViewModel> Nodes { get; set; }

        public MainWindow(PluginManager pluginManager)
        {
            InitializeComponent();
            DataContext = this;

            Nodes = new ObservableCollection<NodeViewModel>();
            NodesControl.ItemsSource = Nodes;

          
        }

        private static int count = 0;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nodes.Add(new ShapeNodeViewModel
            {
                Width = 100,
                Height = 100,
                Fill = Brushes.Black,
                Left = 0,
                Top = 0,
                RotateAngle = 10,
                DefiningShape = new RectangleGeometry { Rect = new Rect(0, 0, 1, 1) }
            });

            count++;

            var node = Nodes[0];

            node.Left += 500;
            node.Top += 500;
        }

        public void LoadFrom(string path)
        {

        }
    }
}
