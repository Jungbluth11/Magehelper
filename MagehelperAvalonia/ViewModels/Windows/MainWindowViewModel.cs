using Avalonia.Platform.Storage;
using DSAUtils.HeldentoolInterop;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Enums;

namespace Magehelper.Avalonia.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private static readonly MainWindowViewModel _instance = new();
        public bool CanLoadCharacterFromTool => HeldentoolInterop.IsInstalled();
        public static MainWindowViewModel Instance => _instance;
        public Settings Settings { get; }
        public Core.Core Core { get; set; }

        public MainWindowViewModel()
        {
            Settings = new();
            Core = new(Settings.CurrentSettingsPath)
            {
                SpellStoragePoints = Settings.SpellStoragePoints,
            };

            if (Settings.CheckForUpdates)
            {
                Update();
            }
        }

        public void NewFile()
        {
            Core.ResetTool();
        }

        public void LoadFile(string path)
        {
            Core.ReadFileVersionSelector(path);
        }

        public void SaveFile(string path)
        {
            if (string.IsNullOrEmpty(Core.FileName))
            {
                Core.FileName = path;
            }
            Core.WriteFile();
        }

        public string WarnOtherVersionFilesMessage(bool isLegacy)
        {
            if (isLegacy)
            {
                return "Achtung! Diese Datei wurde mit einer Version vor 3.0.0 erstellt. Wenn sie gespeichert wird kann nicht wieder in Magehelper vor Version 3.0.0 geöffnet werden!";
            }
            return "Achtung! Die Datei wurde mit einer anderen Version von Magehelper erstellt.\nFalls diese Datei mit einer neueren Version erstellt wurde, werden eventuell nicht alle Funktionen unterstützt und es können Daten verloren gehen wenn sie gespeichert wird.";
        }

        private async void Update()
        {
            Updater updater = new();
            if (updater.CheckForUpdates())
            {
                if (Settings.AutoInstallUpdates)
                {
                    updater.Update();
                }
                else
                {
                    IMsBox<string> messageBox = MessageBoxGenerator.GetMessageBox("Es ist ein Update vorhanden, soll es installiert werden?", MessageBoxGenerator.Buttons.YesNo, Icon.Question);
                    string result = await messageBox.ShowAsync();
                    if (result == "Ja")
                    {
                        updater.Update();
                    }
                }
            }
        }
    }
}
