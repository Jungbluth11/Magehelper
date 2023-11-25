using System;
using System.Windows;
using System.Windows.Controls;

namespace Magehelper.Updater
{
    /// <summary>
    /// Interaktionslogik für UpdaterWindow.xaml
    /// </summary>
    public partial class UpdaterWindow : Window
    {
        private readonly UpdaterCore updater;

        public UpdaterWindow()
        {
            InitializeComponent();
            updater = new UpdaterCore(DownloadProgressChanged, DownLoadFileComplete, InstallationComplete);
        }

        public void DownloadProgressChanged(int value)
        {
            ProgressBar.Value = value;
        }

        public void DownLoadFileComplete()
        {
            StringStatus.Content = "Kopiere neue Dateien...";
        }

        public void InstallationComplete()
        {
            StringStatus.Content = "Lösche temporäre Dateien...";
            Close();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                StringStatus.Content = "Lade Dateien herunter...";
                updater.Download();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Magehelper Updater", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
    }
}