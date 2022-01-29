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
    public class CopyCommand : CommandBase
    {
        private MainWindow _window { get; set; }

        public CopyCommand(MainWindow window)
        {
            _window = window;
            Gesture = new KeyGesture(Key.C, ModifierKeys.Control);
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
        }
    }
}
