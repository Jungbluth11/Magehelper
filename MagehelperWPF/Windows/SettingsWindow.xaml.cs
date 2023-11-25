using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly Settings settings;
        private readonly ObservableCollection<string> configNames;

        public SettingsWindow()
        {
            InitializeComponent();
            settings = new Settings();
            configNames = new ObservableCollection<string>(settings.ConfigNames);
            DropdownSettings.ItemsSource = configNames;
            DropdownSettings.SelectedIndex = 0;
            LoadConfig();
        }

        private void LoadConfig(string configName = null)
        {
            if (configName != null)
            {
                settings.LoadConfig(configName);
            }
            ShowTabsPanel.ItemsSource = settings.TabSettings;
            CbAllowRemoveSpells.IsChecked = settings.AllowRemoveSpells;
            CbUseHeldentoolNames.IsChecked = settings.UseHeldentoolNames;
            CbCheckForUpdates.IsChecked = settings.CheckForUpdates;
            CbAutoinstallUpdates.IsChecked = settings.AutoInstallUpdates;
            CbWarnOtherVersionFiles.IsChecked = settings.WarnOtherVersionFiles;
            TabControlArtifacts.Items.Clear();
            StaffSettingsControl staffSettings = new StaffSettingsControl(settings.StaffSpells);
            ArtifactSettingsControl crystalBallSettings = new ArtifactSettingsControl(settings.CrystalBallSpells);
            ArtifactSettingsControl bowlSettings = new ArtifactSettingsControl(settings.BowlSpells);
            ArtifactSettingsControl boneCubSettings = new ArtifactSettingsControl(settings.BoneCubSpells);
            ArtifactSettingsControl ringOfLifeSettings = new ArtifactSettingsControl(settings.RingOfLifeSpells);
            ArtifactSettingsControl obsidianDaggerSettings = new ArtifactSettingsControl(settings.ObsidianDaggerSpells);
            IArtifactSettingsTab[] artifactSettings = new IArtifactSettingsTab[]
            {
                staffSettings,
                crystalBallSettings,
                bowlSettings,
                boneCubSettings,
                ringOfLifeSettings,
                obsidianDaggerSettings
            };
            foreach (IArtifactSettingsTab setting in artifactSettings)
            {
                TabItem tabItem = new TabItem();
                tabItem.Header = setting.SettingsHeader;
                tabItem.Content = setting;
                TabControlArtifacts.Items.Add(tabItem);
            }
        }

        private void BtnAddSettings_Click(object sender, RoutedEventArgs e)
        {
            DialogTextWindow dialogTextWindow = new DialogTextWindow("Konfiguration hinzufügen", "Name der Konfiguration");
            if (dialogTextWindow.ShowDialog() == true)
            {
                settings.AddConfig(dialogTextWindow.value);
                configNames.Add(dialogTextWindow.value);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (settings.SettingsChanged)
            {
                if (MessageBox.Show("Sollen die Änderungen gespeichert werden?", "Magehelper", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    settings.SaveConfigFile();
                    settings.SetCurrentConfig();
                    if (MessageBox.Show("Die Änderungen werden erst nach einem Neustart wirksam.\nSoll jetzt neu gestartet werden?", "Magehelper", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                    }
                }
            }
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnForceUpdate_Click(object sender, RoutedEventArgs e)
        {
            Updater updater = new Updater();
            if (updater.CheckForUpdates())
            {
                updater.Update();
            }
            else
            {
                MessageBox.Show("Magehelper ist auf dem neuesten Stand.", "Magehelper", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CbTab_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            for (int i = 0; i < settings.TabSettings.Length; i++)
            {
                if (settings.TabSettings[i].TabName == checkBox.Content.ToString())
                {
                    settings.TabSettings[i].ShowTab = (bool)checkBox.IsChecked;
                    break;
                }
            }
        }

        private void CbAllowRemoveSpells_CheckedChanged(object sender, RoutedEventArgs e)
        {
            settings.AllowRemoveSpells = (bool)CbAllowRemoveSpells.IsChecked;
            settings.SettingsChanged = true;
        }

        private void CbUseHeldentoolNames_CheckedChanged(object sender, RoutedEventArgs e)
        {
            settings.UseHeldentoolNames = (bool)CbUseHeldentoolNames.IsChecked;
            settings.SettingsChanged = true;
        }

        private void CbCheckForUpdates_CheckedChanged(object sender, RoutedEventArgs e)
        {
            settings.CheckForUpdates = (bool)CbCheckForUpdates.IsChecked;
            settings.SettingsChanged = true;
        }

        private void CbAutoinstallUpdates_CheckedChanged(object sender, RoutedEventArgs e)
        {
            settings.AutoInstallUpdates = (bool)CbAutoinstallUpdates.IsChecked;
            settings.SettingsChanged = true;
        }

        private void CbWarnOtherVersionFiles_CheckedChanged(object sender, RoutedEventArgs e)
        {
            settings.WarnOtherVersionFiles = (bool)CbWarnOtherVersionFiles.IsChecked;
            settings.SettingsChanged = true;
        }

        private void DropdownSettings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (settings.SettingsChanged)
            {
                if (MessageBox.Show("Sollen die Änderungen gespeichert werden?", "Magehelper", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    settings.SaveConfigFile();
                }
            }
            LoadConfig(DropdownSettings.Text);
        }
    }
}