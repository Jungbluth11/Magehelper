using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für TabContentSpellStorage.xaml
    /// </summary>
    public partial class TabContentSpellStorage : UserControl
    {
        internal MainWindow MainWindow { get; }
        internal List<SpellStorageControl> SpellStorageList { get; set; } = new List<SpellStorageControl>();
        public SpellStorage SpellStorage { get; }

        public TabContentSpellStorage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.MainWindow = mainWindow;
            SpellStorage = new SpellStorage(mainWindow.Core);
            mainWindow.Core.EnableSpellStorageGUIAction = EnableTab;
            if (mainWindow.Core.HasSpellStorage)
            {
                EnableTab();
            }
        }

        public void EnableTab()
        {
            BtnAddSpell.IsEnabled = true;
            StringNone.Visibility = Visibility.Collapsed;
            WrapPanelStorages.Visibility = Visibility.Visible;
            for (int i = 0; i < SpellStorage.StorageCount; i++)
            {
                SpellStorageList.Add(new SpellStorageControl(i, SpellStorage));
                WrapPanelStorages.Children.Add(SpellStorageList[i]);
            }
        }

        internal void ResetTab()
        {
            BtnAddSpell.IsEnabled = false;
            StringNone.Visibility = Visibility.Visible;
            WrapPanelStorages.Visibility = Visibility.Collapsed;
            SpellStorageList.Clear();
        }

        private void BtnAddSpell_Click(object sender, RoutedEventArgs e)
        {
            AddSpellWindow addSpellWindow = new AddSpellWindow(this);
            if (addSpellWindow.ShowDialog() == true)
            {
                StoragedSpell spell = addSpellWindow.Spell;
                SpellStorageList[spell.Storage].Spells.Add(spell);
            }
        }
    }
}