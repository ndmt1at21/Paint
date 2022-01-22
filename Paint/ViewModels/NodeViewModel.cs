using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint.ViewModels
{
    public class NodeViewModel : ViewModelBase
    {
        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double RotateAngle { get; set; }
        public Point TransformOrigin { get; set; } = new Point(0.5, 0.5);

        public bool IsSelected { get; set; }
    }
}
