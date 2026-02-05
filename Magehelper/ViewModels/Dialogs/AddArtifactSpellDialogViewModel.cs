namespace Magehelper.ViewModels.Dialogs;

public partial class AddArtifactSpellDialogViewModel : ObservableObject
{
    protected readonly Character? Character = Core.Core.Instance.Character;
    protected Action? AddSpellAction;
    protected Action<string>? SelectedSpellNameChangedAction;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddSpellCommand))]
    private bool _canAddSpell;

    [ObservableProperty] private bool _isErrorVisible;
    [ObservableProperty] private string _dialogTitle = string.Empty;
    [ObservableProperty] private string _errorText = string.Empty;
    [ObservableProperty] private string _selectedSpellName = string.Empty;
    public TraditionArtifact Artifact { get; }
    public ObservableObject? AddArtifactSpellControlViewModel { get; protected set; }
    public ObservableCollection<string> SpellNames { get; } = [];

    public AddArtifactSpellDialogViewModel(TraditionArtifact artifact)
    {
        Artifact = artifact;
        DialogTitle = $"{TraditionalArtifactHelper.SpellDescriptor[Artifact.Name]} hinzufügen";
        SpellNames.AddRange(from ArtifactSpell in Artifact.SpellsAvailable select ArtifactSpell.Name);
        SpellNames.Add("Apport");
        SelectedSpellName = SpellNames[0];
    }

    protected void AddArtifactSpell(string selectedSpellName, Dictionary<string, string> additionalValues)
    {
        WeakReferenceMessenger.Default.Send(new AddArtifactSpellDialogMessage(Artifact.Name, selectedSpellName,
            additionalValues));
    }

    protected void CheckRequirements(ArtifactSpell spell)
    {
        if (spell.Requirements == null)
        {
            return;
        }

        foreach (string requirement in spell.Requirements)
        {
            try
            {
                // ReSharper disable once UnusedVariable --- just checking if requirement is fulfilled
                ArtifactSpell artifactSpell = Artifact.BoundSpells.Single(a => a.Name == requirement);
            }
            catch
            {
                ErrorText = $"Voraussetzung nicht erfüllt! Benötigt: {requirement}";
                IsErrorVisible = true;
            }
        }
    }

    private bool CheckCanAddSpell()
    {
        return !IsErrorVisible;
    }

    partial void OnSelectedSpellNameChanged(string value)
    {
        IsErrorVisible = false;

        if (SelectedSpellNameChangedAction == null)
        {
            CheckRequirements(Artifact.SpellsAvailable.First(a => a.Name == value));
        }
        else
        {
            SelectedSpellNameChangedAction(value);
        }
    }


    [RelayCommand(CanExecute = nameof(CheckCanAddSpell))]
    private void AddSpell()
    {
        if (AddSpellAction == null)
        {
            AddArtifactSpell(SelectedSpellName, []);
        }
        else
        {
            AddSpellAction();
        }
    }
}
