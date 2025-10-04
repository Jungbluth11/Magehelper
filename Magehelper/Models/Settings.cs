using System.Text.Json;

namespace Magehelper.Models;

public class Settings
{
    private static Settings? _instance;
    private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
    private readonly List<string> _configNames = [];

    private readonly string[] _artifactFiles =
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
    public string CurrentSettingsPath { get; private set; }
    public ArtifactSpell[] ObsidianDaggerSpells { get; set; } = [];
    public ArtifactSpell[] RingOfLifeSpells { get; set; } = [];
    public bool SettingsChanged { get; set; }
    public int SpellStoragePoints { get; set; }
    public ArtifactSpell[] StaffSpells { get; set; } = [];
    public TabSetting[] TabSettings { get; private set; } = [];
    public bool UseHeldentoolNames { get; set; }
    public bool WarnOtherVersionFiles { get; set; }

    private Settings()
    {
        BaseSettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "magehelper", "config");
#if DEBUG
        CurrentSettingsPath = Path.Combine(AppContext.BaseDirectory, "BaseSettings");
#endif
        if (_localSettings.Values["CurrrentSettingsPath"] == null)
        {
            try
            {
                LoadConfig("Standard");
            }
            catch
            {
                // ReSharper disable once UnusedVariable --- suppress warning for debug build
                string defaultConfigPath = AddConfig("Standard");
#if RELEASE
                CurrentSettingsPath = defaultConfigPath;
#endif
            }

            SetCurrentConfig();
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

        foreach (string file in _artifactFiles.Append("tabSettings.json").Append("appSettings.json"))
        {
            File.Copy(Path.Combine(AppContext.BaseDirectory, "BaseSettings", file),
                Path.Combine(configDirectory.FullName, file), true);
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
        CurrentSettingsPath = Path.Combine(BaseSettingsPath, configName);

        foreach (string file in _artifactFiles.Append("tabSettings.json").Prepend("appSettings.json"))
        {
            if (file is not ("tabSettings.json" or "appSettings.json"))
            {
                SettingsHelper.ArtifactSpells[file] = ReadSettingsFile<ArtifactSpell[]>(file);
            }
            else
            {
                switch (file)
                {
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
                        ChangeTraditionArtifactTabName = appSettings.ChangeTraditionArtifactTabName;

                        break;
                }
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

        foreach (string file in _artifactFiles.Append("tabSettings.json").Append("appSettings.json"))
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
        _localSettings.Values["CurrrentSettingsPath"] = CurrentSettingsPath;
        SettingsChanged = true;
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
