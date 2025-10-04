namespace Magehelper.ViewModels.Dialogs;

public partial class AddTraditionArtifactDialogViewModel : ObservableObject
{
    private readonly Core.Core _core = Core.Core.GetInstance();
    [ObservableProperty] private string _currentArtifact;
    [ObservableProperty] private string[] _staffLengthStrings = Staff.LengthStrings;
    [ObservableProperty] private string[] _staffMaterialStrings = Staff.MaterialStrings;
    [ObservableProperty] private string _currentStaffLength = Staff.LengthStrings[0];
    [ObservableProperty] private string _currentStaffMaterial = Staff.MaterialStrings[0];
    [ObservableProperty] private string[] _crystalBallMaterialStrings = CrystalBall.MaterialStrings;
    [ObservableProperty] private string _currentCrystalBallMaterial = CrystalBall.MaterialStrings[0];
    [ObservableProperty] private string[] _bowlMatrialStrings = Bowl.MaterialStrings;
    [ObservableProperty] private string _currentBowlMaterial = Bowl.MaterialStrings[0];
    [ObservableProperty] private bool _isBowlSelected;
    [ObservableProperty] private bool _isStaffSelected;
    [ObservableProperty] private bool _isCrystalBallSelected;
    [ObservableProperty] private int _additionalPasp;
    public List<string> Artifacts => [];

    public AddTraditionArtifactDialogViewModel()
    {
        foreach (string artifactName in _core.ArtifactNames)
        {
            if (!TraditionalArtifactHelper.IsInitialized[artifactName])
            {
                Artifacts.Add(artifactName);
            }
        }

        _currentArtifact = Artifacts[0];
    }

    partial void OnCurrentArtifactChanged(string value)
    {
        switch (value)
        {
            case "Magierstab":
                IsStaffSelected = true;
                IsCrystalBallSelected = false;
                IsBowlSelected = false;

                break;
            case "Kristallkugel":
                IsStaffSelected = false;
                IsBowlSelected = false;
                ToggleCrystalBall();

                break;
            case "Alchemistenschale":
                IsStaffSelected = false;
                IsCrystalBallSelected = false;
                IsBowlSelected = true;
                break;
            default:
                IsStaffSelected = false;
                IsCrystalBallSelected = false;
                IsBowlSelected = false;

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

        WeakReferenceMessenger.Default.Send(new AddTraditionArtifactMessage(CurrentArtifact, additionalValues));
    }
}
