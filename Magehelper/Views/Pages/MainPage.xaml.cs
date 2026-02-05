using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;

namespace Magehelper.Views.Pages;

public sealed partial class MainPage : Page, IRecipient<CharacterLoadedMessage>, IRecipient<FileActionMessage>
{
    private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
    private readonly Core.Core _core = Core.Core.Instance;
    private readonly List<string> _loadedTabs = [];
    private ApplicationView View => ApplicationView.GetForCurrentView();
    private MainPageViewModel ViewModel => (MainPageViewModel)DataContext;

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
            LoadTabs(Settings.Instance.DefaultTabs);
        }

        if(Settings.Instance.CheckForUpdates && Updater.CheckForUpdates())
        {
            ShowUpdateDialog().RunSynchronously();
        }
    }

    private async Task ShowUpdateDialog()
    {
        ContentDialog dialog = new()
        {
            XamlRoot = XamlRoot,
            Content = "Es ist eine neue Version von Magehelper verfügbar. Soll sie installiert werden?",
            PrimaryButtonText = "Ja",
            CloseButtonText = "Nein"
        };

        ContentDialogResult contentDialogResult = await dialog.ShowAsync();

        if (contentDialogResult != ContentDialogResult.Primary)
        {
            return;
        }

        await Updater.Update();
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

    private void MenuCharacterLinkToFile_OnClick(object sender, RoutedEventArgs e)
    {
        if (_loadedTabs.Contains("Charakter"))
        {
            _core.Character!.ToggleLinkCharacterToFile();
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
        ResetTool(Settings.Instance.DefaultTabs);
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

    private void MenuFileTabArcaneGlyphs_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabArcaneGlyphs>();

        if (TabView.TabItems.OfType<TabArcaneGlyphs>().Any())
        {
            GoToTab("Zauberzeichen");
        }
    }

    private void MenuFileTabArtifacts_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabArtifact>();

        if (TabView.TabItems.OfType<TabArtifact>().Any())
        {
            GoToTab("Artefakte");
        }
    }

    private void MenuFileTabCharacter_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabCharacter>();

        if (TabView.TabItems.OfType<TabCharacter>().Any())
        {
            GoToTab("Charakter");
            MenuCharacterLoadFromFile.IsEnabled = true;
            MenuCharacterLoadFromTool.IsEnabled = true;
        }
        else
        {
            MenuCharacterLoadFromFile.IsEnabled = false;
            MenuCharacterLoadFromTool.IsEnabled = false;
        }
    }

    private void MenuFileTabFlameSword_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabFlameSword>();

        if (TabView.TabItems.OfType<TabFlameSword>().Any())
        {
            GoToTab("Flammenschwert");
        }
    }

    private void MenuFileTabMod_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabMod>();

        if (TabView.TabItems.OfType<TabMod>().Any())
        {
            GoToTab("Modifikationsrechner");
        }
    }

    private void MenuFileTabPet_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabPet>();

        if (TabView.TabItems.OfType<TabPet>().Any())
        {
            GoToTab("Vertrautentier");
        }
    }

    private void MenuFileTabSpellStorage_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabSpellStorage>();

        if (TabView.TabItems.OfType<TabSpellStorage>().Any())
        {
            GoToTab("Zauberspeicher");
        }
    }

    private void MenuFileTabTimer_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabTimer>();

        if (TabView.TabItems.OfType<TabTimer>().Any())
        {
            GoToTab("Timer");
        }
    }

    private void MenuFileTabTraditionalArtifact_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabTraditionArtifact>();

        if (TabView.TabItems.OfType<TabTraditionArtifact>().Any())
        {
            GoToTab("Traditionsartefakt");
        }
    }

    private void MenuSettings_OnClick(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(SettingsPage), TabView.SelectedItem.ToString());
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        try
        {
            if (e.Parameter == null || e.Parameter.ToString() == string.Empty)
            {
                return;
            }

            if (File.Exists(e.Parameter.ToString()))
            {
                await LoadFile(e.Parameter.ToString()!);
            }
            else if (SettingsHelper.TabName.TryGetValue(e.Parameter.ToString()!, out string? tabName) && tabName != null)
            {
                string[] tabList = _core.FileTabs.Any()
                    ? _core.FileTabs.ToArray()
                    : Settings.Instance.DefaultTabs;

                LoadTabs(tabList);
                GoToTab(tabName);
            }
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private void GoToTab(string tabName)
    {
        TabView.SelectedItem = tabName switch
        {
            "Traditionsartefakt" => TabView.TabItems.OfType<TabTraditionArtifact>().First(),
            "Zauberspeicher" => TabView.TabItems.OfType<TabSpellStorage>().First(),
            "Flammenschwert" => TabView.TabItems.OfType<TabFlameSword>().First(),
            "Artefakte" => TabView.TabItems.OfType<TabArtifact>().First(),
            "Zauberzeichen" => TabView.TabItems.OfType<TabArcaneGlyphs>().First(),
            "Charakter" => TabView.TabItems.OfType<TabCharacter>().First(),
            "Vertrautentier" => TabView.TabItems.OfType<TabPet>().First(),
            "Timer" => TabView.TabItems.OfType<TabTimer>().First(),
            "Modifikationsrechner" => TabView.TabItems.OfType<TabMod>().First(),
            _ => TabView.SelectedItem
        };
    }

    private async Task LoadFile(string path)
    {
        try
        {
            if (Settings.Instance.WarnOtherVersionFiles && _core.GetFileVersion(path) != _core.MagehelperFileVersion)
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

            ViewModel.LoadFile(path);

            string[] tabList = _core.FileTabs.Any()
                ? _core.FileTabs.ToArray()
                : Settings.Instance.DefaultTabs;

            ResetTool(tabList);
        }
        catch
        {
            throw new(path + " ist keine gültige Magehelper Datei");
        }
    }

    private void LoadTabs(string[] tabList)
    {
        if (_loadedTabs.SequenceEqual(tabList))
        {
            return;
        }

        _loadedTabs.Clear();
        TabView.TabItems.Clear();

        foreach (string tab in tabList)
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

        TabView.SelectedIndex = 0;
    }

    private void ResetTool(string[] tabList)
    {
        MenuFileTabTraditionalArtifact.IsChecked = false;
        MenuFileTabSpellStorage.IsChecked = false;
        MenuFileTabFlameSword.IsChecked = false;
        MenuFileTabArtifacts.IsChecked = false;
        MenuFileTabArcaneGlyphs.IsChecked = false;
        MenuFileTabCharacter.IsChecked = false;
        MenuFileTabPet.IsChecked = false;
        MenuFileTabTimer.IsChecked = false;
        MenuFileTabMod.IsChecked = false;
        MenuCharacterLoadFromFile.IsEnabled = false;
        MenuCharacterLoadFromTool.IsEnabled = false;
        LoadTabs(tabList);
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

                if (!_core.FileTabs.Any())
                {
                    _core.FileTabs.AddRange(_loadedTabs);
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
        string tabName = SettingsHelper.TabName[typeof(T).ToString()];

        if (!TabView.TabItems.OfType<T>().Any())
        {
            _loadedTabs.Add(tabName);
            TabView.TabItems.Add(new T());

            if (_core.FileTabs.Any())
            {
                _core.FileTabs.Add(tabName);
            }
        }
        else
        {
            _loadedTabs.Remove(tabName);
            TabView.TabItems.Remove(TabView.TabItems.OfType<T>().First());

            if (_core.FileTabs.Any())
            {
                _core.FileTabs.Remove(tabName);
            }
        }
    }
}
