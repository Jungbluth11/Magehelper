using DSAUtils.Settings.Aventurien;

namespace Magehelper.ViewModels.Controls;

public partial class AddStaffSpellControlViewModel : ObservableObject
{
    private readonly Character? _character = Core.Core.Instance.Character;
    private readonly Staff _staff;
    [ObservableProperty] private bool _isCharacteristicVisible;
    [ObservableProperty] private bool _isFlameSwordFailureTextVisible;
    [ObservableProperty] private bool _isFlameSwordFiveChecked;
    [ObservableProperty] private bool _isFlameSwordFourChecked;
    [ObservableProperty] private bool _isFlameSwordSelected;
    [ObservableProperty] private bool _isHammerSelected;
    [ObservableProperty] private bool _isReptileSkinSelected;
    [ObservableProperty] private bool _isSpellPointsVisible;
    [ObservableProperty] private bool _isSpellVariantVisible;
    [ObservableProperty] private int _flameSwordFailure;
    [ObservableProperty] private int _hammerRkp;
    [ObservableProperty] private int _maxSpellPoints = 1;
    [ObservableProperty] private int _minSpellPoints = 1;
    [ObservableProperty] private int _spellPoints = 1;
    [ObservableProperty] private string _flameSwordFailureText = string.Empty;
    [ObservableProperty] private string _selectedCharacteristic = string.Empty;
    [ObservableProperty] private string _selectedReptileSkinVariant = string.Empty;
    public bool AreFlameSwordRadioButtonsVisible { get; set; } = true;
    public bool CanRollFlameSword { get; set; }
    public bool CanRollHammer { get; set; }
    public string[] Characteristics => Aventurien.Zauber.merkmalsliste;
    public int FlameSwordFailureDamage { get; private set; }
    public string[] ReptileSkinVariants => ["Chamäleon", "Speikobra"];

    public AddStaffSpellControlViewModel(Staff staff)
    {
        _staff = staff;
        SelectedCharacteristic = Characteristics[0];
        SelectedReptileSkinVariant = ReptileSkinVariants[0];
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

    [RelayCommand]
    private void RollFlameSword()
    {
        (int dice, int damage) = _staff.RollFlameSwordFailure();
        FlameSwordFailure = dice;
        FlameSwordFailureDamage = damage;
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
