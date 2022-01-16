using ControlzEx.Theming;
using Paint.Lib;
using Paint.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Paint
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            string appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Paint"
            );

            string recentFilesPath = Path.Combine(appDataPath, "RecentFiles", "recent.json");
            string backupPath = Path.Combine(appDataPath, "BackupFiles");

            Environment.SetEnvironmentVariable("AppDataPath", appDataPath);
            Environment.SetEnvironmentVariable("RecentFilesPath", recentFilesPath);
            Environment.SetEnvironmentVariable("BackupFilesPath", backupPath);

            RecentFiles.Shared.SetConfig(new RecentFileConfig
            {
                MaxItem = 10,
                Path = Environment.GetEnvironmentVariable("RecentFilesPath"),
            });

            StartupWindow startupWindow = new StartupWindow(e);
            startupWindow.Show();
        }
    }
}
