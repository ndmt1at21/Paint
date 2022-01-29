using PluginContract;
using System;

namespace Rectangle
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
