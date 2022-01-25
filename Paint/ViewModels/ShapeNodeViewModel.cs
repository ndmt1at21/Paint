using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint.ViewModels
{
    public class ShapeNodeViewModel : NodeViewModel
    {
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

        public Geometry DefiningShape { get; set; } = Geometry.Empty;
    }
}
