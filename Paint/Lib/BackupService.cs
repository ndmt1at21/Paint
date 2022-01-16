using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Threading;

namespace Paint.Lib
{
    public class BackupConfig
    {
        public int BackupInterval { get; set; }
        public string Extension { get; set; }
        public string Directory { get; set; }
    }

    public class BackupService<T> : SaveService<T>
    {
        public Func<T> GetBackupData;
        public Func<string> GetBackupPath;

        public Action OnBackup;
        public Action OnBackuped;

        private string _currentFileName { get; set; }
        public string CurrentFilePath => $"{_config.Directory}\\{_currentFileName}_{_backupStartTime.Millisecond}.{_config.Extension}";

        private BackupConfig _config { get; set; }
        private DispatcherTimer _dispatcherTimer { get; set; }
        private bool _isBackup { get; set; } = false;
        private readonly DateTime _backupStartTime = DateTime.Now;

        public BackupService(BackupConfig config, IPersister<T> persister) : base(persister)
        {
            _config = config;
            _persister = new JsonPersister<T>();
        }

        public bool CheckExistBackupFile()
        {
            var files = Directory.GetFiles(_config.Directory);

            foreach (var file in files)
            {
                if (Path.GetExtension(file) == _config.Extension)
                {
                    return true;
                }
            }
            return false;
        }

        public void StartBackup()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(Backup_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, _config.BackupInterval);
            _dispatcherTimer.Start();
        }

        public void StopBackup()
        {
            _dispatcherTimer.Tick -= new EventHandler(Backup_Tick);
            _dispatcherTimer.Stop();
            DeleteBackupFile();
        }

        public T LoadBackupFile()
        {
            try
            {
                T result = _persister.Load(CurrentFilePath);
                return result;
            }
            catch
            {
                throw new Exception("Invalid backup file structure");
            }
        }

        private async void Backup_Tick(object sender, EventArgs e)
        {
            if (_isBackup)
                return;

            T data = GetBackupData.Invoke();
            string path = GetBackupPath.Invoke();

            if (data == null)
                throw new Exception("Invalid backup data");

            DeleteBackupFile();

            _currentFileName = Path.GetFileName(path);
            await SaveAsync(data, CurrentFilePath);
        }

        private void DeleteBackupFile()
        {
            _persister.Delete(CurrentFilePath);
        }
    }
}
