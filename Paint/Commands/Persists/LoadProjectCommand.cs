using Microsoft.Win32;
using Paint.Commands;
using Paint.Lib;
using Paint.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Commands
{
    public class LoadProjectCommand : CommandBase
    {
        private Store _store { get; set; }
        private LoadService<ProjectStore> _loadService { get; set; }

        public LoadProjectCommand(Store store, LoadService<ProjectStore> loadService)
        {
            _store = store;
            _loadService = loadService;
        }

        public override void Execute(object parameter)
        {
            string loadPath = (string)parameter;

            ProjectStore projectStore = _loadService.Load(loadPath);
            _store.LoadStoreFrom(projectStore);
            _store.CurrentProjectPath = loadPath;
            _store.IsBlankProject = false;
            _store.IsSaveBefore = true;

            foreach (var item in _store.Nodes)
            {
                item.IsSelected = false;
            }
        }
    }
}
