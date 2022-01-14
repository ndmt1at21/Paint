using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PluginContract
{
    public abstract class Node
    {
        public abstract string Name { get; }

        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public double RotateAngle { get; set; }

        public bool IsSelected { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static string Serialize(Node node)
        {
            return JsonConvert.SerializeObject(node, Formatting.Indented);
        }

        public static Node? Deserialize(string serializeNode)
        {
            Node? node = JsonConvert.DeserializeObject<Node>(serializeNode);
            return node;
        }
    }
}
