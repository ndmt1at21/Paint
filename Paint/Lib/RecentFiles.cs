using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Lib
{
    public class RecentFileItem
    {
        public string Path { get; set; }
        public DateTime LastModified { get; set; }
    }

    public class RecentFileItems
    {
        public List<RecentFileItem> Items { get; set; }

        public RecentFileItems()
        {
            Items = new List<RecentFileItem>();
        }
    }

    public class RecentFileConfig
    {
        public string Path;
        public int MaxItem;
    }

    public class RecentFiles
    {
        private IPersister<RecentFileItems> _persister { get; set; }
        private RecentFileConfig _config { get; set; }

        private RecentFiles()
        {
            _persister = new JsonPersister<RecentFileItems>();
        }

        private static RecentFiles _shared { get; set; }
        public static RecentFiles Shared
        {
            get
            {
                if (_shared == null)
                    _shared = new RecentFiles();
                return _shared;
            }
        }

        public void SetConfig(RecentFileConfig config)
        {
            _config = config;
        }

        public RecentFileItems GetRecentFiles()
        {
            try
            {
                RecentFileItems recentFiles = _persister.Load(_config.Path);
                return recentFiles == null ? new RecentFileItems() : recentFiles;
            }
            catch
            {
                return new RecentFileItems();
            }
        }

        public void AddRecent(string recentFilePath)
        {
            RecentFileItems recentFiles = GetRecentFiles();
            RecentFileItem recentFile = new RecentFileItem
            {
                Path = recentFilePath,
                LastModified = DateTime.Now
            };

            bool isFileExists = recentFiles.Items.Where(item => item.Path == recentFilePath).Count() > 0;

            if (isFileExists)
            {
                recentFiles.Items = recentFiles.Items.OrderBy(item => item.LastModified).ToList();
            }

            if (!isFileExists)
            {
                if (recentFiles.Items.Count >= _config.MaxItem)
                    recentFiles.Items.RemoveAt(0);
                recentFiles.Items.Add(recentFile);
            }

            _persister.Save(_config.Path, recentFiles);
        }

        public void RemoveRecent(string path)
        {
            RecentFileItems recentFiles = GetRecentFiles();
            recentFiles.Items.RemoveAll(f => f.Path == path);
            _persister.Save(_config.Path, recentFiles);
        }
    }
}
