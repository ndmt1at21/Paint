using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Paint.ViewModels
{
    public class ImageNodeViewModel : NodeViewModel
    {
        public BitmapImage ImageSource { get; set; }

        public override NodeViewModel Clone()
        {
            return new ImageNodeViewModel
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
                ImageSource = ImageSource.Clone(),
            };
        }
    }
}
