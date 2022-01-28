using Paint.CustomControl;
using Paint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Paint.Thumb
{
    public class LineStartPointThumb : System.Windows.Controls.Primitives.Thumb
    {
        private DesignItemContainer container { get; set; }
        private LineNodeViewModel nodeVM { get; set; }

        public LineStartPointThumb()
        {
            DragStarted += LineStartPointThumb_DragStarted;
            DragDelta += LineStartPointThumb_DragDelta;

            Style = (Style)FindResource("LineResizeThumbStyle");
        }

        private void LineStartPointThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            container = (DesignItemContainer)DataContext;
            nodeVM = (LineNodeViewModel)container.DataContext;
        }

        private void LineStartPointThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (nodeVM != null)
            {
                nodeVM.X1 += e.HorizontalChange;
                nodeVM.Y1 += e.VerticalChange;
            }
        }
    }
}
