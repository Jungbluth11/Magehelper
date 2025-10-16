namespace Magehelper.ViewModels.Controls;

public partial class SpellStorageControlViewModel : ObservableObject, IRecipient<AddStoragedSpellMessage>
{
    private int _spellStorageIndex;
    private SpellStorage? _spellStorage;
    [ObservableProperty] private string _storageName = "Speicher ";
    [ObservableProperty] private int _aspRemain;
    public ObservableCollection<StoragedSpell> Spells { get; } = [];


    public SpellStorageControlViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(AddStoragedSpellMessage message)
    {
        if (message.Value == _spellStorageIndex)
        {
            Spells.Add(_spellStorage!.Spells.Last());
        }
    }

    private void Spells_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        AspRemain = _spellStorage!.PointsRemain[_spellStorageIndex];
    }

    public void Init(int spellStorageIndex, SpellStorage spellStorage)
    {
        _spellStorageIndex = spellStorageIndex;
        _spellStorage = spellStorage;
        StorageName += (spellStorageIndex + 1).ToString();
        AspRemain = spellStorage.PointsRemain[spellStorageIndex];

        Spells.AddRange(from spell in spellStorage.Spells
                        where spell.Storage == spellStorageIndex
                        select spell);

        Spells.CollectionChanged += Spells_CollectionChanged;
    }

    [RelayCommand]
    private void RemoveSpell(StoragedSpell storagedSpell)
    {
        _spellStorage!.RemoveSpell(storagedSpell.Guid);
        Spells.Remove(storagedSpell);
    }
}