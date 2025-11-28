namespace Magehelper.ViewModels.Controls;

public partial class SpellStorageControlViewModel : ObservableObject, IRecipient<AddStoragedSpellMessage>, IRecipient<RemoveStoragedSpellMessage>
{
    private readonly int _spellStorageIndex;
    private readonly SpellStorage? _spellStorage;
    [ObservableProperty] private int _aspRemain;
    public string StorageName { get; } = "Speicher ";
    public ObservableCollection<StoragedSpellControlViewModel> Spells { get; } = [];
    
    public SpellStorageControlViewModel(int spellStorageIndex, SpellStorage spellStorage)
    {
        _spellStorageIndex = spellStorageIndex;
        _spellStorage = spellStorage;
        StorageName += (spellStorageIndex + 1).ToString();
        AspRemain = spellStorage.PointsRemain[spellStorageIndex];

        IEnumerable<StoragedSpell> storagedSpells = from spell in spellStorage.Spells
            where spell.Storage == spellStorageIndex
            select spell;

        foreach (StoragedSpell storagedSpell in storagedSpells)
        {
            Spells.Add(new(storagedSpell));
        }

        Spells.CollectionChanged += Spells_CollectionChanged;
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    public void Receive(AddStoragedSpellMessage message)
    {
        if (message.Value == _spellStorageIndex)
        {
            Spells.Add(new(_spellStorage!.Spells.Last()));
        }
    }

    public void Receive(RemoveStoragedSpellMessage message)
    {
        _spellStorage!.RemoveSpell(message.Value.Guid);
        Spells.Remove(message.Value);
    }

    private void Spells_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        AspRemain = _spellStorage!.PointsRemain[_spellStorageIndex];
    }
}
