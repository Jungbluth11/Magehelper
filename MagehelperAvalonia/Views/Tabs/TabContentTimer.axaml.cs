using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;

namespace Magehelper.Avalonia.Views.Tabs;

public partial class TabContentTimer : UserControl
{
    public TabContentTimer()
    {
        DataContext = TabContentTimerViewModel.Instance;
        InitializeComponent();
    }

    public void ResetTab()
    {
        TabContentTimerViewModel.Instance.ResetTab();
    }

    private void BtnAddTimer_Click(object sender, RoutedEventArgs e)
    {
        Window mainWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow;
        new AddTimerWindow().ShowDialog(mainWindow);
    }
}
