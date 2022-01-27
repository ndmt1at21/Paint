using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PluginContract
{
    public class LineNode : INode
    {
        public string ID => "Line";

        public string Name => "Line";

        public Image Icon => throw new NotImplementedException();
    }
}
