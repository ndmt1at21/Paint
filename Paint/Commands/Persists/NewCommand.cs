using Paint.Commands;
using Paint.Lib;
using Paint.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Paint.Commands
{
    public class NewCommand : CommandBase
    {
        private PluginManager _pluginManager { get; set; }
        private Action _onExecuted { get; set; }

        public NewCommand(PluginManager pluginManager, Action onExecuted = null)
        {
            _pluginManager = pluginManager;
            _onExecuted = onExecuted;

            Gesture = new KeyGesture(Key.N, ModifierKeys.Control);
        }

        public override void Execute(object parameter)
        {
            MainWindow.ProjectNumber++;
            MainWindow mainWindow = new MainWindow(_pluginManager);
            mainWindow.Show();

            _onExecuted?.Invoke();
        }
    }
}
