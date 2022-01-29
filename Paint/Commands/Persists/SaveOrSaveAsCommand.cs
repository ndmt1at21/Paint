using Paint.Commands;
using Paint.Lib;
using Paint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Paint.Commands
{
    public class SaveOrSaveAsCommand : CommandBase
    {
        private Store _store { get; set; }
        private SaveService<Store> _saveService { get; set; }

        private ICommand _saveCommand { get; set; }
        private ICommand _saveAsCommand { get; set; }

        public SaveOrSaveAsCommand(Store store, SaveService<Store> saveService)
        {
            _store = store;
            _saveService = saveService;

            _saveCommand = new SaveCommand(_store, saveService);
            _saveAsCommand = new SaveAsCommand(_store, saveService);

            Gesture = new KeyGesture(Key.S, ModifierKeys.Control);
        }

        public override void Execute(object parameter)
        {
            if (_store.IsSaveBefore)
            {
                _saveCommand.Execute(parameter);
                return;
            }


            if (!_store.IsSaveBefore)
            {
                _saveAsCommand.Execute(parameter);
                return;
            }
        }
    }
}
