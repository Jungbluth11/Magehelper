namespace Magehelper.ViewModels.Dialogs;

public partial class AddCrystalBallSpellDialogViewModel : AddArtifactSpellDialogViewModel
{
    private readonly AddCrystalBallSpellControlViewModel _control;

    public AddCrystalBallSpellDialogViewModel(CrystalBall crystalBall) : base(crystalBall)
    {
        AddSpellAction = AddSpell;
        SelectedSpellNameChangedAction = SelectedSpellNameChanged;
        AddArtifactSpellControlViewModel = new AddCrystalBallSpellControlViewModel();
        _control = (AddArtifactSpellControlViewModel as AddCrystalBallSpellControlViewModel)!;
    }

    private void SelectedSpellNameChanged(string value)
    {
        IsErrorVisible = false;
        ArtifactSpell spell = Artifact.SpellsAvailable.First(a => a.Name == value);

        if (value == "Bildergalerie")
        {
            _control.IsSpellVariantVisible = true;
        }
        else
        {
            _control.IsSpellVariantVisible = false;
            _control.SelectedSpellVariant = _control.SpellVariants.First();
        }

        CheckRequirements(spell);
    }

    private void AddSpell()
    {
        Dictionary<string, string> additionalValues = [];

        if (_control.IsSpellVariantVisible)
        {
            additionalValues["variant"] = _control.SelectedSpellVariant;
        }

        if (_control.IsVariantDescriptionVisible)
        {
            additionalValues["variantDescription"] = _control.VariantDescription;
        }

        AddArtifactSpell(SelectedSpellName, additionalValues);
    }
}
