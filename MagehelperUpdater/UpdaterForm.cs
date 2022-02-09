using System;
using System.Windows.Forms;

namespace Magehelper.Updater
{
    public partial class UpdaterForm : Form
    {
        private readonly UpdaterCore updater;

        public UpdaterForm()
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
            StringStatus.Text = "Kopiere neue Dateien...";
        }

        public void InstallationComplete()
        {
            StringStatus.Text = "Lösche temporäre Dateien...";
            Application.Exit();
        }

        private void UpdaterForm_Shown(object sender, EventArgs e)
        {
            try
            {
                StringStatus.Text = "Lade Dateien herunter...";
                updater.Download();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Magehelper updater", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}