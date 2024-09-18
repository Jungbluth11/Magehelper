#pragma warning disable CS8602
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using DSAUtils.UI;
using MsBox.Avalonia.Base;

namespace Magehelper.Avalonia.Views.Windows;

public partial class MainWindow : Window
{
    private readonly TopLevel? topLevel;
    private readonly MainWindowViewModel viewModel;
    public TabContentArtifact? TabContentArtifact { get; set; }
    public TabContentSpellStorage? TabContentSpellStorage { get; set; }
    public TabContentFlameSword? TabContentFlameSword { get; set; }
    public TabContentCharacter? TabContentCharacter { get; set; }
    public TabContentPet? TabContentPet { get; set; }
    public TabContentTimer? TabContentTimer { get; set; }

    public MainWindow()
    {
        topLevel = GetTopLevel(this);
        viewModel = MainWindowViewModel.Instance; ;
        DataContext = viewModel;
        InitializeComponent();
        TabSetting[] tabSettings = viewModel.Settings.GetTabSettings();
        foreach (TabSetting tabSetting in tabSettings)
        {
            if (tabSetting.ShowTab)
            {
                UserControl tabContent = null!;
                switch (tabSetting.TabName)
                {
                    case "Traditionsartefakt":
                        TabContentArtifact = new TabContentArtifact();
                        tabContent = TabContentArtifact;
                        break;
                    case "Zauberspeicher":
                        TabContentSpellStorage = new TabContentSpellStorage();
                        tabContent = TabContentSpellStorage;
                        break;
                    case "Flammenschwert":
                        TabContentFlameSword = new TabContentFlameSword();
                        tabContent = TabContentFlameSword;
                        break;
                    case "Charakter":
                        TabContentCharacter = new TabContentCharacter();
                        tabContent = TabContentCharacter;
                        MenuItemCharacter.IsEnabled = true;
                        break;
                    case "Vertrautentier":
                        TabContentPet = new TabContentPet();
                        tabContent = TabContentPet;
                        break;
                    case "Timer":
                        TabContentTimer = new TabContentTimer();
                        tabContent = TabContentTimer;
                        break;
                    case "Modifikationsrechner":
                        tabContent = new TabContentMod();
                        break;
                }
                TabItem tabItem = new()
                {
                    Header = tabSetting.TabName,
                    Content = tabContent
                };
                TabControl.Items.Add(tabItem);

            }
        }
    }

    public void ResetTool()
    {
        if (TabContentArtifact != null)
        {
            TabContentArtifact.ResetTab();
        }
        if (TabContentSpellStorage != null)
        {
            TabContentSpellStorage.ResetTab();
        }
        if (TabContentFlameSword != null)
        {
            TabContentFlameSword.ResetTab();
        }
        if (TabContentCharacter != null)
        {
            TabContentCharacter.ResetTab();
        }
        if (TabContentPet != null)
        {
            TabContentPet.ResetTab();
        }
        if (TabContentTimer != null)
        {
            TabContentTimer.ResetTab();
        }
    }

    private void SelectFileSaveOption()
    {
        if (viewModel.Core.FileName != null)
        {
            viewModel.SaveFile(viewModel.Core.FileName);
        }
        else
        {
            SaveFileDialog();
        }
    }

    private async void SaveFileDialog()
    {
        IStorageFile? file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Datei Speichern - Magehelper",
            FileTypeChoices = [MainWindowViewModel.MagehelperFileType],
            DefaultExtension = ".magehelper",
            ShowOverwritePrompt = true
        });
        if (file != null)
        {
            viewModel.SaveFile(file.Path.AbsolutePath);
        }
    }

    private async void MenuItemFileNew_Click(object? sender, RoutedEventArgs e)
    {
        if (viewModel.Core.FileChanged)
        {
            IMsBox<string> messageBox = MessageBoxGenerator.GetMessageBox("Sollen die Änderungen gespeichert werden?", MessageBoxGenerator.Buttons.YesNoCancel, MsBox.Avalonia.Enums.Icon.Question);
            string result = await messageBox.ShowAsync();

            if (result == "Abbrechen")
            {
                return;
            }
            if (result == "Ja")
            {
                SelectFileSaveOption();
            }
        }
        ResetTool();
    }

    private async void MenuItemFileLoad_Click(object? sender, RoutedEventArgs e)
    {
        if (viewModel.Core.FileChanged)
        {
            IMsBox<string> messageBox = MessageBoxGenerator.GetMessageBox("Sollen die Änderungen gespeichert werden?", MessageBoxGenerator.Buttons.YesNoCancel, MsBox.Avalonia.Enums.Icon.Question);
            string result = await messageBox.ShowAsync();

            if (result == "Abbrechen")
            {
                return;
            }
            if (result == "Ja")
            {
                SelectFileSaveOption();
            }
        }
        IReadOnlyList<IStorageFile> files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Datei Öffnen - Magehelper",
            FileTypeFilter = [MainWindowViewModel.MagehelperFileType],
            AllowMultiple = false
        });

        if (files.Count == 1)
        {
            string fileVersion = viewModel.Core.GetFileVersion(files[0].Path.AbsolutePath);
            if (fileVersion != viewModel.Core.MagehelperFileVersion && viewModel.Settings.WarnOtherVersionFiles)
            {
                bool isLegacy = false;
                if (fileVersion == "0")
                {
                    isLegacy = true;
                }
                IMsBox<string> WarnOtherVersionFileSDialog = MessageBoxGenerator.GetMessageBox(viewModel.WarnOtherVersionFilesMessage(isLegacy), MessageBoxGenerator.Buttons.YesNo, MsBox.Avalonia.Enums.Icon.Question);
                string DialogResult = await WarnOtherVersionFileSDialog.ShowAsync();
                if(DialogResult == "Nein")
                {
                    return;
                }
            }
            ResetTool();
            viewModel.LoadFile(files[0].Path.AbsolutePath);
        }
    }

    private void MenuItemFileSave_Click(object? sender, RoutedEventArgs e)
    {
        SelectFileSaveOption();
    }

    private void MenuItemFileSaveAs_Click(object? sender, RoutedEventArgs e)
    {
        SaveFileDialog();
    }

    private void MenuItemCharacterLoadFromTool_Click(object? sender, RoutedEventArgs e)
    {
        new LoadFromToolWindow().Show();
    }

    private async void MenuItemCharacterLoadFromFile_Click(object? sender, RoutedEventArgs e)
    {
        IReadOnlyList<IStorageFile> files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Datei Öffnen - Magehelper",
            FileTypeFilter = [MainWindowViewModel.MagehelperFileType],
            AllowMultiple = false
        });

        if (files.Count == 1)
        {
            TabContentCharacter.ResetTab();
            TabContentCharacterViewModel.Instance.LoadCharacterFromFile(files[0].Path.AbsolutePath);
        }
    }

    private void MenuItemSettings_Click(object? sender, RoutedEventArgs e)
    {
        new SettingsWindow().Show();
    }

    private void MenuItemAbout_Click(object? sender, RoutedEventArgs e)
    {
        string major = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
        string minor = Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
        string build = Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
        new AboutWindow("Magehelper", major + "." + minor + "." + build).ShowDialog(this);
    }

#pragma warning disable CS1998 //suppress error message for missing 'await' when in debug
    private async void Window_Closing(object? sender, WindowClosingEventArgs e)
#pragma warning restore CS1998 
    {
#if RELEASE
        if (viewModel.Core.FileChanged)
        {
            IMsBox<string> messageBox = MessageBoxGenerator.GetMessageBox("Sollen die Änderungen gespeichert werden?", MessageBoxGenerator.Buttons.YesNoCancel, MsBox.Avalonia.Enums.Icon.Question);
            string result = await messageBox.ShowAsync();

            if (result == "Abbrechen")
            {
                return;
            }
            if (result == "Ja")
            {
                SelectFileSaveOption();
            }
        }
#endif
    }
}