using Paint.CustomControl;
using Paint.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Paint.Thumb
{
    public class ResizeThumb : System.Windows.Controls.Primitives.Thumb
    {
        private DesignItemContainer container { get; set; }
        private NodeViewModel nodeVM { get; set; }

        public ResizeThumb()
        {
            DragStarted += ResizeThumb_DragStarted;
            DragDelta += ResizeThumb_DragDelta;
        }

        private void ResizeThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            container = (DesignItemContainer)DataContext;
            nodeVM = (NodeViewModel)container.DataContext;
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (container == null || nodeVM == null)
                return;

            switch (VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    UpdateBottomResize(e);
                    break;

                case VerticalAlignment.Top:
                    UpdateTopResize(e);
                    break;

                default:
                    break;
            }

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    UpdateLeftResize(e);
                    break;
                case HorizontalAlignment.Right:
                    UpdateRightResize(e);
                    break;
                default:
                    break;
            }
        }

        private void UpdateBottomResize(DragDeltaEventArgs e)
        {
            double deltaVertical = Math.Min(-e.VerticalChange, nodeVM.Height);
            double angle = nodeVM.RotateAngle * Math.PI / 180;

            nodeVM.Top += (nodeVM.TransformOrigin.Y * deltaVertical * (1 - Math.Cos(angle)));
            nodeVM.Left += -(nodeVM.TransformOrigin.X * deltaVertical * Math.Sin(-angle));
            nodeVM.Height -= deltaVertical;
        }

        private void UpdateTopResize(DragDeltaEventArgs e)
        {
            double deltaVertical = Math.Min(e.VerticalChange, nodeVM.Height);
            double angle = nodeVM.RotateAngle * Math.PI / 180;

            nodeVM.Top += deltaVertical * Math.Cos(-angle) + (nodeVM.TransformOrigin.Y * deltaVertical * (1 - Math.Cos(-angle)));
            nodeVM.Left += deltaVertical * Math.Sin(-angle) - (nodeVM.TransformOrigin.Y * deltaVertical * Math.Sin(-angle));
            nodeVM.Height -= deltaVertical;
        }

        private void UpdateLeftResize(DragDeltaEventArgs e)
        {
            double deltaHorizontal = Math.Min(e.HorizontalChange, nodeVM.Width);
            double angle = nodeVM.RotateAngle * Math.PI / 180;

            nodeVM.Top += deltaHorizontal * Math.Sin(angle) - nodeVM.TransformOrigin.X * deltaHorizontal * Math.Sin(angle);
            nodeVM.Left += deltaHorizontal * Math.Cos(angle) + nodeVM.TransformOrigin.X * deltaHorizontal * (1 - Math.Cos(angle));
            nodeVM.Width -= deltaHorizontal;
        }

        private void UpdateRightResize(DragDeltaEventArgs e)
        {
            double deltaHorizontal = Math.Min(-e.HorizontalChange, nodeVM.Width);
            double angle = nodeVM.RotateAngle * Math.PI / 180;

            nodeVM.Top -= nodeVM.TransformOrigin.X * deltaHorizontal * Math.Sin(angle);
            nodeVM.Left += nodeVM.TransformOrigin.X * deltaHorizontal * (1 - Math.Cos(angle));
            nodeVM.Width -= deltaHorizontal;
        }
    }
}
