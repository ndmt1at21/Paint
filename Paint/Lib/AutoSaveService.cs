using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Paint.Lib
{
    public class AutoSaveConfig
    {
        public bool AutoSave { get; set; }
        public int AutoSaveInterval { get; set; }
    }

    public class AutoSaveService<T> : SaveService<T>
    {
        public Func<T> GetSaveData;
        public Func<string> GetSavePath;

        private AutoSaveConfig _config { get; set; }
        private DispatcherTimer _timer { get; set; }

        private string _tempPath { get; set; }

        public AutoSaveService(AutoSaveConfig config, IPersister<T> persister) : base(persister)
        {
            _config = config;
        }

        public void EnableAutoSave()
        {
            _config.AutoSave = true;

            if (_timer != null)
                _timer.Stop();

            if (_timer == null)
                _timer = new DispatcherTimer();

            _timer.Start();
            _timer.Interval = new TimeSpan(0, 0, _config.AutoSaveInterval);
            _timer.Tick += new EventHandler(AutoSave_Tick);
            _timer.Start();
        }

        public void StopAutoSave()
        {
            _config.AutoSave = false;
            _timer.Tick -= new EventHandler(AutoSave_Tick);
            _timer.Stop();
        }

        public async void AutoSave_Tick(object sender, EventArgs e)
        {
            T data = GetSaveData.Invoke();
            _tempPath = GetSavePath.Invoke();

            if (data == null)
                throw new Exception("Invalid data");

            DeleteAutoSaveFile();
            await SaveAsync(data, _tempPath);
        }

        private void DeleteAutoSaveFile()
        {
            if (!File.Exists(_tempPath))
                return;

            File.Delete(_tempPath);
        }
    }
}
