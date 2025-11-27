using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;

namespace Magehelper.Views.Pages;

public sealed partial class MainPage : Page, IRecipient<CharacterLoadedMessage>, IRecipient<FileActionMessage>
{
    private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
    private readonly Core.Core _core = Core.Core.GetInstance();
    private readonly List<string> _loadedTabs = [];

    private MainPageViewModel ViewModel => (MainPageViewModel)DataContext;

    private ApplicationView View => ApplicationView.GetForCurrentView();

    public MainPage()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.RegisterAll(this);
    }


    public void Receive(CharacterLoadedMessage message)
    {
        View.Title = $"Magehelper - {message.Value.Name}";
        MenuCharacterLinkToFile.IsEnabled = true;
    }

    public void Receive(FileActionMessage message)
    {
        switch (message.Value)
        {
            case FileAction.New:
                View.Title = "Magehelper";
                break;
            case FileAction.Loaded:
                View.Title = $"Magehelper - {_core.FileName}";
                break;
            case FileAction.Changed:
                if (_core.FileChanged)
                {
                    View.Title = $"*{View.Title}";
                }
                else
                {
                    View.Title = View.Title.TrimStart('*');
                }
                break;
        }
    }

    private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        (XamlRoot!.Content as FrameworkElement)!.RequestedTheme = (string)_localSettings.Values["theme"] switch
        {
            "Light" => ElementTheme.Light,
            "Dark" => ElementTheme.Dark,
            _ => ElementTheme.Default
        };

        if (_loadedTabs.Count == 0)
        {
            LoadTabs();
        }
    }

    private async void MenuAbout_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            AboutDialog dialog = new()
            {
                XamlRoot = XamlRoot
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private async void MenuCharacterLoadFromFile_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            FileOpenPicker fileOpenPicker = new()
            {
                FileTypeFilter = { ".xml" },
                CommitButtonText = "Auswählen"
            };

            StorageFile file = await fileOpenPicker.PickSingleFileAsync();

            try
            {
                if (file == null)
                {
                    return;
                }

                WeakReferenceMessenger.Default.Send(new CharacterSelectedMessage(file.Path));
            }
            catch
            {
                throw new(file.Path + " ist keine gültige Helden-Software Datei oder kann nicht gelesen werden");
            }
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private async void MenuCharacterLoadFromTool_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            LoadFromToolDialog dialog = new()
            {
                XamlRoot = XamlRoot
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private void MenuCharacterLinkToFile_OnClick(object sender, RoutedEventArgs e)
    {
        if (_loadedTabs.Contains("Magehelper.Views.Tabs.TabCharacter"))
        {
            _core.Character!.ToggleLinkCharacterToFile();
        }
    }

    private void MenuFileTabCharacter_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabCharacter>();

        if (TabView.TabItems.OfType<TabCharacter>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabCharacter>().First();
            MenuCharacterLoadFromFile.IsEnabled = true;
            MenuCharacterLoadFromTool.IsEnabled = true;
        }
        else
        {
            MenuCharacterLoadFromFile.IsEnabled = false;
            MenuCharacterLoadFromTool.IsEnabled = false;
        }
    }


    private void MenuFileTabArtifacts_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabArtifact>();

        if (TabView.TabItems.OfType<TabArtifact>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabArtifact>().First();
        }
    }

    private void MenuFileTabArcaneGlyphs_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabArcaneGlyphs>();

        if (TabView.TabItems.OfType<TabArcaneGlyphs>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabArcaneGlyphs>().First();
        }
    }

    private void MenuFileTabFlameSword_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabFlameSword>();

        if (TabView.TabItems.OfType<TabFlameSword>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabFlameSword>().First();
        }
    }

    private void MenuFileTabMod_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabMod>();

        if (TabView.TabItems.OfType<TabMod>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabMod>().First();
        }
    }

    private void MenuFileTabPet_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabPet>();

        if (TabView.TabItems.OfType<TabPet>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabPet>().First();
        }
    }

    private void MenuFileTabSpellStorage_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabSpellStorage>();

        if (TabView.TabItems.OfType<TabSpellStorage>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabSpellStorage>().First();
        }
    }

    private void MenuFileTabTimer_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabTimer>();

        if (TabView.TabItems.OfType<TabTimer>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabTimer>().First();
        }
    }

    private void MenuFileTabTraditionalArtifact_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabTraditionArtifact>();

        if (TabView.TabItems.OfType<TabTraditionArtifact>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabTraditionArtifact>().First();
        }
    }

    private async void MenuFileLoad_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            FileOpenPicker fileOpenPicker = new()
            {
                FileTypeFilter = { ".magehelper" },
                CommitButtonText = "Öffnen"
            };

            StorageFile file = await fileOpenPicker.PickSingleFileAsync();

            if (file != null)
            {
                await LoadFile(file.Path);
            }
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private void MenuFileNew_OnClick(object sender, RoutedEventArgs e)
    {
        ResetTool();
        LoadTabs();
        ViewModel.NewFile();
    }

    private void MenuFileSave_OnClick(object sender, RoutedEventArgs e)
    {
        if (_core.FileName == string.Empty)
        {
            SaveFileAs();
        }
        else
        {
            ViewModel.SaveFile(_core.FileName);
        }
    }

    private void MenuFileSaveAs_OnClick(object sender, RoutedEventArgs e)
    {
        SaveFileAs();
    }

    private void MenuSettings_OnClick(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(SettingsPage));
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        try
        {
            if (e.Parameter != null && e.Parameter.ToString() != string.Empty)
            {
                await LoadFile(e.Parameter.ToString()!);
            }
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private async Task LoadFile(string path)
    {
        try
        {
            if (Settings.GetInstance().WarnOtherVersionFiles && _core.GetFileVersion(path) != _core.MagehelperFileVersion)
            {
                ContentDialog dialog = new()
                {
                    XamlRoot = XamlRoot,
                    Content = ViewModel.WarnOtherVersionFilesMessage(path),
                    PrimaryButtonText = "Ja",
                    CloseButtonText = "Nein"
                };

                ContentDialogResult contentDialogResult = await dialog.ShowAsync();

                if (contentDialogResult != ContentDialogResult.Primary)
                {
                    return;
                }
            }

            ResetTool();
            ViewModel.LoadFile(path);
        }
        catch
        {
            throw new(path + " ist keine gültige Magehelper Datei");
        }
    }

    private void LoadTabs()
    {
        foreach (string tab in Settings.GetInstance().DefaultTabs)
        {
            switch (tab)
            {
                case "Traditionsartefakt":
                    MenuFileTabTraditionalArtifact.IsChecked = true;
                    ToggleTab<TabTraditionArtifact>();

                    break;
                case "Zauberspeicher":
                    MenuFileTabSpellStorage.IsChecked = true;
                    ToggleTab<TabSpellStorage>();

                    break;
                case "Flammenschwert":
                    MenuFileTabFlameSword.IsChecked = true;
                    ToggleTab<TabFlameSword>();

                    break;
                case "Artefakte":
                    MenuFileTabArtifacts.IsChecked = true;
                    ToggleTab<TabArtifact>();
                    break;
                case "Zauberzeichen":
                    MenuFileTabArcaneGlyphs.IsChecked = true;
                    ToggleTab<TabArcaneGlyphs>();
                    break;
                case "Charakter":
                    MenuFileTabCharacter.IsChecked = true;
                    MenuCharacterLoadFromFile.IsEnabled = true;
                    MenuCharacterLoadFromTool.IsEnabled = true;
                    ToggleTab<TabCharacter>();

                    break;
                case "Vertrautentier":
                    MenuFileTabPet.IsChecked = true;
                    ToggleTab<TabPet>();

                    break;
                case "Timer":
                    MenuFileTabTimer.IsChecked = true;
                    ToggleTab<TabTimer>();

                    break;
                case "Modifikationsrechner":
                    MenuFileTabMod.IsChecked = true;
                    ToggleTab<TabMod>();

                    break;
            }
        }
    }

    private void ResetTool()
    {
        MenuFileTabTraditionalArtifact.IsChecked = false;
        MenuFileTabSpellStorage.IsChecked = false;
        MenuFileTabFlameSword.IsChecked = false;
        MenuFileTabCharacter.IsChecked = false;
        MenuFileTabPet.IsChecked = false;
        MenuFileTabTimer.IsChecked = false;
        MenuFileTabMod.IsChecked = false;
        MenuCharacterLoadFromFile.IsEnabled = false;
        MenuCharacterLoadFromTool.IsEnabled = false;
        _loadedTabs.Clear();
        TabView.TabItems.Clear();
        LoadTabs();
    }

    private async void SaveFileAs()
    {
        try
        {
            FileSavePicker fileSavePicker = new()
            {
                SuggestedFileName = string.IsNullOrEmpty(_core.FileName)
                    ? ViewModel.LoadedCharacter
                    : Path.GetFileNameWithoutExtension(_core.FileName),
                FileTypeChoices =
                {
                    {"Magehelper Datei", new List<string> {".magehelper"}}
                },
                CommitButtonText = "Speichern"
            };

            StorageFile file = await fileSavePicker.PickSaveFileAsync();

            try
            {
                if (file == null)
                {
                    return;
                }

                ViewModel.SaveFile(file.Path);
            }
            catch
            {
                throw new(file.Path + " ist keine gültige Magehelper Datei");
            }
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private void ToggleTab<T>() where T : TabViewItem, new()
    {
        if (!TabView.TabItems.OfType<T>().Any())
        {
            _loadedTabs.Add(typeof(T).ToString());
            TabView.TabItems.Add(new T());
        }
        else
        {
            _loadedTabs.Remove(typeof(T).ToString());
            TabView.TabItems.Remove(TabView.TabItems.OfType<T>().First());
        }
    }
}
