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
                DefiningShape = Geometry.Parse("M76.5 0L113.883 27.5173L152.109 53.8967L136.987 97.2827L123.229 141.103L76.5 140.4L29.7711 141.103L16.0128 97.2827L0.891006 53.8967L39.1169 27.5173L76.5 0Z")
            });
        }

        public void LoadFrom(string path)
        {

        }
    }
}
