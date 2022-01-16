using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PluginContract
{
    public class TextNode : INode
    {
        public string Text { get; set; }
        public int FontSize { get; set; }
        public double FontWeight { get; set; }
        public SolidColorBrush Color { get; set; } = Brushes.Black;

        public string Name => "Text";
        public string ID => "Text";
    }
}
