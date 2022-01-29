using Microsoft.Win32;
using Paint.Commands;
using Paint.Lib;
using Paint.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Commands
{
    public class SaveAsCommand : CommandBase
    {
        private Store _store { get; set; }
        private SaveService<Store> _saveService { get; set; }

        public SaveAsCommand(Store store, SaveService<Store> saveService)
        {
            _store = store;
            _saveService = saveService;
        }

        public override void Execute(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Save As";
            saveFileDialog.DefaultExt = ".bare";
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.Filter = "Batch Rename project (*.pait)|*.pait";
            saveFileDialog.FileName = Path.GetFileName(_store.CurrentProjectPath);

            if (saveFileDialog.ShowDialog() == true)
            {
                string savePath = saveFileDialog.FileName;

                _saveService.Save(_store, savePath);
                _store.CurrentProjectPath = savePath;
                _store.IsSaveBefore = true;
                _store.IsBlankProject = false;
                _store.HasContentUnsaved = false;


                RecentFiles.Shared.AddRecent(_store.CurrentProjectPath);
            }
        }
    }
}
