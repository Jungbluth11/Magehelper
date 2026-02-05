namespace Magehelper.ViewModels.Dialogs;

public partial class AddPetSpellDialogViewModel : ObservableObject
{
    private readonly Pet _pet;
    public ObservableCollection<PetSpell> SpellsAvailable { get; set; } = [];
    public ObservableCollection<PetSpell> SpellsToLearn { get; set; } = [];

    public AddPetSpellDialogViewModel()
    {
        _pet = Core.Core.Instance.Pet!;

        SpellsAvailable.AddRange(from p in _pet.SpellsAvailable
                                 where !_pet.KnownSpells.Contains(p)
                                 orderby p.Name
                                 select p);
    }

    private bool CanSubmit()
    {
        return SpellsToLearn.Count > 0;
    }

    [RelayCommand]
    private void AddSpell(PetSpell petSpell)
    {
        SpellsAvailable.Remove(petSpell);
        SpellsToLearn.Add(petSpell);
        SubmitCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    private void RemoveSpell(PetSpell petSpell)
    {
        SpellsAvailable.Add(petSpell);
        SpellsAvailable.Sort(p => p.Name);
        SpellsToLearn.Remove(petSpell);
        SubmitCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private void Submit()
    {
        foreach (PetSpell spell in SpellsToLearn)
        {
            _pet.LearnSpell(spell.Name);
        }
    }
}
