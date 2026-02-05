namespace Magehelper.ViewModels.Dialogs;

public partial class AddArtifactDialogViewModel : ObservableObject
{
    [ObservableProperty] private bool _isChargesVisible;
    [ObservableProperty] private bool _isIntervalVisible;
    [ObservableProperty] private int _charges = 1;
    [ObservableProperty] private string _currentInterval;
    [ObservableProperty] private string _currentType;

    [NotifyCanExecuteChangedFor(nameof(AddArtifactCommand))]
    [ObservableProperty]
    private string _description = string.Empty;
    
    [ObservableProperty] private string _filePath = string.Empty;

    [NotifyCanExecuteChangedFor(nameof(AddArtifactCommand))]
    [ObservableProperty]
    private string _name = string.Empty;

    public string[] IntervalStrings => Artifacts.IntervalStrings;
    public string[] TypeStrings => Artifacts.TypeStrings;

    public AddArtifactDialogViewModel()
    {
        CurrentType = TypeStrings[0];
        CurrentInterval = IntervalStrings[0];
    }

    private bool CanAddArtifact()
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Description);
    }

    partial void OnCurrentTypeChanged(string value)
    {
        switch (value)
        {
            case "Wiederaufladbar":
                IsChargesVisible = true;
                IsIntervalVisible = false;

                break;
            case "Semipermanent":
                IsChargesVisible = false;
                IsIntervalVisible = true;

                break;
            default:
                IsChargesVisible = false;
                IsIntervalVisible = false;

                break;
        }
    }

    partial void OnFilePathChanged(string value)
    {
        Artifact artifact = Core.Core.Instance.Artifacts!.LoadArtifactFromFile(value);
        CurrentType = artifact.TypeString;
        Name = artifact.Name;
        Description = artifact.Description;

        if (artifact.MaxCharges.HasValue)
        {
            Charges = artifact.MaxCharges.Value;
        }

        if (artifact.IntervalString != null)
        {
            CurrentInterval = artifact.IntervalString;
        }
    }

    [RelayCommand(CanExecute = nameof(CanAddArtifact))]
    private void AddArtifact()
    {
        ArtifactType type = CurrentType switch
        {
            "Einmalig" => ArtifactType.Single,
            "Wiederaufladbar" => ArtifactType.Rechargeable,
            "Semipermanent" => ArtifactType.Semipermanent,
            _ => ArtifactType.Permanent
        };

        ArtifactInterval? interval;

        if (IsIntervalVisible)
        {
            interval = CurrentInterval switch
            {
                "Tag" => ArtifactInterval.Day,
                "Woche" => ArtifactInterval.Week,
                "Monat" => ArtifactInterval.Month,
                _ => (ArtifactInterval?) ArtifactInterval.Year
            };
        }
        else
        {
            interval = null;
        }

        Artifact artifact = Core.Core.Instance.Artifacts!.CreateArtifact(Name, Description, type, Charges, Charges, interval);

        WeakReferenceMessenger.Default.Send(new AddArtifactMessage(artifact));
    }
}
