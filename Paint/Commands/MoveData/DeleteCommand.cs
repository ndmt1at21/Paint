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
    public class DeleteCommand : CommandBase
    {
        private MainWindow _window { get; set; }

        public DeleteCommand(MainWindow window)
        {
            _window = window;
            Gesture = new KeyGesture(Key.Delete);
        }

        public override void Execute(object parameter)
        {
            _window.SelectedItems.ToList().ForEach(item =>
            {
                _window.Nodes.Remove(item);
            });
        }
    }
}
