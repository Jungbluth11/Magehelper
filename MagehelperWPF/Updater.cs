using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;

namespace Magehelper.WPF
{
    internal class Updater
    {
#pragma warning disable SYSLIB0014
        private readonly WebClient webClient = new WebClient();
#pragma warning restore SYSLIB0014

        public bool CheckForUpdates()
        {
            try
            {
                string version = webClient.DownloadString("https://api.jungbluthcloud.de/updates/magehelper/version");
                if (version != Assembly.GetExecutingAssembly().GetName().Version.ToString())
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Update()
        {
            try
            {
                Process.Start(Path.Combine(AppContext.BaseDirectory, "updater.exe"));
                Application.Current.Shutdown();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}