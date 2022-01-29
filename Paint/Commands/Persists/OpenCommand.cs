using Microsoft.Win32;
using Paint.Commands;
using Paint.Lib;
using Paint.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Paint.Commands
{
    public class OpenCommand : CommandBase
    {
        private PluginManager _pluginManager { get; set; }
        private Action _onExecuted { get; set; }

        public OpenCommand(PluginManager pluginManager, Action onExecuted = null)
        {
            _pluginManager = pluginManager;
            _onExecuted = onExecuted;

            Gesture = new KeyGesture(Key.O, ModifierKeys.Control);
        }

        public override void Execute(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".bare";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Bare files (*.pait)|*.pait|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == false)
            {
                return;
            }

            try
            {
                MainWindow mainWindow = new MainWindow(_pluginManager);
                mainWindow.LoadFrom(openFileDialog.FileName);
                mainWindow.Show();

                _onExecuted?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
