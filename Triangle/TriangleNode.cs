using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace Triangle
{
    public class TriangleNode : IShapeNode
    {
        public string ID => "Triangle";

        public string Name => "Triangle";

        public Image Icon => throw new NotImplementedException();

        public Geometry CreateGeometry()
        {
            Geometry res = Geometry.Parse("m226.865 331.44813l127.5 -201.99998l127.5 201.99998l-255.00001 0z");
            return res;
        }
    }
}
