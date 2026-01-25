using DSAUtils.Settings.Aventurien;

namespace Magehelper.ViewModels.Dialogs;

public partial class AddSpellDialogViewModel : ObservableObject
{
    private readonly SpellStorage _spellStorage;
    [ObservableProperty]
    private int _spellstorageNumber;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _name = string.Empty;
    [ObservableProperty]
    private string _characteristic;
    [ObservableProperty]
    private string _komplex;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private int _cost;
    [ObservableProperty]
    private int _zfp;
    public IEnumerable<string> CharacteristicStrings { get; }
    public IEnumerable<string> KomplexStrings { get; }
    public List<string> SpellStorages { get; } = [];

    public AddSpellDialogViewModel()
    {
        _spellStorage = Core.Core.GetInstance().SpellStorage!;
        CharacteristicStrings = Aventurien.Zauber.merkmalsliste;
        KomplexStrings = DSA.komplexitaet;
        Characteristic = CharacteristicStrings.First();
        Komplex = KomplexStrings.First();
        for (int i = 1; i <= _spellStorage.StorageCount; i++)
        {
            SpellStorages.Add($"Speicher {i}");
        }
    }

    private bool CanSubmit()
    {
        return !string.IsNullOrWhiteSpace(Name) && Cost > 0;
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private void Submit()
    {
        _spellStorage.AddSpell(Name, Characteristic, Komplex, Cost, Zfp, SpellstorageNumber);
        WeakReferenceMessenger.Default.Send(new AddStoragedSpellMessage(SpellstorageNumber));
    }
}
