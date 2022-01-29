using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PluginContract
{
    public class RectangleNode : IShapeNode
    {
        public string ID => "Rectangle";

        public string Name => "Rectangle";

        public string CreateGeometry()
        {
            return "M 1 1 H 90 V 90 H 1 L 1 1";
        }
    }
}
