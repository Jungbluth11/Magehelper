using System.Windows;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void ApplicationStartup(object sender, StartupEventArgs e)
        {
            MainWindow window = new MainWindow(e.Args);
            window.Show();
        }
    }
}