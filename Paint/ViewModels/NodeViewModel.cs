using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint.ViewModels
{
    public class NodeViewModel : ViewModelBase
    {
        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int ZIndex { get; set; }
        public double RotateAngle { get; set; }
        public Point TransformOrigin { get; set; } = new Point(0.5, 0.5);

        public Brush Stroke { get; set; }
        public Brush Fill { get; set; }

        public DoubleCollection StrokeDashArray { get; set; }

        public PenLineCap StrokeDashCap { get; set; }
        public PenLineCap StrokeStartLineCap { get; set; }
        public PenLineCap StrokeEndLineCap { get; set; }
        public PenLineJoin StrokeLineJoin { get; set; }

        public double StrokeDashOffset { get; set; }
        public double StrokeMiterLimit { get; set; }
        public double StrokeThickness { get; set; }

        public bool IsSelected { get; set; }
        public bool IsDrawing { get; set; }
        public bool IsDragging { get; set; }

        public NodeViewModel Clone()
        {
            return Utils.Object.DeepClone(this);
        }
    }
}
