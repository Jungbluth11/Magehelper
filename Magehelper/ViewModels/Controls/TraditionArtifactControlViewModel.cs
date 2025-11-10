namespace Magehelper.ViewModels.Controls;

public partial class TraditionArtifactControlViewModel : ObservableObject, IRecipient<AddTraditionArtifactSpellMessage>
{
    public Artifact Artifact { get; set; }
    public string ArtifactName { get; set; }
    public string ArtifactSpellCounterText { get; set; }
    public string ArtifactSpellCounterValue { get; set; } = string.Empty;
    public string ArtifactSpellName { get; set; }
    public ObservableCollection<ArtifactSpell> Spells { get; } = [];
    public AddArtifactSpellDialogViewModel AddArtifactSpellDialogViewModel { get; }

    public TraditionArtifactControlViewModel(Artifact artifact)
    {
        Artifact = artifact;
        ArtifactName = artifact.Name;
        ArtifactSpellName = TraditionalArtifactHelper.SpellDescriptor[artifact.Name];

        AddArtifactSpellDialogViewModel = artifact switch
        {
            Staff staff => new AddStaffSpellDialogViewModel(staff),
            CrystalBall crystalBall => new AddCrystalBallSpellDialogViewModel(crystalBall),
            BoneCub boneCub => new AddBoneCubSpellDialogViewModel(boneCub),
            _ => new(artifact)
        };

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

        if (Artifact is Staff or IMaxSpellArtifact)
        {
            SetSpellCounter();
            Spells.CollectionChanged += Spells_CollectionChanged;
        }

        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(AddTraditionArtifactSpellMessage message)
    {
        if (message.Type != Artifact.GetType())
        {
            return;
        }

        Spells.Add(message.ArtifactSpell);

        if (Artifact is Staff or IMaxSpellArtifact)
        {
            SetSpellCounter();
        }
    }

    private void Spells_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        SetSpellCounter();
    }


    public void SetSpellCounter()
    {
        ArtifactSpellCounterValue = Artifact is Staff staff
            ? staff.AfvRemain.ToString()
            : (Artifact as IMaxSpellArtifact)!.SpellsRemain.ToString();
    }

    [RelayCommand]
    private void Delete()
    {
        TraditionalArtifactHelper.GetArtifact[ArtifactName] = null;
        WeakReferenceMessenger.Default.Send(new DeleteTraditionArtifactMessage(Artifact.GetType()));
    }


    [RelayCommand]
    private void RemoveArtifactSpell(ArtifactSpell artifactSpell)
    {
        Artifact.RemoveSpell(artifactSpell.Guid);
        Spells.Remove(artifactSpell);

        if (Artifact is Staff or IMaxSpellArtifact)
        {
            SetSpellCounter();
        }
    }
}
