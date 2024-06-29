using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.Json;
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
                WebClient webClient = new();
                Version lastVersion = JsonSerializer.Deserialize<Version>(webClient.DownloadString("https://api.jungbluthcloud.de/updates/dsametatalente/version"));
                System.Version? currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                if (currentVersion.Major < lastVersion.Major)
                {
                    return true;
                }
                else if (currentVersion.Minor < lastVersion.Minor)
                {
                    return true;
                }
                else if (currentVersion.Build < lastVersion.Build)
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