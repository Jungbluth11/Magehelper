using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DSAUtils.UI.WPF;
using Microsoft.Win32;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal Core.Core Core { get; set; }
        internal TabContentArtifact TabContentArtifact { get; set; }
        internal TabContentSpellStorage TabContentSpellStorage { get; set; }
        internal TabContentFlameSword TabContentFlameSword { get; set; }
        internal TabContentCharacter TabContentCharacter { get; set; }
        internal TabContentPet tabContentPet { get; set; }
        internal TabContentTimer TabContentTimer { get; set; }
        public MainWindow(string[] args)
        {
            InitializeComponent();
            TabSetting[] tabSettings;
            if (Properties.Settings.Default.CurrrentSettingsPath == string.Empty)
            {
                Settings settings = new Settings();
                tabSettings = settings.TabSettings;
            }
            else
            {
                tabSettings = Settings.GetTabSettings(Properties.Settings.Default.CurrrentSettingsPath);
            }
            Core = new Core.Core(Properties.Settings.Default.CurrrentSettingsPath);
            Core.SpellStoragePoints = Properties.Settings.Default.SpellStoragePoints;
            Core.WarnOtherVersionFiles = Properties.Settings.Default.WarnOtherVersionFiles;
            Core.WarnOtherVersionFilesGUIFunc = WarnOtherVersionFilesDialog;
            foreach (TabSetting tabSetting in tabSettings)
            {
                if (tabSetting.ShowTab)
                {
                    UserControl tabContent;
                    switch (tabSetting.TabName)
                    {
                        case "Traditionsartefakt":
                            TabContentArtifact = new TabContentArtifact(this);
                            tabContent = TabContentArtifact;
                            break;
                        case "Zauberspeicher":
                            TabContentSpellStorage = new TabContentSpellStorage(this);
                            tabContent = TabContentSpellStorage;
                            break;
                        case "Flammenschwert":
                            TabContentFlameSword = new TabContentFlameSword(this);
                            tabContent = TabContentFlameSword;
                            break;
                        case "Charakter":
                            TabContentCharacter = new TabContentCharacter(this);
                            tabContent = TabContentCharacter;
                            MenuItemCharacter.IsEnabled = true;
                            MenuItemCharacterLoadFromTool.Click += TabContentCharacter.MenuItemCharacterLoadFromTool_Click;
                            MenuItemCharacterLoadFromFile.Click += TabContentCharacter.MenuItemCharacterLoadFromFile_Click;
                            break;
                        case "Vertrautentier":
                            tabContentPet = new TabContentPet(this);
                            tabContent = tabContentPet;
                            break;
                        case "Timer":
                            TabContentTimer = new TabContentTimer(this);
                            tabContent = TabContentTimer;
                            break;
                        default:
                            tabContent = new TabContentMod();
                            break;
                    }
                    TabItem tabItem = new TabItem();
                    tabItem.Header = tabSetting.TabName;
                    tabItem.Content = tabContent;
                    TabControl.Items.Add(tabItem);
                }
            }
            if (TabControl.Items.Count > 0)
            {
                TabControl.Visibility = Visibility.Visible;
                StringNoTabs.Visibility = Visibility.Collapsed;
            }
#if RELEASE
            if (Properties.Settings.Default.CheckForUpdates)
            {
                Updater updater = new Updater();
                if (updater.CheckForUpdates())
                {
                    if (Properties.Settings.Default.AutoInstallUpdates)
                    {
                        updater.Update();
                    }
                    else if (MessageBox.Show("Es ist ein Update vorhanden, soll es installiert werden?", "Magehelper", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        updater.Update();
                    }
                }
            }
#endif
            if (args.Length == 1)
            {
                Core.ReadFileVersionSelector(args[0]);
            }
        }

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(Core.FileName))
            {
                SaveFileAs();
            }
            else
            {
                Core.WriteFile();
            }
        }

        private void SaveFileAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Magehelper-Dateien | *.magehelper";
            if (saveFileDialog.ShowDialog() == true)
            {
                Core.FileName = saveFileDialog.FileName;
                Core.WriteFile();
            }
        }

        private void ResetTool()
        {
            if (TabContentArtifact != null)
            {
                TabContentArtifact.ResetTab();
            }
            if (TabContentSpellStorage != null)
            {
                TabContentSpellStorage.ResetTab();
            }
            if (TabContentFlameSword != null)
            {
                TabContentFlameSword.ResetTab();
            }
            if (TabContentCharacter != null)
            {
                TabContentCharacter.ResetTab();
            }
            if (tabContentPet != null)
            {
                tabContentPet.ResetTab();
            }
            if (TabContentTimer != null)
            {
                TabContentTimer.ResetTab();
            }
        }

        public bool WarnOtherVersionFilesDialog(bool isLegacy)
        {
            string msg = "Achtung! Die Datei wurde mit einer anderen Version von Magehelper erstellt.\nFalls diese Datei mit einer neueren Version erstellt wurde, werden eventuell nicht alle Funktionen unterstützt und es können Daten verloren gehen wenn sie gespeichert wird.";
            if (isLegacy)
            {
                msg = "Achtung! Diese Datei wurde mit einer Version vor 3.0.0 erstellt. Wenn sie gespeichert wird kann nicht wieder in Magehelper vor Version 3.0.0 geöffnet werden!";
            }
            if (MessageBox.Show(msg, "Magehelper", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }

        private void MenuItemFileNew_Click(object sender, RoutedEventArgs e)
        {
            Core.ResetTool();
            ResetTool();
        }

        private void MenuItemFileLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Magehelper-Dateien | *.magehelper";
            if (openFileDialog.ShowDialog() == true)
            {
                Core.ReadFileVersionSelector(openFileDialog.FileName);
            }
        }

        private void MenuItemFileSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

        private void MenuItemFileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileAs();
        }

        private void MenuItemSettings_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().Show();
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            new About("Magehelper", Assembly.GetExecutingAssembly().GetName().Version.ToString()).ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
#if RELEASE
            if (Core.FileChanged)
            {
                if (MessageBox.Show("Sollen die Änderungen gespeichert werden?", "Magehelper", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    SaveFile();
                }
            }
#endif
        }
    }
}