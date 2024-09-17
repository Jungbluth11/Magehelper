using DSAUtils.HeldentoolInterop;

namespace Magehelper.Avalonia.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private static readonly MainWindowViewModel _instance = new();
        public static MainWindowViewModel Instance => _instance;
        public bool CanLoadCharacter => TabContentCharacter != null;
        public bool CanLoadCharacterFromTool => HeldentoolInterop.IsInstalled();
        public Settings Settings { get; }
        public Core.Core Core { get; set; }
        public TabContentArtifactViewModel? TabContentArtifact { get; set; }
        public TabContentSpellStorageViewModel? TabContentSpellStorage { get; set; }
        public TabContentFlameSwordViewModel? TabContentFlameSword { get; set; }
        public TabContentCharacterViewModel? TabContentCharacter { get; set; }
        public TabContentPetViewModel? TabContentPet { get; set; }
        public TabContentTimerViewModel? TabContentTimer { get; set; }

        public MainWindowViewModel()
        {
            Settings = new Settings();
            Core = new Core.Core(Settings.CurrentSettingsPath)
            {
                SpellStoragePoints = Settings.SpellStoragePoints,
                WarnOtherVersionFiles = Settings.WarnOtherVersionFiles
            };
            
            
            if (Settings.CheckForUpdates)
            {
                Updater updater = new();
                if (updater.CheckForUpdates())
                {
                    if (Settings.AutoInstallUpdates)
                    {
                        updater.Update();
                    }
                    //else if (MessageBox.Show("Es ist ein Update vorhanden, soll es installiert werden?", "Magehelper", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    //{
                    //    updater.Update();
                    //}
                }
            }
        }


        [RelayCommand]
        private void NewFile()
        {
            Core.ResetTool();
            ResetTool();
        }

        public void LoadFile(string fileName)
        {
            ResetTool();
            Core.ReadFileVersionSelector(fileName);
        }

        [RelayCommand]
        private void SaveFile(string fileName)
        {
            if (string.IsNullOrEmpty(Core.FileName))
            {
                Core.FileName = fileName;
            }
            Core.WriteFile();
        }

        public void ResetTool()
        {
            //if (TabContentArtifact != null)
            //{
            //    TabContentArtifact.ResetTab();
            //}
            //if (TabContentSpellStorage != null)
            //{
            //    TabContentSpellStorage.ResetTab();
            //}
            //if (TabContentFlameSword != null)
            //{
            //    TabContentFlameSword.ResetTab();
            //}
            //if (TabContentCharacter != null)
            //{
            //    TabContentCharacter.ResetTab();
            //}
            //if (tabContentPet != null)
            //{
            //    tabContentPet.ResetTab();
            //}
            //if (TabContentTimer != null)
            //{
            //    TabContentTimer.ResetTab();
            //}
        }
    }
}
