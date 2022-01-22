using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Diamond
{
    public class DiamondNode : IShapeNode
    {
        public string ID => "Diamond";

        public string Name => "Diamond";

        public Geometry CreateGeometry()
        {
            Geometry res = Geometry.Parse("m297.50999 255.24999l88 -105l88 105l-88 105l-88 -105z");
            return res;
        }
    }
}
