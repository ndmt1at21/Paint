using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint.ViewModels
{
    public class TextNodeViewModel : NodeViewModel
    {
        public string Content { get; set; }
        public bool IsFocusable { get; set; }

        public FontFamily FontFamily { get; set; }
        public int FontSize { get; set; }
        public FontWeight FontWeight { get; set; }
        public FontStyle FontStyle { get; set; }
        public TextDecorationCollection TextDecorations { get; set; }

        public override NodeViewModel Clone()
        {
            return new TextNodeViewModel
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
                Content = Content,
            };
        }
    }
}
