using DSAUtils.Settings.Aventurien;

namespace Magehelper.ViewModels.Dialogs;

public partial class AddArtifactSpellDialogViewModel : ObservableObject
{
    private readonly Character? _character = Core.Core.GetInstance().Character;
    private int _flameSwordFailureDamage;
    private int _pointsRemain;
    [ObservableProperty] private Artifact? _artifact;
    [ObservableProperty] private bool _areFlameSwordRadioButtonsVisible = true;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddSpellCommand))]
    private bool _canAddSpell;

    [ObservableProperty] private bool _canRollFlameSword;
    [ObservableProperty] private bool _canRollHammer;
    [ObservableProperty] private bool _isCharacteristicVisible;
    [ObservableProperty] private bool _isCrystallBallSpell;
    [ObservableProperty] private bool _isErrorVisible;
    [ObservableProperty] private bool _isFlameSwordFailureTextVisible;
    [ObservableProperty] private bool _isFlameSwordFiveChecked;
    [ObservableProperty] private bool _isFlameSwordFourChecked;
    [ObservableProperty] private bool _isFlameSwordSelected;
    [ObservableProperty] private bool _isHammerSelected;
    [ObservableProperty] private bool _isReptileSkinSelected;
    [ObservableProperty] private bool _isSpellPointsVisible;
    [ObservableProperty] private bool _isSpellVariantVisible;
    [ObservableProperty] private bool _isStaffSpell;
    [ObservableProperty] private bool _isVariantDescriptionVisible;
    [ObservableProperty] private int _flameSwordFailure;
    [ObservableProperty] private int _hammerRkp;
    [ObservableProperty] private int _maxSpellPoints = 1;
    [ObservableProperty] private int _minSpellPoints = 1;
    [ObservableProperty] private int _spellPoints = 1;
    [ObservableProperty] private string _dialogTitle = string.Empty;
    [ObservableProperty] private string _errorText = string.Empty;
    [ObservableProperty] private string _flameSwordFailureText = string.Empty;
    [ObservableProperty] private string _selectedCharacteristic = string.Empty;
    [ObservableProperty] private string _selectedReptileSkinVariant = string.Empty;
    [ObservableProperty] private string _selectedSpellName = string.Empty;
    [ObservableProperty] private string _selectedSpellVariant = string.Empty;
    [ObservableProperty] private string _variantDescription = string.Empty;
    public string[] Characteristics => Aventurien.Zauber.merkmalsliste;
    public string[] ReptileSkinVariants => ["Chamäleon", "Speikobra"];

    public ObservableCollection<string> SpellNames { get; } = [];
    public string[] SpellVariants => ["Variante 1", "Variante 2", "Beschreibung eingeben"];

    private bool CanRoll(string ritualName)
    {
        return _character != null &&
               _character.Rituals!.Contains(_character.Rituals!.Single(r => r.Name == ritualName));
    }

    private bool CheckCanAddSpell()
    {
        return !IsErrorVisible;
    }

    private void CheckPoints()
    {
        if (_pointsRemain < SpellPoints)
        {
            ErrorText =
                $"Der Zauber verbraucht {SpellPoints} Volumenpunkte. Es sind nur noch {_pointsRemain} Volumenpunkte im Stab vorhanden";

            IsErrorVisible = true;
        }

        IsErrorVisible = false;
    }

    partial void OnArtifactChanged(Artifact? value)
    {
        DialogTitle = $"{TraditionalArtifactHelper.SpellDescriptor[Artifact!.Name]} hinzufügen";
        SpellNames.AddRange(from ArtifactSpell in Artifact.SpellsAvailable select ArtifactSpell.Name);
        SpellNames.Add("Apport");
        SelectedSpellName = SpellNames[0];

        if (Artifact is Staff)
        {
            IsStaffSpell = true;
            SelectedCharacteristic = Characteristics[0];
            SelectedReptileSkinVariant = ReptileSkinVariants[0];
            _pointsRemain = (Artifact as Staff)!.AfvRemain;
        }

        if (Artifact is CrystalBall)
        {
            IsCrystallBallSpell = true;
        }
    }

    partial void OnIsFlameSwordFiveCheckedChanged(bool value)
    {
        if (value)
        {
            FlameSwordFailure = 5;
        }
    }

    partial void OnIsFlameSwordFourCheckedChanged(bool value)
    {
        if (value)
        {
            FlameSwordFailure = 4;
        }
    }

    partial void OnSelectedSpellNameChanged(string value)
    {
        IsErrorVisible = false;
        ArtifactSpell spell = Artifact!.SpellsAvailable.First(a => a.Name == value);

        if (IsStaffSpell)
        {
            if (value != "Zauberspeicher" && value != "Doppeltes Maß")
            {
                SpellPoints = spell.Points;
            }

            CheckPoints();

            switch (value)
            {
                case "Merkmalsfokus":
                    IsSpellPointsVisible = false;
                    IsCharacteristicVisible = true;
                    IsHammerSelected = true;
                    IsFlameSwordSelected = false;

                    break;
                case "Doppeltes Maß":
                case "Zauberspeicher":
                    IsSpellPointsVisible = true;
                    IsCharacteristicVisible = false;
                    IsHammerSelected = true;
                    IsFlameSwordSelected = false;

                    MaxSpellPoints = Artifact.SpellsAvailable.Where(a => a.Name == value).ToArray()[0]
                        .Points;

                    MinSpellPoints = 1;
                    SpellPoints = 1;

                    if (value == "Doppeltes Maß")
                    {
                        SpellPoints = MaxSpellPoints;
                        MinSpellPoints = MaxSpellPoints;
                        MaxSpellPoints *= 2;
                    }

                    break;
                case "Hammer des Magus":
                    IsSpellPointsVisible = false;
                    IsCharacteristicVisible = false;
                    IsHammerSelected = true;
                    IsFlameSwordSelected = false;
                    CanRollHammer = CanRoll(value);

                    break;
                case "Flammenschwert":
                    IsSpellPointsVisible = false;
                    IsCharacteristicVisible = false;
                    IsHammerSelected = false;
                    IsFlameSwordSelected = true;
                    CanRollFlameSword = CanRoll(value);

                    break;
                case "Schuppenhaut":
                    IsSpellPointsVisible = false;
                    IsCharacteristicVisible = false;
                    IsHammerSelected = false;
                    IsFlameSwordSelected = false;
                    IsReptileSkinSelected = true;

                    break;
                default:
                    IsSpellPointsVisible = false;
                    IsCharacteristicVisible = false;
                    IsHammerSelected = false;
                    _isFlameSwordSelected = false;

                    break;
            }
        }
        else if (IsCrystallBallSpell)
        {
            if (value == "Bildergalerie")
            {
                IsSpellVariantVisible = true;
            }
            else
            {
                IsSpellVariantVisible = false;
                SelectedSpellVariant = SpellVariants.First();
            }
        }

        if (spell.Requirements == null)
        {
            return;
        }

        {
            foreach (string requirement in spell.Requirements)
            {
                try
                {
                    // ReSharper disable once UnusedVariable --- just checking if requirement is fulfilled
                    ArtifactSpell artifactSpell = Artifact!.BoundSpells.Single(a => a.Name == requirement);
                }
                catch
                {
                    ErrorText = $"Voraussetzung nicht erfüllt! Benötigt: {requirement}";
                    IsErrorVisible = true;
                }
            }
        }
    }

    partial void OnSelectedSpellVariantChanged(string value)
    {
        if (value == "Beschreibung eingeben")
        {
            IsVariantDescriptionVisible = true;
        }
        else
        {
            IsVariantDescriptionVisible = false;
            VariantDescription = string.Empty;
        }
    }


    partial void OnSpellPointsChanged(int value)
    {
        CheckPoints();
    }

    [RelayCommand(CanExecute = nameof(CheckCanAddSpell))]
    private void AddSpell()
    {
        Dictionary<string, string> additionalValues = [];

        if (IsStaffSpell)
        {
            additionalValues["spellPoints"] = SpellPoints.ToString();
            additionalValues["characteristic"] = SelectedCharacteristic;

            if (IsHammerSelected)
            {
                additionalValues["hammerRkp"] = HammerRkp.ToString();
            }

            if (IsFlameSwordSelected)
            {
                additionalValues["flameSwordFailure"] = FlameSwordFailure.ToString();

                if (FlameSwordFailure < 4)
                {
                    additionalValues["flameSwordFailureDamage"] = _flameSwordFailureDamage.ToString();
                }
            }

            if (IsReptileSkinSelected)
            {
                additionalValues["reptileSkinVariant"] = SelectedReptileSkinVariant;
            }
        }

        if (IsCrystallBallSpell)
        {
            if (IsSpellVariantVisible)
            {
                additionalValues["variant"] = SelectedSpellVariant;
            }

            if (IsVariantDescriptionVisible)
            {
                additionalValues["variantDescription"] = VariantDescription;
            }
        }

        WeakReferenceMessenger.Default.Send(new AddArtifactSpellDialogMessage(Artifact!.Name, SelectedSpellName,
            additionalValues));
    }

    [RelayCommand]
    private void RollFlameSword()
    {
        (int dice, int damage) = (Artifact as Staff)!.RollFlameSwordFailure();
        FlameSwordFailure = dice;
        _flameSwordFailureDamage = damage;
        AreFlameSwordRadioButtonsVisible = false;
        IsFlameSwordFailureTextVisible = true;

        FlameSwordFailureText = FlameSwordFailure switch
        {
            4 => "4 gewürfelt!\n7 Volumenpunkte verloren!",
            5 => "5 gewürfelt!\nStab kann keine weiteren Zauber aufnehmen!",
            6 => "6 gewürfelt!\nStab verloren!",
            _ => $"{dice} gewürfelt!\n{damage} Schaden bekommen! (1W20 + RkW/2)"
        };
    }


    [RelayCommand]
    private void RollHammer()
    {
        HammerRkp = _character!.RollRitual(_character.Rituals!.Single(r => r.Name == "Hammer des Magus")).pointsLeft;
    }
}
