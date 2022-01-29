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
    public class SelectAllCommand : CommandBase
    {
        private MainWindow _window { get; set; }

        public SelectAllCommand(MainWindow window)
        {
            _window = window;
            Gesture = new KeyGesture(Key.A, ModifierKeys.Control);
        }

        public override void Execute(object parameter)
        {
            foreach (var item in _window.Nodes)
            {
                item.IsSelected = true;
                _window.SelectedItems.Add(item);
            }
        }
    }
}
