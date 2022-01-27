using Paint.Adorner;
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
    public partial class MainWindow : RibbonWindow
    {
        private PluginManager _pluginManager { get; set; }

        public ObservableCollection<NodeViewModel> Nodes { get; set; }

        public List<List<NodeViewModel>> NodesList { get; set; }

        public MainWindow(PluginManager pluginManager)
        {
            InitializeComponent();
            Nodes = new ObservableCollection<NodeViewModel>();

            _pluginManager = pluginManager;

            NodesControl.ItemsSource = Nodes;

            //for (int i = 0; i < 10; i++)
            //{
            //    Nodes.Add(new ShapeNodeViewModel
            //    {
            //        DefiningShape = new RectangleGeometry(new Rect(0, 0, 1, 1)),
            //        Width = 100,
            //        Height = 100,
            //        Top = i * 50,
            //        Left = i * 50,
            //        Fill = new SolidColorBrush(Color.FromArgb(255, 255, (byte)(i * 10), (byte)(i * 10))),
            //        ZIndex = 10 - i
            //    });
            //}
            NodeViewModel model = new ShapeNodeViewModel
            {
                DefiningShape = new RectangleGeometry(new Rect(0, 0, 1, 1)),
                Fill = new SolidColorBrush(Color.FromArgb(255, 255, (byte)(10 * 10), (byte)(10 * 10))),
                ZIndex = 10,
                Width = 100,
                Height = 100,
            };

            Nodes.Add(model);
        }
    }
}
