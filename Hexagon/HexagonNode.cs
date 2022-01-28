using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hexagon
{
	public class HexagonNode: IShapeNode
	{
        public string ID => "Hexagon";

        public string Name => "Hexagon";

		public Image Icon => throw new NotImplementedException();

		public Geometry CreateGeometry()
        {
            Geometry res = Geometry.Parse("M69.5 0L103.685 21.8212L138.349 42.75L137.87 85.5L138.349 128.25L103.685 149.179L69.5 171L35.315 149.179L0.650978 128.25L1.13 85.5L0.650978 42.75L35.315 21.8212L69.5 0Z");
            return res;
        }
    }
}
