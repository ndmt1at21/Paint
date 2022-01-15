using Paint.CustomControl;
using Paint.ViewModels;
using PluginContract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Paint.Thumb
{
    public class MoveThumb : System.Windows.Controls.Primitives.Thumb
    {
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

            if (vm != null)
            {
                vm.Top += e.VerticalChange;
                vm.Left += e.HorizontalChange;
            }

            e.Handled = true;
        }
    }
}
