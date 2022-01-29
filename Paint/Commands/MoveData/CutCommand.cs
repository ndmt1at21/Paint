using Newtonsoft.Json;
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
    public class CutCommand : CommandBase
    {
        private MainWindow _window { get; set; }

        public CutCommand(MainWindow window)
        {
            _window = window;
            Gesture = new KeyGesture(Key.X, ModifierKeys.Control);
        }

        public override void Execute(object parameter)
        {
            JsonSerializerSettings _serializeSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            };

            Clipboard.Clear();
            Clipboard.SetData("PaintObjects", JsonConvert.SerializeObject(_window.SelectedItems.ToList(), _serializeSettings));

            _window.SelectedItems.ToList().ForEach(item => _window.Nodes.Remove(item));
        }
    }
}
