using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pentagon
{
    public class PenatgonNode : IShapeNode
    {
        public string ID => "Pentagon";

        public string Name => "Pentagon";

        public Image Icon => throw new NotImplementedException();

        public string CreateGeometry()
        {
            return "M76.5 0L113.883 27.5173L152.109 53.8967L136.987 97.2827L123.229 141.103L76.5 140.4L29.7711 141.103L16.0128 97.2827L0.891006 53.8967L39.1169 27.5173L76.5 0Z";
        }
    }
}
