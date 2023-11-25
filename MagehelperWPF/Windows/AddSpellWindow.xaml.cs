using System.Collections.Generic;
using System.Windows;
using DSAUtils;
using DSAUtils.Settings.Aventurien;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für AddSpellWindow.xaml
    /// </summary>
    public partial class AddSpellWindow : Window
    {
        private readonly TabContentSpellStorage tabContentSpellStorage;
        private readonly List<string> spellStorages = new List<string>();
        public StoragedSpell Spell { get; private set; }

        public AddSpellWindow(TabContentSpellStorage tabContentSpellStorage)
        {
            InitializeComponent();
            this.tabContentSpellStorage = tabContentSpellStorage;
            DropdownKomplex.ItemsSource = DSA.komplexitaet;
            ComboBoxCharacteristics.ItemsSource = Aventurien.Zauber.merkmalsliste;
            for (int i = 0; i < tabContentSpellStorage.SpellStorage.StorageCount; i++)
            {
                spellStorages.Add("Speicher " + (i + 1).ToString());
            }
            DropdownSpellStorage.ItemsSource = spellStorages;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TBoxName.Text) && !string.IsNullOrWhiteSpace(ComboBoxCharacteristics.Text) && (TBoxCost.Text ?? TBoxZfp.Text) != null && DropdownKomplex.SelectedIndex >= 0 && DropdownSpellStorage.SelectedIndex >= 0)
            {
                string name = TBoxName.Text;
                string characteristics = ComboBoxCharacteristics.Text;
                string komplex = DropdownKomplex.Text;
                int cost = int.Parse(TBoxCost.Text);
                int zfp = int.Parse(TBoxZfp.Text);
                int storage = DropdownSpellStorage.SelectedIndex;
                StoragedSpell storagedSpell = tabContentSpellStorage.SpellStorage.AddSpell(name, characteristics, komplex, cost, zfp, storage);
                Spell = storagedSpell;
                DialogResult = true;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}