using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Magehelper.WPF
{
    internal class Updater
    {
        private bool updataAvailable = false;
        public bool CheckForUpdates()
        {
            Process updater = new Process();
            updater.StartInfo.FileName = Path.Combine(AppContext.BaseDirectory, "updater.exe");
            updater.StartInfo.Arguments = "checkforupdates " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            updater.StartInfo.RedirectStandardOutput = true;
            updater.Start();
            while (!updater.StandardOutput.EndOfStream)
            {
                updataAvailable = updater.StandardOutput.ReadLine() == "True" ? true : false;
            }
            return updataAvailable;
        }

        public void Update()
        {
            Process.Start(Path.Combine(AppContext.BaseDirectory, "updater.exe"));
            Application.Current.Shutdown();
        }
    }
}