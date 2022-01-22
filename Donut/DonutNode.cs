using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;


namespace Donut
{
    public class DonutNode : IShapeNode
    {
        public string ID => "Donut";

        public string Name => "Donut";

        public Geometry CreateGeometry()
        {
            Geometry res = Geometry.Parse("m293.53002 271.16501l0 0c0 -57.43761 54.62127 -104.00001 122 -104.00001l0 0c32.3564 0 63.38759 10.95711 86.26701 30.46089c22.87945 19.50379 35.73299 45.95659 35.73299 73.53912l0 0c0 57.43761 -54.62127 104.00001 -122 104.00001l0 0c-67.37873 0 -122 -46.5624 -122 -104.00001zm61 0l0 0c0 28.7188 27.31065 52 61 52c33.68937 0 61 -23.2812 61 -52c0 -28.7188 -27.31064 -52 -61 -52l0 0c-33.68935 0 -61 23.28121 -61 52z");
            return res;
        }
    }
}
