namespace Magehelper.ViewModels.Dialogs;

public partial class AddTraditionArtifactDialogViewModel : ObservableObject
{
    private readonly Core.Core _core = Core.Core.Instance;
    [ObservableProperty] private string _currentArtifact = string.Empty;
    [ObservableProperty] private string[] _staffLengthStrings = Staff.LengthStrings;
    [ObservableProperty] private string[] _staffMaterialStrings = Staff.MaterialStrings;
    [ObservableProperty] private string _currentStaffLength = Staff.LengthStrings[0];
    [ObservableProperty] private string _currentStaffMaterial = Staff.MaterialStrings[0];
    [ObservableProperty] private string[] _crystalBallMaterialStrings = CrystalBall.MaterialStrings;
    [ObservableProperty] private string _currentCrystalBallMaterial = CrystalBall.MaterialStrings[0];
    [ObservableProperty] private string[] _bowlMatrialStrings = Bowl.MaterialStrings;
    [ObservableProperty] private string _currentBowlMaterial = Bowl.MaterialStrings[0];
    [ObservableProperty] private string[] _boneCubTypeStrings = BoneCub.TypeStrings;
    [ObservableProperty] private string _currentBoneCubType = BoneCub.TypeStrings[0];
    [ObservableProperty] private bool _isBowlSelected;
    [ObservableProperty] private bool _isStaffSelected;
    [ObservableProperty] private bool _isCrystalBallSelected;
    [ObservableProperty] private bool _isBoneCubSelected;
    [ObservableProperty] private int _additionalPasp;
    public List<string> Artifacts { get; } = [];

    public AddTraditionArtifactDialogViewModel()
    {
        foreach (string artifactName in _core.TraditionArtifactNames)
        {
            if (!TraditionalArtifactHelper.IsInitialized[artifactName])
            {
                Artifacts.Add(artifactName);
            }
        }
    }

    partial void OnCurrentArtifactChanged(string value)
    {
        switch (value)
        {
            case "Magierstab":
                IsStaffSelected = true;
                IsCrystalBallSelected = false;
                IsBowlSelected = false;
                IsBoneCubSelected = false;
                break;
            case "Kristallkugel":
                IsStaffSelected = false;
                IsBowlSelected = false;
                IsBoneCubSelected = false;
                ToggleCrystalBall();
                break;
            case "Alchemistenschale":
                IsStaffSelected = false;
                IsCrystalBallSelected = false;
                IsBowlSelected = true;
                break;
            case "Knochenkeule":
                IsStaffSelected = false;
                IsCrystalBallSelected = false;
                IsBowlSelected = false;
                IsBoneCubSelected = true;
                break;
            default:
                IsStaffSelected = false;
                IsCrystalBallSelected = false;
                IsBowlSelected = false;
                IsBoneCubSelected = false;
                break;
        }
    }

    partial void OnCurrentStaffLengthChanged(string value)
    {
        ToggleCrystalBall();
    }

    private void ToggleCrystalBall()
    {
        if (CurrentArtifact == "Kristallkugel" || CurrentStaffLength == "Magierstab m. Kristallkugel")
        {
            IsCrystalBallSelected = true;
        }
        else
        {
            IsCrystalBallSelected = false;
        }
    }

    [RelayCommand]
    private void Submit()
    {
        Dictionary<string, string> additionalValues = [];

        if (IsStaffSelected)
        {
            additionalValues["StaffLength"] = CurrentStaffLength;
            additionalValues["StaffMaterial"] = CurrentStaffMaterial;
            additionalValues["AdditionalPasp"] = AdditionalPasp.ToString();
        }

        if (IsCrystalBallSelected)
        {
            additionalValues["CrystalBallMaterial"] = CurrentCrystalBallMaterial;
        }

        if (IsBowlSelected)
        {
            additionalValues["BowlMaterial"] = CurrentBowlMaterial;
        }

        if (IsBoneCubSelected)
        {
            additionalValues["BoneCubType"] = CurrentBoneCubType;
        }

        WeakReferenceMessenger.Default.Send(new AddTraditionArtifactMessage(CurrentArtifact, additionalValues));
    }
}
