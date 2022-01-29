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
        public string DefiningShape { get; set; }

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
                IsCommitChanged = IsCommitChanged,
                RotateAngle = RotateAngle,
                StrokeMiterLimit = StrokeMiterLimit,
                StrokeThickness = StrokeThickness,
                DefiningShape = DefiningShape,
            };
        }
    }
}
