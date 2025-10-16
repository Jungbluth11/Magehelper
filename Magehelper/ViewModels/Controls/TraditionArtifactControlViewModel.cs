namespace Magehelper.ViewModels.Controls;

public partial class TraditionArtifactControlViewModel : ObservableObject, IRecipient<AddTraditionArtifactSpellMessage>
{
    [ObservableProperty] private Artifact? _artifact;
    [ObservableProperty] private string _artifactName = string.Empty;
    public string ArtifactSpellName { get; set; } = string.Empty;
    public string ArtifactSpellCounterText { get; set; } = string.Empty;
    public string ArtifactSpellCounterValue { get; set; } = string.Empty;
    public ObservableCollection<ArtifactSpell> Spells { get; } = [];

    public TraditionArtifactControlViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
    }

    partial void OnArtifactChanged(Artifact? value)
    {
        ArtifactSpellName = TraditionalArtifactHelper.SpellDescriptor[Artifact!.Name];

        ArtifactSpellCounterText = Artifact switch
        {
            Staff => "Volumenpunkte:",
            IMaxSpellArtifact => "Verbleibende Zauber:",
            _ => string.Empty
        };

        foreach (ArtifactSpell spell in Artifact.BoundSpells)
        {
            ArtifactSpell artifactSpell = spell;
            if (Settings.GetInstance().AllowRemoveSpells)
            {
                artifactSpell.IsNew = true;
            }
            Spells.Add(artifactSpell);
        }

        if (Artifact is not (Staff or IMaxSpellArtifact))
        {
            return;
        }

        SetSpellCounter();
        Spells.CollectionChanged += Spells_CollectionChanged;
    }


    public void SetSpellCounter()
    {
        ArtifactSpellCounterValue = Artifact is Staff staff
            ? staff.AfvRemain.ToString()
            : (Artifact as IMaxSpellArtifact)!.SpellsRemain.ToString();
    }

    private void Spells_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        SetSpellCounter();
    }


    [RelayCommand]
    private void RemoveArtifactSpell(ArtifactSpell artifactSpell)
    {
        Artifact!.RemoveSpell(artifactSpell.Guid);
        Spells.Remove(artifactSpell);
    }

    public void Receive(AddTraditionArtifactSpellMessage message)
    {
        if (message.Type == Artifact!.GetType())
        {
            Spells.Add(message.ArtifactSpell);
        }
    }

    [RelayCommand]
    private void Delete()
    {
        TraditionalArtifactHelper.GetArtifact[ArtifactName] = null;
        WeakReferenceMessenger.Default.Send(new DeleteTraditionArtifactMessage(Artifact!.GetType()));
    }
}
