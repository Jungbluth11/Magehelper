using System.Text.Json;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;

namespace Magehelper.Views.Pages;

public sealed partial class MainPage : Page, IRecipient<CharacterLoadedMessage>
{
    private readonly Core.Core _core = Core.Core.GetInstance();
    private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
    private readonly List<string> _loadedTabs = [];
    private MainPageViewModel ViewModel => (MainPageViewModel)DataContext;

    public MainPage()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(CharacterLoadedMessage message)
    {
        ApplicationView.GetForCurrentView().Title = $"Magehelper - {message.Value!}";
    }

    private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        (XamlRoot!.Content as FrameworkElement)!.RequestedTheme = (string)_localSettings.Values["theme"] switch
        {
            "Light" => ElementTheme.Light,
            "Dark" => ElementTheme.Dark,
            _ => ElementTheme.Default,
        };

        LoadTabs();
    }

    private void MenuFileNew_OnClick(object sender, RoutedEventArgs e)
    {
        ResetTool();
        LoadTabs();
        ViewModel.NewFile();
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

            try
            {
                if (file == null)
                {
                    return;
                }

                ViewModel.LoadFile(file.Path);
                ResetTool();
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

    private void MenuCharacterTabTraditionalArtifact_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabArtifact>();

        if (TabView.TabItems.OfType<TabArtifact>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabArtifact>().First();
        }

    }

    private void MenuCharacterTabSpellStorage_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabSpellStorage>();
        if (TabView.TabItems.OfType<TabSpellStorage>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabSpellStorage>().First();
        }
    }

    private void MenuCharacterTabFlameSword_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabFlameSword>();
        if (TabView.TabItems.OfType<TabFlameSword>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabFlameSword>().First();
        }
    }

    private void MenuCharacterTabCharacter_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabCharacter>();
        if (TabView.TabItems.OfType<TabCharacter>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabCharacter>().First();
        }
    }

    private void MenuCharacterTabPet_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabPet>();
        if (TabView.TabItems.OfType<TabPet>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabPet>().First();
        }
    }

    private void MenuCharacterTabTimer_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabTimer>();
        if (TabView.TabItems.OfType<TabTimer>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabTimer>().First();
        }
    }

    private void MenuCharacterTabMod_OnClick(object sender, RoutedEventArgs e)
    {
        ToggleTab<TabMod>();
        if (TabView.TabItems.OfType<TabMod>().Any())
        {
            TabView.SelectedItem = TabView.TabItems.OfType<TabMod>().First();
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

    private void MenuSettings_OnClick(object sender, RoutedEventArgs e)
    {
        (XamlRoot!.Content as Frame)!.Navigate(typeof(SettingsPage));
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

    private void LoadTabs()
    {
        foreach (string tab in JsonSerializer.Deserialize<string[]>((string)_localSettings.Values["loadedTabs"])!)
        {
            switch (tab)
            {
                case "Traditionsartefakt":
                    MenuCharacterTabTraditionalArtifact.IsChecked = true;
                    ToggleTab<TabArtifact>();

                    break;
                case "Zauberspeicher":
                    MenuCharacterTabSpellStorage.IsChecked = true;
                    ToggleTab<TabSpellStorage>();

                    break;
                case "Flammenschwert":
                    MenuCharacterTabFlameSword.IsChecked = true;
                    ToggleTab<TabFlameSword>();

                    break;
                case "Charakter":
                    MenuCharacterTabCharacter.IsChecked = true;
                    ToggleTab<TabCharacter>();

                    break;
                case "Vertrautentier":
                    MenuCharacterTabPet.IsChecked = true;
                    ToggleTab<TabPet>();

                    break;
                case "Timer":
                    MenuCharacterTabTimer.IsChecked = true;
                    ToggleTab<TabTimer>();

                    break;
                case "Modifikationsrechner":
                    MenuCharacterTabMod.IsChecked = true;
                    ToggleTab<TabMod>();

                    break;
            }
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

        _localSettings.Values["loadedTabs"] = JsonSerializer.Serialize(_loadedTabs.ToArray().Select(s => SettingsHelper.TabName[s]));
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

    private void ResetTool()
    {
        MenuCharacterTabTraditionalArtifact.IsChecked = false;
        MenuCharacterTabSpellStorage.IsChecked = false;
        MenuCharacterTabFlameSword.IsChecked = false;
        MenuCharacterTabCharacter.IsChecked = false;
        MenuCharacterTabPet.IsChecked = false;
        MenuCharacterTabTimer.IsChecked = false;
        MenuCharacterTabMod.IsChecked = false;
    }
}
