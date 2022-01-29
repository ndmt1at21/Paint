using Paint.CustomControl;
using Paint.ViewModels;
using PluginContract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Paint.Thumb
{
    public class RotateThumb : System.Windows.Controls.Primitives.Thumb
    {
        // Container is bound of element render nodeVM
        private DesignItemContainer container { get; set; }
        private DesignCanvas canvas;
        private NodeViewModel nodeVM { get; set; }

        // For calculator end vector
        private Point centerPoint { get; set; }
        private Vector startVector { get; set; }
        private double initialAngle { get; set; }

        public RotateThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.RotateThumb_DragDelta);
            DragStarted += new DragStartedEventHandler(this.RotateThumb_DragStarted);
            DragCompleted += RotateThumb_DragCompleted;
        }

        private void RotateThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            container = (DesignItemContainer)DataContext;
            nodeVM = (NodeViewModel)container.DataContext;
            canvas = (DesignCanvas)VisualTreeHelper.GetParent(container);

            if (canvas != null)
            {
                initialAngle = nodeVM.RotateAngle;
                startVector = CalculateStartVector();
                nodeVM.IsCommitChanged = false;
            }
        }

        private void RotateThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (container == null || nodeVM == null || canvas == null)
                return;

            Point currentPoint = Mouse.GetPosition(canvas);
            Vector deltaVector = Point.Subtract(currentPoint, centerPoint);

            double angle = Vector.AngleBetween(startVector, deltaVector);
            nodeVM.RotateAngle = initialAngle + Math.Round(angle, 0);
        }

        private void RotateThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (nodeVM != null)
            {
                nodeVM.IsCommitChanged = true;
            }
        }

        private Vector CalculateStartVector()
        {
            centerPoint = container.TranslatePoint(
                new Point
                (
                    container.Width * container.RenderTransformOrigin.X,
                    container.Height * container.RenderTransformOrigin.Y
                ), canvas);

            Point startPoint = Mouse.GetPosition(canvas);
            return Point.Subtract(startPoint, centerPoint);
        }
    }
}
