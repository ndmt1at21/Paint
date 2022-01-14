using Paint.Adorner;
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

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public ObservableCollection<Node> Nodes = new ObservableCollection<Node>();

        public MainWindow()
        {
            InitializeComponent();
            NodesControl.ItemsSource = Nodes;

            Nodes.Add(new RectangleNode
            {
                Top = 10,
                Left = 100,
                Width = 100,
                Height = 100,
                Fill = Brushes.Black
            });

            Nodes.Add(new RectangleNode
            {
                Top = 50,
                Left = 500,
                Width = 100,
                Height = 150,
                Fill = Brushes.Black
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nodes[0] = new RectangleNode
            {
                Top = 50,
                Left = 100,
                Width = 100 + 200,
                Height = 150,
                Fill = Brushes.Black
            };
        }
    }
}
