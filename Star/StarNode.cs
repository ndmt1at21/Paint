using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Star
{
	public class StarNode : IShapeNode
	{
        public string ID => "Star";

        public string Name => "Star";

        public Geometry CreateGeometry()
        {
            Geometry res = Geometry.Parse("M156 0L192.596 105.029H311.022L215.213 169.941L251.809 274.971L156 210.059L60.191 274.971L96.7868 169.941L0.977783 105.029H119.404L156 0Z");
            return res;
        }
    }
}
