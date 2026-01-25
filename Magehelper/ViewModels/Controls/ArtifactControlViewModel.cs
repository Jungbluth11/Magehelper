namespace Magehelper.ViewModels.Controls;

public partial class ArtifactControlViewModel : ObservableObject
{
    private readonly Artifacts _artifacts = Core.Core.GetInstance().Artifacts!;
    private readonly string _guid;
    private readonly string _name;
    private readonly string _type;
    [ObservableProperty] private string _currentCharges = string.Empty;
    [ObservableProperty] private string _shortDescription;
    [ObservableProperty] private bool _isActivateButtonEnabled = true;
    public string ExtendedDescription { get; }
    public bool IsActivateButtonVisible { get; }
    public bool IsRechargeButtonVisible { get; }

    public ArtifactControlViewModel(Artifact artifact)
    {
        _guid = artifact.Guid;
        _name = artifact.Name;
        _type = artifact.TypeString;

        switch (artifact.Type)
        {
            case ArtifactType.Semipermanent:
                _type += $" - Intervall: {artifact.IntervalString}";

                break;
            case ArtifactType.Rechargeable:
                _currentCharges = $" - {artifact.CurrentCharges} Aufladungen";
                IsRechargeButtonVisible = true;

                break;
        }

        ShortDescription = $"{artifact.Name} ({_type}{CurrentCharges})";

        if (artifact.Type != ArtifactType.Permanent)
        {
            IsActivateButtonVisible = true;
        }

        ExtendedDescription = artifact.Description;
    }

    partial void OnCurrentChargesChanged(string value)
    {
        ShortDescription = $"{_name} ({_type} - {CurrentCharges} Aufladungen)";
    }

    [RelayCommand]
    private void ActivateArtifact()
    {
        _artifacts.ActivateArtifact(_guid);

        if (_type == "Einmalig")
        {
           ShortDescription = $"{_name} (verbraucht)";
        }
        else
        {
            CurrentCharges = _artifacts[_guid].CurrentCharges.ToString()!;
        }

        if (_artifacts[_guid].CurrentCharges == 0)
        {
            IsActivateButtonEnabled = false;
        }
    }

    [RelayCommand]
    private void DeleteArtifact()
    {
        _artifacts.DeleteArtifact(_guid);
        WeakReferenceMessenger.Default.Send(new DeleteArtifactMessage(this));
    }

    [RelayCommand]
    private void RechargeArtifact()
    {
        _artifacts.RechargeArtifact(_guid);
        CurrentCharges = _artifacts[_guid].CurrentCharges.ToString()!;
        IsActivateButtonEnabled = true;
    }
}
