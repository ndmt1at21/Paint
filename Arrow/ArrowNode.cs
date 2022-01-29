using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Arrow
{
    public class ArrowNode : IShapeNode
    {
        public string ID => "Arrow";

        public string Name => "Arrow";

        public Image Icon => throw new NotImplementedException();

        public strng CreateGeometry()
        {
            return "M142.061 13.0607C142.646 12.4749 142.646 11.5251 142.061 10.9394L132.515 1.39341C131.929 0.807624 130.979 0.807624 130.393 1.39341C129.808 1.9792 129.808 2.92894 130.393 3.51473L138.879 12L130.393 20.4853C129.808 21.0711 129.808 22.0208 130.393 22.6066C130.979 23.1924 131.929 23.1924 132.515 22.6066L142.061 13.0607ZM-1.31134e-07 13.5L141 13.5L141 10.5L1.31134e-07 10.5L-1.31134e-07 13.5Z";
        }
    }
}
