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
            ContentControl control = (ContentControl)sender;
            Node node = (Node)control.DataContext;

            Debug.WriteLine(node);

        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {

        }
    }
}
