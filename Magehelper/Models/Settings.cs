using Windows.UI.ViewManagement;

namespace Magehelper.Models;

public class Settings
{
    private static Settings? _instance;
    private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
    private readonly List<string> _configNames = [];
    private ElementTheme _theme;

    public string[] ArtifactFiles =>
    [
        "staff.json",
        "crystalBall.json",
        "bowl.json",
        "boneCub.json",
        "ringOfLife.json",
        "obsidianDagger.json"
    ];

    public bool AllowRemoveSpells { get; set; }
    public bool AutoInstallUpdates { get; set; }
    public string BaseSettingsPath { get; }
    public ArtifactSpell[] BoneCubSpells { get; set; } = [];
    public ArtifactSpell[] BowlSpells { get; set; } = [];
    public bool ChangeTraditionArtifactTabName { get; set; }
    public bool CheckForUpdates { get; set; }
    public ReadOnlyCollection<string> ConfigNames => _configNames.AsReadOnly();
    public ArtifactSpell[] CrystalBallSpells { get; set; } = [];
    public string CurrentSettingsPath { get; set; }
    public ArtifactSpell[] ObsidianDaggerSpells { get; set; } = [];
    public ArtifactSpell[] RingOfLifeSpells { get; set; } = [];
    public bool SettingsChanged { get; set; }
    public int SpellStoragePoints { get; set; }
    public ArtifactSpell[] StaffSpells { get; set; } = [];
    public TabSetting[] TabSettings { get; private set; } = [];
    public string[] DefaultTabs => [..from tabSetting in TabSettings where tabSetting.ShowTab select tabSetting.TabName];
    public bool UseHeldentoolNames { get; set; }
    public bool WarnOtherVersionFiles { get; set; }
    public string CurrentConfigName { get; private set; }
    public ElementTheme Theme {
        get => _theme;
        set
        {
            _theme = value;
            _localSettings.Values["theme"] = value.ToString();
        } 
    }

    private Settings()
    {
        BaseSettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "magehelper", "config");
#if DEBUG
        BaseSettingsPath = Path.Combine(AppContext.BaseDirectory, "BaseSettings");
        CurrentSettingsPath = Path.Combine(AppContext.BaseDirectory, "BaseSettings");
        CurrentConfigName = "Standard";
#endif
        if (_localSettings.Values["CurrentSettingsPath"] == null)
        {
            // ReSharper disable once UnusedVariable --- suppress warning for debug build
            string defaultConfigPath = AddConfig("Standard");
#if RELEASE
            CurrentSettingsPath = defaultConfigPath;
#endif
        }
        else
        {
#if RELEASE
            CurrentSettingsPath = _localSettings.Values["CurrentSettingsPath"].ToString()!;
#endif
        }

        SetCurrentConfig();
        LoadConfig(CurrentConfigName);


        if (_localSettings.Values["theme"] is string theme)
        {
            Theme = theme switch
            {
                "Light" => ElementTheme.Light,
                "Dark" => ElementTheme.Dark,
                _ => ElementTheme.Default
            };
        }
        else
        {
            string defaultTheme = new UISettings().GetColorValue(UIColorType.Background).ToString();
            Theme = defaultTheme == "#FF000000" ? ElementTheme.Dark : ElementTheme.Light;
            _localSettings.Values["theme"] = Theme.ToString();
        }


        string[] directoryList = Directory.GetDirectories(BaseSettingsPath);

        foreach (string directory in directoryList)
        {
            _configNames.Add(Path.GetFileName(directory));
        }
    }

    public static Settings GetInstance()
    {
        _instance ??= new();

        return _instance;
    }

    public string AddConfig(string configName)
    {
        DirectoryInfo configDirectory = Directory.CreateDirectory(Path.Combine(BaseSettingsPath, configName));

        foreach (string file in ArtifactFiles.Append("tabSettings.json").Append("appSettings.json"))
        {
            File.Copy(Path.Combine(
                    AppContext.BaseDirectory,
                    "BaseSettings",
                    file),
                Path.Combine(
                    configDirectory.FullName,
                    file),
                true);
        }

        return configDirectory.FullName;
    }

    public TabSetting[] GetTabSettings()
    {
        return JsonSerializer.Deserialize<TabSetting[]>(
            File.ReadAllText(Path.Combine(CurrentSettingsPath, "tabSettings.json")))!;
    }

    public void LoadConfig(string configName)
    {
#if DEBUG
        configName = string.Empty;
#endif
        CurrentSettingsPath = Path.Combine(BaseSettingsPath, configName);

        foreach (string file in ArtifactFiles.Append("tabSettings.json").Prepend("appSettings.json"))
        {
            switch (file)
            {
                case "staff.json":
                    StaffSpells = ReadSettingsFile<ArtifactSpell[]>(file);
                    break;
                case "crystalBall.json":
                    CrystalBallSpells = ReadSettingsFile<ArtifactSpell[]>(file);
                    break;
                case "bowl.json":
                    BowlSpells = ReadSettingsFile<ArtifactSpell[]>(file);
                    break;
                case "boneCub.json":
                    BoneCubSpells = ReadSettingsFile<ArtifactSpell[]>(file);
                    break;
                case "ringOfLife.json":
                    RingOfLifeSpells = ReadSettingsFile<ArtifactSpell[]>(file);
                    break;
                case "obsidianDagger.json":
                    ObsidianDaggerSpells = ReadSettingsFile<ArtifactSpell[]>(file);
                    break;
                case "tabSettings.json":
                    TabSettings = ReadSettingsFile<TabSetting[]>(file);
                    break;
                case "appSettings.json":
                    AppSettings appSettings = ReadSettingsFile<AppSettings>(file);
                    SpellStoragePoints = appSettings.SpellStoragePoints;
                    AllowRemoveSpells = appSettings.AllowRemoveSpells;
                    UseHeldentoolNames = appSettings.UseHeldentoolNames;
                    CheckForUpdates = appSettings.CheckForUpdates;
                    AutoInstallUpdates = appSettings.AutoInstallUpdates;
                    WarnOtherVersionFiles = appSettings.WarnOtherVersionFiles;
                    break;

            }
        }
    }

    public void SaveConfigFile()
    {
        AppSettings appSettings = new()
        {
            SpellStoragePoints = SpellStoragePoints,
            AllowRemoveSpells = AllowRemoveSpells,
            UseHeldentoolNames = UseHeldentoolNames,
            CheckForUpdates = CheckForUpdates,
            AutoInstallUpdates = AutoInstallUpdates,
            WarnOtherVersionFiles = WarnOtherVersionFiles
        };

        foreach (string file in ArtifactFiles.Append("tabSettings.json").Append("appSettings.json"))
        {
            if (file is not ("tabSettings.json" or "appSettings.json"))
            {
                WriteSettingsFile(file, SettingsHelper.ArtifactSpells[file]);
            }
            else
            {
                switch (file)
                {
                    case "tabSettings.json":
                        WriteSettingsFile(file, TabSettings);

                        break;
                    case "appSettings.json":
                        WriteSettingsFile(file, appSettings);

                        break;
                }
            }
        }

        SettingsChanged = false;
    }

    public void SetCurrentConfig()
    {
        _localSettings.Values["CurrentSettingsPath"] = CurrentSettingsPath;
        CurrentConfigName = Path.GetFileName(CurrentSettingsPath);
        //TODO remove
        Console.WriteLine();
    }

    private T ReadSettingsFile<T>(string fileName) where T : notnull
    {
        T output = JsonSerializer.Deserialize<T>(File.ReadAllText(Path.Combine(CurrentSettingsPath, fileName)))!;

        // ReSharper disable once InvertIf
        if (output is ArtifactSpell[] a && UseHeldentoolNames)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i].Name = HeldentoolInterop.Rename(a[i].Name, Name.Tool);
            }
        }

        return output;
    }

    private void WriteSettingsFile<T>(string fileName, T data) where T : notnull
    {
        if (data is ArtifactSpell[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i].Name = HeldentoolInterop.Rename(a[i].Name, Name.Offi);
            }
        }

        string settingsFile = JsonSerializer.Serialize(data);
        File.WriteAllText(Path.Combine(CurrentSettingsPath, fileName), settingsFile);
    }
}
