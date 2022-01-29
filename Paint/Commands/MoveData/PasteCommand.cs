using Newtonsoft.Json;
using Paint.ViewModels;
using Paint.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Paint.Commands.MoveData
{
    public class PasteCommand : CommandBase
    {
        private MainWindow _window { get; set; }
        public object SelectedItems { get; private set; }

        public PasteCommand(MainWindow window)
        {
            _window = window;
            Gesture = new KeyGesture(Key.V, ModifierKeys.Control);
        }

        public override void Execute(object parameter)
        {
            JsonSerializerSettings _serializeSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            };

            string data = null;

            if (Clipboard.ContainsData("PaintObjects"))
                data = (string)Clipboard.GetData("PaintObjects");

            if (data == null)
                return;

            List<NodeViewModel> nodes = JsonConvert.DeserializeObject<List<NodeViewModel>>(data, _serializeSettings);

            foreach (var node in nodes)
            {
                _window.Nodes.Add(node);
                _window.SelectedItems.Add(node);
            }
        }
    }
}
