using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Magehelper.Avalonia.Views.Windows;

public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        DataContext = new SettingsWindowViewModel();
        InitializeComponent();
        foreach(TabItem tabItem in (DataContext as SettingsWindowViewModel).Tabs)
        {
            TabControlArtifacts.Items.Add(tabItem);
        }
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        new DialogTextWindow("Konfiguration hinzufügen", "Name der Konfiguration", (DataContext as SettingsWindowViewModel).AddConfig).ShowDialog(this);
    }

    private void ComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (TabControlArtifacts != null)
        {
            foreach (TabItem tabItem in (DataContext as SettingsWindowViewModel).Tabs)
            {
                TabControlArtifacts.Items.Add(tabItem);
            }
        }
    }
}