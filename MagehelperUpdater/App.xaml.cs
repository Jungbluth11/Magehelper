using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace Magehelper.Updater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);

        private const int ATTACH_PARENT_PROCESS = -1;
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length == 2 && e.Args[0] == "checkforupdates")
            {
                AttachConsole(ATTACH_PARENT_PROCESS);
                Console.WriteLine(UpdaterCore.CheckForUpdates(e.Args[1]));
            }
            else
            {
                new UpdaterWindow().Show();
            }
            base.OnStartup(e);
        }
    }
}