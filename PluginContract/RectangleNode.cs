using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PluginContract
{
    public class RectangleNode : ShapeNode
    {
        public override Geometry DefiningShape => CreateGeometry();

        public Geometry CreateGeometry()
        {
            return new RectangleGeometry { Rect = new Rect(0, 0, Width, Height) };
        }
    }

}
