using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace Magehelper.Updater
{
    public class UpdaterCore
    {
        private readonly string updaterfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "magehelper", "updaterDownloads");
#pragma warning disable SYSLIB0014
        private static readonly WebClient webClient = new WebClient();
#pragma warning restore SYSLIB0014
        private readonly Action<int> downloadProgressChangedAction;
        private readonly Action downloadFileCompleteAction;
        private readonly Action installationCompleteAction;

        public UpdaterCore(Action<int> downloadProgressChangedAction, Action downloadFileCompleteAction, Action installationCompleteAction)
        {
            this.downloadProgressChangedAction = downloadProgressChangedAction;
            this.downloadFileCompleteAction = downloadFileCompleteAction;
            this.installationCompleteAction = installationCompleteAction;
        }

        public static bool CheckForUpdates(string currentVersion)
        {
            try
            {
                string version = webClient.DownloadString("https://api.jungbluthcloud.de/updates/magehelper/version");
                if (version != currentVersion)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                throw new("Verbindung zum update-Server fehlgeschlagen");
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadProgressChangedAction(e.ProgressPercentage);
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                downloadFileCompleteAction();
                ZipFile.ExtractToDirectory(Path.Combine(updaterfolder, "tmp.zip"), updaterfolder);
            }
            catch
            {
                File.Delete(Path.Combine(updaterfolder, "MagehelperInstaller.exe"));
            }
            File.Delete(Path.Combine(updaterfolder, "tmp.zip"));
            Process setup = new Process();
            setup.StartInfo.FileName = Path.Combine(updaterfolder, "MagehelperInstaller.exe");
#if RELEASE
            setup.StartInfo.Arguments = "/SILENT";
#endif
            setup.Start();
            setup.WaitForExit();
            installationCompleteAction();
            File.Delete(Path.Combine(updaterfolder, "MagehelperInstaller.exe"));
        }

        public void Download()
        {
            try
            {
                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                webClient.DownloadFileAsync(new Uri("https://api.jungbluthcloud.de/updates/magehelper/link"), Path.Combine(updaterfolder, "tmp.zip"));
            }
            catch
            {
                throw new Exception("Download des updates fehlgeschlagen");
            }
        }
    }
}