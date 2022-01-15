using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint
{
    public abstract class Parent : INotifyPropertyChanged
    {
        public abstract string Name { get; set; }
        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; } = 0;
        public double Height { get; set; } = 0;

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public abstract class Yeah : Parent
    {
        override public string Name { get; set; } = "ABC";
        public abstract Geometry DefiningShape { get; }
    }

    public class Test : Yeah
    {
        public override Geometry DefiningShape => new RectangleGeometry
        {
            Rect = new Rect(0, 0, Width, Height)
        };
        public SolidColorBrush Fill { get; set; } = Brushes.Black;

    }
}
