using System.Reflection;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DSAUtils.UI;

namespace Magehelper.Avalonia.Views.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        DataContext = MainWindowViewModel.Instance;
        InitializeComponent();
        TabSetting[] tabSettings = MainWindowViewModel.Instance.Settings.GetTabSettings();
        foreach (TabSetting tabSetting in tabSettings)
        {
            if (tabSetting.ShowTab)
            {
                UserControl tabContent = null!;
                switch (tabSetting.TabName)
                {
                    case "Traditionsartefakt":
                        tabContent = new TabContentArtifact();
                        break;
                    case "Zauberspeicher":
                        tabContent = new TabContentSpellStorage();
                        break;
                    case "Flammenschwert":
                        tabContent = new TabContentFlameSword();
                        break;
                    case "Charakter":
                        tabContent = new TabContentCharacter();
                        break;
                    case "Vertrautentier":
                        tabContent = new TabContentPet();
                        break;
                    case "Timer":
                        tabContent = new TabContentTimer();
                        break;
                    case "Modifikationsrechner":
                        tabContent = new TabContentMod();
                        break;
                }
                TabItem tabItem = new();
                tabItem.Header = tabSetting.TabName;
                tabItem.Content = tabContent;
                TabControl.Items.Add(tabItem);

            }
        }
    }

    private void MenuItemFileLoad_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void MenuItemFileSaveAs_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void MenuItemCharacterLoadFromTool_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void MenuItemCharacterLoadFromFile_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void MenuItemSettings_Click(object? sender, RoutedEventArgs e)
    {
        new SettingsWindow().Show();
    }

    private void MenuItemAbout_Click(object? sender, RoutedEventArgs e)
    {
#pragma warning disable CS8602
        string major = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
        string minor = Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
        string build = Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
#pragma warning restore CS8602
        new AboutWindow("Magehelper", major + "." + minor + "." + build).ShowDialog(this);
    }
}