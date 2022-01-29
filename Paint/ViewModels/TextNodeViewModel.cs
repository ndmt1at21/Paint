﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.ViewModels
{
    public class TextNodeViewModel : NodeViewModel
    {
        public string Content { get; set; }
        public bool IsFocusable { get; set; }

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
                IsDragging = IsDragging,
                RotateAngle = RotateAngle,
                StrokeMiterLimit = StrokeMiterLimit,
                StrokeThickness = StrokeThickness,
                Content = Content,
            };
        }
    }
}
