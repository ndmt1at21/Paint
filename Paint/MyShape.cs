using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint
{
    public class MyShape : UIElement
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double RadiusX { get; set; }
        public double RadiusY { get; set; }
        public SolidColorBrush Background { get; set; }

        public UIElement RenderObject => new System.Windows.Shapes.Rectangle()
        {
            Width = Width,
            Height = Height,
            RadiusX = RadiusX,
            RadiusY = RadiusY,
            Fill = Background,
        };

        public void ChangePos(double x, double y)
        {
            X = x;
            Y = y;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
