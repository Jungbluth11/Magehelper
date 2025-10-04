namespace Magehelper.ViewModels.Pages;

public partial class SettingsPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _currentConfigName;
    [ObservableProperty]
    private bool _allowRemoveSpells;
    [ObservableProperty]
    private bool _useHeldentoolNames;
    [ObservableProperty]
    private bool _checkForUpdates;
    [ObservableProperty]
    private bool _autoinstallUpdates;
    [ObservableProperty]
    private bool _warnOtherVersionFiles;
    [ObservableProperty]
    private IEnumerable<TabSetting> _tabSettings = [];
    private readonly Settings settings;
    public ObservableCollection<string> ConfigNames { get; }
    public ObservableCollection<TabItem> Tabs { get; set; } = [];


    public SettingsPageViewModel()
    {
        settings = new Settings();
        ConfigNames = new ObservableCollection<string>(settings.ConfigNames);
        LoadConfig();
        _currentConfigName = Path.GetFileName(settings.CurrentSettingsPath);
    }

    public void AddConfig(string configName)
    {
        settings.AddConfig(configName);
        ConfigNames.Add(configName);
    }

    private void LoadConfig(string? configName = null)
    {
        if (configName != null)
        {
            settings.LoadConfig(configName);
        }
        TabSettings = settings.TabSettings;
        AllowRemoveSpells = settings.AllowRemoveSpells;
        UseHeldentoolNames = settings.UseHeldentoolNames;
        CheckForUpdates = settings.CheckForUpdates;
        AutoinstallUpdates = settings.AutoInstallUpdates;
        WarnOtherVersionFiles = settings.WarnOtherVersionFiles;
        Tabs.Clear();
        StaffSettingsControl staffSettings = new(settings.StaffSpells);
        ArtifactSettingsControl crystalBallSettings = new(settings.CrystalBallSpells);
        ArtifactSettingsControl bowlSettings = new(settings.BowlSpells);
        ArtifactSettingsControl boneCubSettings = new(settings.BoneCubSpells);
        ArtifactSettingsControl ringOfLifeSettings = new(settings.RingOfLifeSpells);
        ArtifactSettingsControl obsidianDaggerSettings = new(settings.ObsidianDaggerSpells);
        IArtifactSettingsTab[] artifactSettings =
        [
            staffSettings,
            crystalBallSettings,
            bowlSettings,
            boneCubSettings,
            ringOfLifeSettings,
            obsidianDaggerSettings
        ];
        foreach (IArtifactSettingsTab setting in artifactSettings)
        {
            TabItem tabItem = new()
            {
                Header = setting.SettingsHeader,
                Content = setting
            };
            Tabs.Add(tabItem);
        }
    }

    [RelayCommand]
    private async Task ForceUpdate()
    {
        Updater updater = new();
        if (updater.CheckForUpdates())
        {
            updater.Update();
        }
        else
        {
            await MessageBoxGenerator.GetMessageBox("Magehelper ist auf dem neuesten Stand.", MessageBoxGenerator.Buttons.OK).ShowAsync();
        }
    }

    [RelayCommand]
    private async Task Save(Window window)
    {
        if (settings.SettingsChanged)
        {
            IMsBox<string> messageBox = MessageBoxGenerator.GetMessageBox("Sollen die Änderungen gespeichert werden?", MessageBoxGenerator.Buttons.YesNo, Icon.Question);
            string result = await messageBox.ShowAsync();
            if (result == "Ja")
            {
                settings.SaveConfigFile();
                settings.SetCurrentConfig();
            }
            IMsBox<string> messageBoxRestart = MessageBoxGenerator.GetMessageBox("Die Änderungen werden erst nach einem Neustart wirksam.\nSoll jetzt neu gestartet werden?", MessageBoxGenerator.Buttons.YesNo, Icon.Question);
            string resultRestart = await messageBoxRestart.ShowAsync();
            if (resultRestart == "Ja")
            {
                if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp)
                {
                    desktopApp.Shutdown();
                }
            }
            window.Close();
        }
    }

    [RelayCommand]
    private void Cancel(Window window)
    {
        window.Close();
    }

    partial void OnCurrentConfigNameChanging(string value)
    {
        if (settings.SettingsChanged)
        {
            IMsBox<string> messageBox = MessageBoxGenerator.GetMessageBox("Sollen die Änderungen gespeichert werden?", MessageBoxGenerator.Buttons.YesNo, Icon.Question);
            string result = messageBox.ShowAsync().Result;
            if (result == "Ja")
            {
                settings.SaveConfigFile();
            }
            LoadConfig(value);
        }
    }

    partial void OnAllowRemoveSpellsChanged(bool value)
    {
        settings.AllowRemoveSpells = value;
        settings.SettingsChanged = true;
    }

    partial void OnUseHeldentoolNamesChanged(bool value)
    {
        settings.UseHeldentoolNames = value;
        settings.SettingsChanged = true;
    }

    partial void OnCheckForUpdatesChanged(bool value)
    {
        settings.CheckForUpdates = value;
        settings.SettingsChanged = true;
    }

    partial void OnAutoinstallUpdatesChanged(bool value)
    {
        settings.AutoInstallUpdates = value;
        settings.SettingsChanged = true;
    }

    partial void OnWarnOtherVersionFilesChanged(bool value)
    {
        settings.WarnOtherVersionFiles = value;
        settings.SettingsChanged = true;
    }

    partial void OnTabSettingsChanged(IEnumerable<TabSetting> value)
    {
        settings.SettingsChanged = true;
    }
}
