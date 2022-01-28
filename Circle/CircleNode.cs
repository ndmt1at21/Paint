using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Circle
{
    public class CircleNode : IShapeNode
    {
        public string ID => "Circle";

        public string Name => "Circle";

		public Image Icon => throw new NotImplementedException();

		public Geometry CreateGeometry()
        {
            Geometry res = Geometry.Parse("M 366 241 A 87.5 89 0 0 1 278.5 330 87.5 89 0 0 1 191 241 87.5 89 0 0 1 278.5 152 87.5 89 0 0 1 366 241 Z");
            return res;
        }
    }
}
