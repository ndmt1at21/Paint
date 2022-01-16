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

namespace Paint.Thumb
{
    internal enum ResizeDirection
    {
        Bottom, Top, Left, Right
    }

    public class ResizeThumb : System.Windows.Controls.Primitives.Thumb
    {
        private DesignItemContainer container { get; set; }
        private NodeViewModel nodeVM { get; set; }

        private ResizeDirection direction { get; set; }

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

            double deltaVertical, deltaHorizontal;

            switch (VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    deltaVertical = Math.Min(-e.VerticalChange, nodeVM.Height);
                    nodeVM.Height -= deltaVertical;
                    break;
                case VerticalAlignment.Top:
                    deltaVertical = Math.Min(e.VerticalChange, nodeVM.Width);
                    nodeVM.Top += deltaVertical;
                    nodeVM.Height -= deltaVertical;
                    break;
                default:
                    break;
            }

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    deltaHorizontal = Math.Min(e.HorizontalChange, nodeVM.Width);
                    nodeVM.Left += deltaHorizontal;
                    nodeVM.Width -= deltaHorizontal;
                    break;
                case HorizontalAlignment.Right:
                    deltaHorizontal = Math.Min(-e.HorizontalChange, nodeVM.Width);
                    nodeVM.Width -= deltaHorizontal;
                    break;
                default:
                    break;
            }
        }

        private void InitResizeDirection()
        {
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    direction = ResizeDirection.Bottom;
                    break;

                case VerticalAlignment.Top:
                    direction = ResizeDirection.Top;
                    break;
            }

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    direction = ResizeDirection.Left;
                    break;

                case HorizontalAlignment.Right:
                    direction = ResizeDirection.Right;
                    break;
            }
        }

        private void UpdateBottomChanged(DragDeltaEventArgs e)
        {
            double deltaHeightChange = nodeVM.Height + e.VerticalChange;

            if (deltaHeightChange > 0)
                nodeVM.Height += e.VerticalChange;

            if (deltaHeightChange < 0)
                direction = ResizeDirection.Top;
        }

        private void UpdateTopChanged(DragDeltaEventArgs e)
        {
            double deltaHeightChange = nodeVM.Height + e.VerticalChange * -1;

            if (deltaHeightChange > 0)
            {
                Debug.WriteLine(deltaHeightChange);
                nodeVM.Top -= e.VerticalChange * -1;
                nodeVM.Height += e.VerticalChange * -1;
            }

            if (deltaHeightChange < 0)
                direction = ResizeDirection.Bottom;
        }

        private void UpdateLeftChanged(DragDeltaEventArgs e)
        {

        }

        private void UpdateRightChanged(DragDeltaEventArgs e)
        {
            nodeVM.Width += e.VerticalChange;
        }
    }
}
