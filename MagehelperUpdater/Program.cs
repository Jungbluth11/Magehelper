using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Magehelper.Updater
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length == 2 && args[0] == "checkforupdates")
            {
                AttachConsole(ATTACH_PARENT_PROCESS);
                Console.WriteLine(UpdaterCore.CheckForUpdates(args[1]));
            }
            else
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new UpdaterForm());
            }
        }
    }
}