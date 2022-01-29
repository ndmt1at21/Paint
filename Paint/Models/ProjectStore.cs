using Paint.Lib;
using Paint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Models
{
    public class ProjectStore
    {
        public List<NodeViewModel> Nodes;
        public List<RecentFileItem> RecentFiles { get; set; }
        public string CurrentProjectPath { get; set; }
        public bool IsSaveBefore { get; set; } = false;
        public bool IsBlankProject { get; set; } = false;
        public bool HasContentUnsaved { get; set; } = false;
        public WindowPosition MainWindowPosition { get; set; }

        public static ProjectStore CreateFromStore(Store store)
        {
            ProjectStore projectStore = new ProjectStore
            {
                CurrentProjectPath = store.CurrentProjectPath,
                IsBlankProject = store.IsBlankProject,
                HasContentUnsaved = store.HasContentUnsaved,
                MainWindowPosition = store.MainWindowPosition,
                IsSaveBefore = store.IsSaveBefore,
                Nodes = store.Nodes.ToList(),
                RecentFiles = store.RecentFiles.ToList(),
            };

            return projectStore;
        }
    }
}
