using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<NodeViewModel> Nodes { get; set; }

        public MainWindowViewModel()
        {
            Nodes = new ObservableCollection<NodeViewModel>();

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
        }

        public void LoadFrom(string path)
        {

        }
    }
}
