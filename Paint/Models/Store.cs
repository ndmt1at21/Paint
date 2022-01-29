using Paint.Lib;
using Paint.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Models
{
    // Actions
    public partial class Store
    {
        public Action<ObservableCollection<NodeViewModel>> OnNodesChanged;
        public Action OnProjectPathChanged;
        public Action<WindowPosition> OnMainWindowPositionUpdated;
    }

    public partial class Store
    {
        public ObservableCollection<NodeViewModel> Nodes;
        public List<RecentFileItem> RecentFiles { get; set; }

        public Store()
        {
            RecentFiles = new List<RecentFileItem>();
            Nodes = new ObservableCollection<NodeViewModel>();

            Nodes.CollectionChanged += OnCollectionNodesChanged;
        }

        public void LoadStoreFrom(ProjectStore projectStore)
        {
            MainWindowPosition = projectStore.MainWindowPosition;
            Nodes = new ObservableCollection<NodeViewModel>(projectStore.Nodes);
            RecentFiles = projectStore.RecentFiles;
            CurrentProjectPath = projectStore.CurrentProjectPath;
            IsSaveBefore = true;
            IsBlankProject = false;
            HasContentUnsaved = false;
        }

        private void OnCollectionNodesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is NodeViewModel nodeVM)
                        nodeVM.PropertyChanged += OnNodePropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is NodeViewModel nodeVM)
                        nodeVM.PropertyChanged -= OnNodePropertyChanged;
                }
            }

            if (OnNodesChanged != null)
                OnNodesChanged.Invoke(Nodes);
        }

        private void OnNodePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is NodeViewModel nodeVM)
            {
                if (nodeVM.IsCommitChanged)
                    OnNodesChanged.Invoke(Nodes);
            }
        }
    }

    public partial class Store
    {
        private string _currentProjectPath { get; set; }
        public string CurrentProjectPath
        {
            get { return _currentProjectPath; }
            set
            {
                _currentProjectPath = value;
                OnProjectPathChanged?.Invoke();
            }
        }

        public bool IsSaveBefore { get; set; } = false;
        public bool IsBlankProject { get; set; } = false;
        public bool HasContentUnsaved { get; set; } = false;
    }

    public partial class Store
    {
        public WindowPosition MainWindowPosition { get; set; }

        public void UpdateMainWindowPosition(WindowPosition position)
        {
            MainWindowPosition = position.Clone();
            OnMainWindowPositionUpdated?.Invoke(position.Clone());
        }
    }
}
