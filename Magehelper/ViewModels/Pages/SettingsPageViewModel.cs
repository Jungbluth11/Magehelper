namespace Magehelper.ViewModels.Pages;

public partial class SettingsPageViewModel : ObservableObject, IRecipient<ConfigAddedMessage>
{
    private readonly bool _isInitalized;
    private readonly Settings _settings = Settings.GetInstance();

    [ObservableProperty] private bool _allowRemoveSpells;
    [ObservableProperty] private bool _autoinstallUpdates;
    [ObservableProperty] private bool _checkForUpdates;
    [ObservableProperty] private bool _useHeldentoolNames;
    [ObservableProperty] private bool _warnOtherVersionFiles;
    [ObservableProperty] private string _currentConfigName;
    [ObservableProperty] private TabSetting[] _tabSettings = [];
    [ObservableProperty] private bool _isDarkTheme;

    public ObservableCollection<HomeBrewTab> HomeBrewTabs { get; set; } = [];
    public ObservableCollection<string> ConfigNames { get; }

    public SettingsPageViewModel()
    {
        ConfigNames = new(_settings.ConfigNames);
        LoadConfig(_settings.CurrentConfigName);
        _currentConfigName = _settings.CurrentConfigName;
        IsDarkTheme = _settings.Theme == ElementTheme.Dark;
        WeakReferenceMessenger.Default.Register(this);
        _isInitalized = true;
    }

    partial void OnIsDarkThemeChanged(bool value)
    {
        _settings.Theme = value ? ElementTheme.Dark : ElementTheme.Light;
        _settings.SettingsChanged = true;
    }

    public void Receive(ConfigAddedMessage message)
    {
        ConfigNames.Add(message.Value);
    }

    public void Save()
    {
        _settings.CurrentSettingsPath = Path.Combine(_settings.BaseSettingsPath , CurrentConfigName);
        _settings.SaveConfigFile();
        _settings.SetCurrentConfig();
    }

    public void SetSpellCost(string spellName, int spellCost)
    {
        string settingsFile = (from tab in HomeBrewTabs
                               from spell in tab.Spells
                               where spell.Name == spellName
                               select tab.SettingsFile).First();

        ArtifactSpell artifactSpell = (from tab in HomeBrewTabs
                                       from spell in tab.Spells
                                       where spell.Name == spellName
                                       select spell).First();

        int i = SettingsHelper.ArtifactSpells[settingsFile].IndexOf(artifactSpell);
        SettingsHelper.ArtifactSpells[settingsFile][i].Cost = spellCost;
        _settings.SettingsChanged = true;
    }

    public void SetTabSetting(string tabName, bool showTab)
    {
        int i = TabSettings.IndexOf(TabSettings.First(t => t.TabName == tabName));
        _settings.TabSettings[i].ShowTab = showTab;
    }

    private void LoadConfig(string configName)
    {
        _settings.LoadConfig(configName);
        TabSettings = _settings.TabSettings;
        AllowRemoveSpells = _settings.AllowRemoveSpells;
        UseHeldentoolNames = _settings.UseHeldentoolNames;
        CheckForUpdates = _settings.CheckForUpdates;
        AutoinstallUpdates = _settings.AutoInstallUpdates;
        WarnOtherVersionFiles = _settings.WarnOtherVersionFiles;
        HomeBrewTabs.Clear();

        foreach (string file in _settings.ArtifactFiles)
        {
            HomeBrewTabs.Add(new()
            { Name = SettingsHelper.TraditionArtifactName[file], SettingsFile = file, Spells = SettingsHelper.ArtifactSpells[file] });
        }

        if (_isInitalized)
        {
            _settings.SettingsChanged = true;
        }

    }

    partial void OnAllowRemoveSpellsChanged(bool value)
    {
        if (!_isInitalized)
        {
            return;
        }

        _settings.AllowRemoveSpells = value;
        _settings.SettingsChanged = true;
    }

    partial void OnAutoinstallUpdatesChanged(bool value)
    {
        if (!_isInitalized)
        {
            return;
        }

        _settings.AutoInstallUpdates = value;
        _settings.SettingsChanged = true;
    }

    partial void OnCheckForUpdatesChanged(bool value)
    {
        if (!_isInitalized)
        {
            return;
        }

        _settings.CheckForUpdates = value;
        _settings.SettingsChanged = true;
    }

    partial void OnCurrentConfigNameChanged(string value)
    {
        LoadConfig(value);
    }

    partial void OnUseHeldentoolNamesChanged(bool value)
    {
        if (!_isInitalized)
        {
            return;
        }

        _settings.UseHeldentoolNames = value;
        _settings.SettingsChanged = true;
    }

    partial void OnWarnOtherVersionFilesChanged(bool value)
    {
        if (!_isInitalized)
        {
            return;
        }

        _settings.WarnOtherVersionFiles = value;
        _settings.SettingsChanged = true;
    }

    [RelayCommand]
    private void ForceUpdate()
    {
        Updater.Update().RunSynchronously();
    }
}
