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
        public Geometry DefiningShape { get; set; } = Geometry.Empty;

        public override NodeViewModel Clone()
        {
            return new ShapeNodeViewModel
            {
                Top = Top,
                Left = Left,
                Width = Width,
                Height = Height,
                ZIndex = ZIndex,
                TransformOrigin = TransformOrigin,
                Stroke = Stroke,
                Fill = Fill,
                StrokeDashArray = StrokeDashArray,
                StrokeDashOffset = StrokeDashOffset,
                IsSelected = IsSelected,
                IsDrawing = IsDrawing,
                IsDragging = IsDragging,
                RotateAngle = RotateAngle,
                StrokeMiterLimit = StrokeMiterLimit,
                StrokeThickness = StrokeThickness,
                DefiningShape = DefiningShape.Clone(),
            };
        }
    }
}
