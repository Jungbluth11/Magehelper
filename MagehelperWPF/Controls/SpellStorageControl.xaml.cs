using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für SpellStorageControl.xaml
    /// </summary>
    public partial class SpellStorageControl : UserControl
    {
        private readonly int spellStorageindex;
        private readonly SpellStorage spellStorage;
        public ObservableCollection<StoragedSpell> Spells { get; set; } = new ObservableCollection<StoragedSpell>();

        public SpellStorageControl(int spellStorageIndex, SpellStorage spellStorage)
        {
            InitializeComponent();
            spellStorageindex = spellStorageIndex;
            this.spellStorage = spellStorage;
            Name = "SpellStorageControl" + spellStorageIndex.ToString();
            StringName.Content = "Speicher " + (spellStorageIndex + 1).ToString();
            StringAspRemain.Content = spellStorage.PointsRemain[spellStorageIndex];
            foreach (StoragedSpell spell in spellStorage.Spells)
            {
                if (spell.Storage == spellStorageIndex)
                {
                    Spells.Add(spell);
                }
            }
            Spells.CollectionChanged += Spells_CollectionChanged;
            StoragedSpellPanel.ItemsSource = Spells;
        }

        private void Spells_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            StringAspRemain.Content = spellStorage.PointsRemain[spellStorageindex];
        }

        private void BtnRemoveSpell_Click(object sender, RoutedEventArgs e)
        {
            StoragedSpell storagedSpell = (StoragedSpell)(sender as Button).Tag;
            spellStorage.RemoveSpell(storagedSpell.Guid);
            Spells.Remove(storagedSpell);
        }
    }
}