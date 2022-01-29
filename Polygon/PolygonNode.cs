using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Polygon
{
    public class PolygonNode : IShapeNode
    {
        public string ID => "Polygon";

        public string Name => "Polygon";

        public string CreateGeometry()
        {
            return "M106 0L211.655 132.75H0.344902L106 0Z";
        }
    }
}
