namespace Magehelper.ViewModels.Dialogs;

public partial class AddBoneCubSpellDialogViewModel : AddArtifactSpellDialogViewModel
{
    private readonly AddBoneCubSpellControlViewModel _control;

    public AddBoneCubSpellDialogViewModel(BoneCub boneCub) : base(boneCub)
    {
        AddSpellAction = AddSpell;
        SelectedSpellNameChangedAction = SelectedSpellNameChanged;
        AddArtifactSpellControlViewModel = new AddBoneCubSpellControlViewModel(boneCub);
        _control = (AddArtifactSpellControlViewModel as AddBoneCubSpellControlViewModel)!;

        if (string.IsNullOrEmpty((Artifact as BoneCub)!.EnsoulEntityName))
        {
            return;
        }

        _control.IsRollLoyaltyVisible = true;

        if (Character != null)
        {
            _control.CanRollEnsoulEntity = Character.IsLoaded;
        }
    }

    private void SelectedSpellNameChanged(string value)
    {
        IsErrorVisible = false;
        ArtifactSpell spell = Artifact.SpellsAvailable.First(a => a.Name == value);

        switch (value)
        {
            case "Geist der Keule":
                _control.PointsDescriptor = "pAsp:";
                _control.IsPointsVisible = true;
                _control.IsEnsoulEntitySelected = true;
                _control.IsSenseMagicSelected = false;

                if (string.IsNullOrEmpty((Artifact as BoneCub)!.EnsoulEntityName))
                {
                    _control.IsEnsoulEntityNameVisible = true;
                }

                if (string.IsNullOrEmpty(_control.RollEnsoulEntityText))
                {
                    _control.IsEnsoulEntityLoyaltyVisible = true;
                    _control.IsRollEnsoulEntityTextVisible = false;
                }
                else
                {
                    _control.IsEnsoulEntityLoyaltyVisible = false;
                    _control.IsRollEnsoulEntityTextVisible = true;
                }

                break;
            case "Kraft der Keule":
                _control.PointsDescriptor = "Punkte:";
                _control.IsPointsVisible = true;
                _control.IsEnsoulEntitySelected = false;
                _control.IsSenseMagicSelected = false;
                _control.IsEnsoulEntityNameVisible = false;
                _control.IsEnsoulEntityLoyaltyVisible = false;
                _control.IsRollEnsoulEntityTextVisible = false;

                break;
            case "Gespür der Keule":
                _control.PointsDescriptor = "Punkte:";
                _control.IsPointsVisible = false;
                _control.IsEnsoulEntitySelected = false;
                _control.IsSenseMagicSelected = true;
                _control.IsEnsoulEntityNameVisible = false;
                _control.IsEnsoulEntityLoyaltyVisible = false;
                _control.IsRollEnsoulEntityTextVisible = false;
                break;
            default:
                _control.PointsDescriptor = "Punkte:";
                _control.IsPointsVisible = false;
                _control.IsEnsoulEntitySelected = false;
                _control.IsSenseMagicSelected = false;
                _control.IsEnsoulEntityNameVisible = false;
                _control.IsEnsoulEntityLoyaltyVisible = false;
                _control.IsRollEnsoulEntityTextVisible = false;

                break;
        }

        CheckRequirements(spell);
    }

    private void AddSpell()
    {
        Dictionary<string, string> additionalValues = [];

        if (_control.IsEnsoulEntityNameVisible && string.IsNullOrWhiteSpace(_control.EnsoulEntityName))
        {

            ErrorText = "Der Name des Wesens darf nicht leer sein.";
            IsErrorVisible = true;

        }
        else
        {
            IsErrorVisible = false;
        }

        if (_control.IsEnsoulEntitySelected)
        {
            additionalValues["ensoulEntityName"] = _control.EnsoulEntityName;

            if (_control.EnsoulEntityLoyalty > 0)
            {
                additionalValues["ensoulEntityLoyalty"] = _control.EnsoulEntityLoyalty.ToString();
            }
        }

        if (_control.IsPointsVisible)
        {
            additionalValues["points"] = _control.Points.ToString();
        }

        if (_control.IsRollLoyaltyFailure)
        {
            additionalValues["isRollLoyaltyFailure"] = string.Empty;
        }

        AddArtifactSpell(SelectedSpellName, additionalValues);
    }
}
