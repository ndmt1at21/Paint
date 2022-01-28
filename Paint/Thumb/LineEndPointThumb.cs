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
    public class LineEndPointThumb : System.Windows.Controls.Primitives.Thumb
    {
        private DesignItemContainer container { get; set; }
        private LineNodeViewModel nodeVM { get; set; }

        public LineEndPointThumb()
        {
            DragStarted += LineEndPointThumb_DragStarted;
            DragDelta += LineEndPointThumb_DragDelta;

            Style = (Style)FindResource("LineResizeThumbStyle");
        }

        private void LineEndPointThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            container = (DesignItemContainer)DataContext;
            nodeVM = (LineNodeViewModel)container.DataContext;
        }

        private void LineEndPointThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (nodeVM != null)
            {
                nodeVM.X2 += e.HorizontalChange;
                nodeVM.Y2 += e.VerticalChange;
            }
        }
    }
}
