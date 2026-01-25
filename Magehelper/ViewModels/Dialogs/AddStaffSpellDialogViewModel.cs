namespace Magehelper.ViewModels.Dialogs;

public partial class AddStaffSpellDialogViewModel : AddArtifactSpellDialogViewModel
{
    private readonly int _pointsRemain;
    private readonly AddStaffSpellControlViewModel _control;

    public AddStaffSpellDialogViewModel(Staff staff) : base(staff)
    {
        AddSpellAction = AddSpell;
        SelectedSpellNameChangedAction = SelectedSpellNameChanged;
        AddArtifactSpellControlViewModel = new AddStaffSpellControlViewModel(staff);
        _control = (AddArtifactSpellControlViewModel as AddStaffSpellControlViewModel)!;
        _pointsRemain = staff.AfvRemain;
    }

    private void AddSpell()
    {
        Dictionary<string, string> additionalValues = [];

        additionalValues["spellPoints"] = _control.SpellPoints.ToString();
        additionalValues["characteristic"] = _control.SelectedCharacteristic;

        if (_control.IsHammerSelected)
        {
            additionalValues["hammerRkp"] = _control.HammerRkp.ToString();
        }

        if (_control.IsFlameSwordSelected)
        {
            additionalValues["flameSwordFailure"] = _control.FlameSwordFailure.ToString();

            if (_control.FlameSwordFailure < 4)
            {
                additionalValues["flameSwordFailureDamage"] = _control.FlameSwordFailureDamage.ToString();
            }
        }

        if (_control.IsReptileSkinSelected)
        {
            additionalValues["reptileSkinVariant"] = _control.SelectedReptileSkinVariant;
        }

        AddArtifactSpell(SelectedSpellName, additionalValues);
    }

    private bool CanRoll(string ritualName)
    {
        return Character != null &&
               Character.Rituals!.Contains(Character.Rituals!.Single(r => r.Name == ritualName));
    }

    private void CheckPoints()
    {
        if (_pointsRemain < _control.SpellPoints)
        {
            ErrorText =
                $"Der Zauber verbraucht {_control.SpellPoints} Volumenpunkte. Es sind nur noch {_pointsRemain} Volumenpunkte im Stab vorhanden";

            IsErrorVisible = true;
        }

        IsErrorVisible = false;
    }

    //partial void OnSpellPointsChanged(int value)
    //{
    //    CheckPoints();
    //}

    private void SelectedSpellNameChanged(string value)
    {
        IsErrorVisible = false;
        ArtifactSpell spell = Artifact.SpellsAvailable.First(a => a.Name == value);

        if (value != "Zauberspeicher" && value != "Doppeltes Maß")
        {
            _control.SpellPoints = spell.Points;
        }

        CheckPoints();

        switch (value)
        {
            case "Merkmalsfokus":
                _control.IsSpellPointsVisible = false;
                _control.IsCharacteristicVisible = true;
                _control.IsHammerSelected = true;
                _control.IsFlameSwordSelected = false;

                break;
            case "Doppeltes Maß":
            case "Zauberspeicher":
                _control.IsSpellPointsVisible = true;
                _control.IsCharacteristicVisible = false;
                _control.IsHammerSelected = true;
                _control.IsFlameSwordSelected = false;

                _control.MaxSpellPoints = Artifact.SpellsAvailable
                    .Where(a => a.Name == value)
                    .ToArray()[0]
                    .Points;

                _control.MinSpellPoints = 1;
                _control.SpellPoints = 1;

                if (value == "Doppeltes Maß")
                {
                    _control.SpellPoints = _control.MaxSpellPoints;
                    _control.MinSpellPoints = _control.MaxSpellPoints;
                    _control.MaxSpellPoints *= 2;
                }

                break;
            case "Hammer des Magus":
                _control.IsSpellPointsVisible = false;
                _control.IsCharacteristicVisible = false;
                _control.IsHammerSelected = true;
                _control.IsFlameSwordSelected = false;
                _control.CanRollHammer = CanRoll(value);

                break;
            case "Flammenschwert":
                _control.IsSpellPointsVisible = false;
                _control.IsCharacteristicVisible = false;
                _control.IsHammerSelected = false;
                _control.IsFlameSwordSelected = true;
                _control.CanRollFlameSword = CanRoll(value);

                break;
            case "Schuppenhaut":
                _control.IsSpellPointsVisible = false;
                _control.IsCharacteristicVisible = false;
                _control.IsHammerSelected = false;
                _control.IsFlameSwordSelected = false;
                _control.IsReptileSkinSelected = true;

                break;
            default:
                _control.IsSpellPointsVisible = false;
                _control.IsCharacteristicVisible = false;
                _control.IsHammerSelected = false;
                _control.IsFlameSwordSelected = false;

                break;
        }

        CheckRequirements(spell);
    }
}
