using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class SpellStorageControlViewModel : ObservableObject
    {
        private readonly int spellStorageIndex;
        private readonly SpellStorage spellStorage;
        [ObservableProperty]
        private int aspRemain;
        public string StorageName { get; }
        public ObservableCollection<StoragedSpell> Spells { get; set; } = [];

        public SpellStorageControlViewModel(int spellStorageIndex, SpellStorage spellStorage)
        {
            this.spellStorageIndex = spellStorageIndex;
            this.spellStorage = spellStorage;
            StorageName = "Speicher " + spellStorageIndex.ToString();
            AspRemain = spellStorage.PointsRemain[spellStorageIndex];
            foreach (StoragedSpell spell in spellStorage.Spells)
            {
                if (spell.Storage == spellStorageIndex)
                {
                    Spells.Add(spell);
                }
            }
            Spells.CollectionChanged += Spells_CollectionChanged;
        }

        private void Spells_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            AspRemain = spellStorage.PointsRemain[spellStorageIndex];
        }

        [RelayCommand]
        private void RemoveSpell(StoragedSpell storagedSpell)
        {
            spellStorage.RemoveSpell(storagedSpell.Guid);
            Spells.Remove(storagedSpell);
        }
    }
}
