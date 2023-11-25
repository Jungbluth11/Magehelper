using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using DSAUtils.HeldentoolInterop;
using Magehelper.Core;

namespace Magehelper.WPF
{
    public class Settings
    {
        private readonly string[] artifactFiles = new string[]
        {
            "staff.json",
            "crystalBall.json",
            "bowl.json",
            "boneCub.json",
            "ringOfLife.json",
            "obsidianDagger.json"
        };
        private readonly List<string> configNames = new List<string>();
        public int SpellStoragePoints { get; set; }
        public string BaseSettingsPath { get; private set; }
        public string CurrentSettingsPath { get; private set; }
        public bool UseHeldentoolNames { get; set; }
        public bool AllowRemoveSpells { get; set; }
        public bool CheckForUpdates { get; set; }
        public bool AutoInstallUpdates { get; set; }
        public bool SettingsChanged { get; set; }
        public bool WarnOtherVersionFiles { get; set; }
        public ReadOnlyCollection<string> ConfigNames => configNames.AsReadOnly();
        public TabSetting[] TabSettings { get; private set; }
        public ArtifactSpell[] StaffSpells { get; private set; }
        public ArtifactSpell[] CrystalBallSpells { get; private set; }
        public ArtifactSpell[] BowlSpells { get; private set; }
        public ArtifactSpell[] BoneCubSpells { get; private set; }
        public ArtifactSpell[] RingOfLifeSpells { get; private set; }
        public ArtifactSpell[] ObsidianDaggerSpells { get; private set; }

        public Settings()
        {
            BaseSettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "magehelper", "config");
#if DEBUG
            CurrentSettingsPath = Path.Combine(AppContext.BaseDirectory, "BaseSettings");
#endif
            if (Properties.Settings.Default.CurrrentSettingsPath == string.Empty)
            {
                string defaultConfigPath = AddConfig("Standard");
#if RELEASE
                CurrentSettingsPath = defaultConfigPath;
#endif
                LoadConfig("Standard");
                SetCurrentConfig();
            }
            else
            {
                CurrentSettingsPath = Properties.Settings.Default.CurrrentSettingsPath;
                LoadConfig();
            }
            string[] directoryList = Directory.GetDirectories(BaseSettingsPath);
            foreach (string directory in directoryList)
            {
                configNames.Add(Path.GetFileName(directory));
            }
        }

        public static TabSetting[] GetTabSettings(string configPath)
        {
            return JsonSerializer.Deserialize<TabSetting[]>(File.ReadAllText(Path.Combine(configPath, "tabSettings.json")));
        }

        public string AddConfig(string configName)
        {
            DirectoryInfo configDirectory = Directory.CreateDirectory(Path.Combine(BaseSettingsPath, configName));
            foreach (string file in artifactFiles.Append("tabSettings.json").Append("appSettings.json"))
            {
                File.Copy(Path.Combine(AppContext.BaseDirectory, "BaseSettings", file), Path.Combine(configDirectory.FullName, file), true);
            }
            return configDirectory.FullName;
        }

        public void LoadConfig(string configName = null)
        {
            if (!string.IsNullOrEmpty(configName))
            {
                CurrentSettingsPath = Path.Combine(BaseSettingsPath, configName);
            }
            foreach (string file in artifactFiles.Append("tabSettings.json").Prepend("appSettings.json"))
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
            AppSettings appSettings = new AppSettings
            {
                SpellStoragePoints = SpellStoragePoints,
                AllowRemoveSpells = AllowRemoveSpells,
                UseHeldentoolNames = UseHeldentoolNames,
                CheckForUpdates = CheckForUpdates,
                AutoInstallUpdates = AutoInstallUpdates,
                WarnOtherVersionFiles = WarnOtherVersionFiles
            };
            foreach (string file in artifactFiles.Append("tabSettings.json").Append("appSettings.json"))
            {
                switch (file)
                {
                    case "staff.json":
                        WriteSettingsFile(file, StaffSpells);
                        break;
                    case "crystalBall.json":
                        WriteSettingsFile(file, CrystalBallSpells);
                        break;
                    case "bowl.json":
                        WriteSettingsFile(file, BowlSpells);
                        break;
                    case "boneCub.json":
                        WriteSettingsFile(file, BoneCubSpells);
                        break;
                    case "ringOfLife.json":
                        WriteSettingsFile(file, RingOfLifeSpells);
                        break;
                    case "obsidianDagger.json":
                        WriteSettingsFile(file, ObsidianDaggerSpells);
                        break;
                    case "tabSettings.json":
                        WriteSettingsFile(file, TabSettings);
                        break;
                    case "appSettings.json":
                        WriteSettingsFile(file, appSettings);
                        break;
                }
            }
            SettingsChanged = false;
        }

        public void SetCurrentConfig()
        {
            Properties.Settings.Default.CurrrentSettingsPath = CurrentSettingsPath;
            Properties.Settings.Default.SpellStoragePoints = SpellStoragePoints;
            Properties.Settings.Default.AllowRemoveSpells = AllowRemoveSpells;
            Properties.Settings.Default.UseHeldentoolNames = UseHeldentoolNames;
            Properties.Settings.Default.CheckForUpdates = CheckForUpdates;
            Properties.Settings.Default.AutoInstallUpdates = AutoInstallUpdates;
            Properties.Settings.Default.WarnOtherVersionFiles = WarnOtherVersionFiles;
            Properties.Settings.Default.Save();
            SettingsChanged = true;
        }

        private void WriteSettingsFile<T>(string fileName, T data)
        {
            if (data is ArtifactSpell[] a)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    (data as ArtifactSpell[])[i].Name = HeldentoolInterop.Rename(a[i].Name, Name.Offi);
                }
            }
            string settingsFile = JsonSerializer.Serialize(data);
            File.WriteAllText(Path.Combine(CurrentSettingsPath, fileName), settingsFile);
        }

        private T ReadSettingsFile<T>(string fileName)
        {
            T output = JsonSerializer.Deserialize<T>(File.ReadAllText(Path.Combine(CurrentSettingsPath, fileName)));
            if (output is ArtifactSpell[] a && UseHeldentoolNames)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    (output as ArtifactSpell[])[i].Name = HeldentoolInterop.Rename(a[i].Name, Name.Tool);
                }
            }
            return output;
        }
    }
}