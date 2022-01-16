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
    public class RectangleNode : IShapeNode
    {
        public string ID => "Rectangle";

        public string Name => "Rectangle";

        public Geometry CreateGeometry()
        {
            return new RectangleGeometry { Rect = new Rect(0, 0, 1, 1) };
        }
    }
}
