using System.Collections.Specialized;

namespace Magehelper.ViewModels.Controls;

public partial class SpellStorageControlViewModel : ObservableObject
{
    private int _spellStorageIndex;
    private SpellStorage? _spellStorage;
    [ObservableProperty]
    private int _aspRemain;
    public string StorageName { get; private set; } = "Speicher ";
    public ObservableCollection<StoragedSpell> Spells { get; set; } = [];

    public void Init(int spellStorageIndex, SpellStorage spellStorage)
    {
        _spellStorageIndex = spellStorageIndex;
        _spellStorage = spellStorage;
        StorageName += spellStorageIndex.ToString();
        AspRemain = spellStorage.PointsRemain[spellStorageIndex];
        foreach (StoragedSpell spell in spellStorage.Spells)
        {
            if (spell.Storage == spellStorageIndex)
            {
                Spells.Add(spell);
            }
            Spells.CollectionChanged += Spells_CollectionChanged;
        }
    }

    private void Spells_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        AspRemain = _spellStorage!.PointsRemain[_spellStorageIndex];
    }

    [RelayCommand]
    private void RemoveSpell(StoragedSpell storagedSpell)
    {
        _spellStorage!.RemoveSpell(storagedSpell.Guid);
        Spells.Remove(storagedSpell);
    }
}
