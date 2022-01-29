using Paint.Models;
using Paint.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Paint.Commands
{
    public class ExitCommand : CommandBase
    {
        private Store _store { get; set; }
        private MainWindow _window { get; set; }

        public ExitCommand(Store store, MainWindow window)
        {
            _store = store;
            _window = window;
            Gesture = new KeyGesture(Key.F4, ModifierKeys.Alt);
        }

        public override void Execute(object parameter)
        {
            if (_store.HasContentUnsaved)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to discard all changes?", "Unsaved Changes",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning,
                    MessageBoxResult.No
                );

                if (result == MessageBoxResult.Yes)
                {
                    _window.Close();
                }
            }

            if (!_store.HasContentUnsaved)
            {
                _window.Close();
            }
        }
    }
}
