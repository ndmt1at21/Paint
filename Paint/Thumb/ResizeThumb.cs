using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Paint.Thumb
{
    public class ResizeThumb : System.Windows.Controls.Primitives.Thumb
    {
        public ResizeThumb()
        {
            DragStarted += ResizeThumb_DragStarted;
            DragDelta += ResizeThumb_DragDelta;
        }

        private void ResizeThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            e.Handled = true;
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {

        }
    }
}
