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

namespace Paint.Thumb
{
    public class MoveThumb : System.Windows.Controls.Primitives.Thumb
    {
        private DesignItemContainer container { get; set; }

        public MoveThumb()
        {
            DragStarted += MoveThumb_DragStarted;
            DragDelta += MoveThumb_DragDelta;
        }

        private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
        {

        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            DesignItemContainer container = (DesignItemContainer)DataContext;
            NodeViewModel vm = (NodeViewModel)container.DataContext;

            if (container != null)
            {
                Point dragDelta = new Point(e.HorizontalChange, e.VerticalChange);

                dragDelta = container.RotateTransform.Transform(dragDelta);

                if (vm != null)
                {
                    vm.Left += dragDelta.X;
                    vm.Top += dragDelta.Y;
                }
            }

            e.Handled = true;
        }
    }
}
