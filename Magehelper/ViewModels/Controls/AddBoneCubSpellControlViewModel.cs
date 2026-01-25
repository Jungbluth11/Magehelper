namespace Magehelper.ViewModels.Controls;

public partial class AddBoneCubSpellControlViewModel(BoneCub boneCub) : ObservableObject
{
    [ObservableProperty] private bool _canRollEnsoulEntity;
    [ObservableProperty] private bool _isEnsoulEntityLoyaltyVisible;
    [ObservableProperty] private bool _isEnsoulEntityNameVisible;
    [ObservableProperty] private bool _isEnsoulEntitySelected;
    [ObservableProperty] private bool _isSenseMagicSelected;
    [ObservableProperty] private bool _isPointsVisible;
    [ObservableProperty] private bool _isRollEnsoulEntityTextVisible;
    [ObservableProperty] private bool _isRollLoyaltyFailure;
    [ObservableProperty] private bool _isRollLoyaltyVisible;
    [ObservableProperty] private int _ensoulEntityLoyalty;
    [ObservableProperty] private int _ensoulEntityMod;
    [ObservableProperty] private int _points;
    [ObservableProperty] private string _ensoulEntityName = string.Empty;
    [ObservableProperty] private string _rollEnsoulEntityText = string.Empty;
    [ObservableProperty] private string _rollLoyaltyText = string.Empty;
    [ObservableProperty] private string _pointsDescriptor = "Punkte:";

    [RelayCommand]
    private void RollEnsoulEntity()
    {
        (int pointsLeft, int[] diceResult, string text) =
            boneCub.RollEnsoulEntity(Points, EnsoulEntityMod);

        EnsoulEntityLoyalty = pointsLeft + Points;

        RollEnsoulEntityText =
            $"Loyalität des Wesens beträgt {pointsLeft + Points}\n Gewürfelt: {diceResult[0]}/{diceResult[1]}/{diceResult[2]}";

        if (!string.IsNullOrEmpty(text))
        {
            RollEnsoulEntityText += $" ({text})";
        }

        IsRollEnsoulEntityTextVisible = true;
        IsEnsoulEntityLoyaltyVisible = false;
    }

    [RelayCommand]
    private void RollSenseMagic()
    {
        boneCub.RollSenseMagic();
    }


    [RelayCommand]
    private void RollLoyalty()
    {
        (int mod, int? failureResult) = boneCub.RollLoyalty();
        RollLoyaltyText = $"(Modifikator für Ritual {mod}";

        if (failureResult != null && boneCub.EnsoulEntityLoyalty == 0)
        {
            RollLoyaltyText +=
                "\nZweite LO-Probe misslungen! Wesen verlässt die Knochenkeule oder bleibt mit LO 0 erhalten (WDZ 167)";
        }
    }
}
