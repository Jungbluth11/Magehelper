#pragma warning disable 8602
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;

namespace Magehelper.Avalonia.Views.Tabs;

public partial class TabContentMod : UserControl
{
    public TabContentMod()
    {
        DataContext = new TabContentModViewModel();
        InitializeComponent();
    }

    private void CheckBox_Checked(object? sender, RoutedEventArgs e)
    {
        DialogNumberWindow dialogNumberWindow = new("Magieresistenz", "höchste beteiligte Magieresistenz", (DataContext as TabContentModViewModel).AddMr);
#pragma warning disable CS8604
        dialogNumberWindow.ShowDialog((Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow);
#pragma warning restore CS8604
    }
}