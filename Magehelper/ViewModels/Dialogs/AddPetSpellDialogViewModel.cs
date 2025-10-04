namespace Magehelper.ViewModels.Dialogs;

public partial class AddPetSpellWindowViewModel : ObservableObject
{
    private readonly Pet pet;
    public ObservableCollection<PetSpell> SpellsAvailable { get; set; } = [];
    public ObservableCollection<PetSpell> SpellsToLearn { get; set; } = [];

    public AddPetSpellWindowViewModel(Pet pet)
    {
        this.pet = pet;
        foreach (PetSpell spell in pet.SpellsAvailable)
        {
            if (!pet.KnownSpells.Contains(spell))
            {
                SpellsAvailable.Add(spell);
            }
        }
        SpellsAvailable.Sort(p => p.Name);
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
    private void Submit(Window window)
    {
        foreach (PetSpell spell in SpellsToLearn)
        {
            pet.LearnSpell(spell.Name);
        }
        window.Close();
    }

    [RelayCommand]
    private void Cancel(Window window)
    {
        window.Close();
    }
}
