using PluginContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heart
{
    public class HeartNode : IShapeNode
    {
        public string ID => "Heart";

        public string Name => "Heart";

        public Image Icon => throw new NotImplementedException();

        public string CreateGeometry()
        {
            return "M 366 241 A 87.5 89 0 0 1 278.5 330 87.5 89 0 0 1 191 241 87.5 89 0 0 1 278.5 152 87.5 89 0 0 1 366 241 Z";
        }
    }
}
