namespace Magehelper.ViewModels.Controls;

public partial class ObsidianDaggerControlViewModel : ObservableObject, IRecipient<AddArtifactSpellDialogMessage>
{
    private readonly ObsidianDagger _obsidianDagger;

    public ObsidianDaggerControlViewModel()
    {
        _obsidianDagger = Core.Core.GetInstance().ObsidianDagger!;
        WeakReferenceMessenger.Default.Register(this);
    }


    public void Receive(AddArtifactSpellDialogMessage message)
    {
        if (message.ArtifactName != _obsidianDagger.Name)
        {
            return;
        }

        ArtifactSpell spell = _obsidianDagger.AddSpell(message.SpellName);
        WeakReferenceMessenger.Default.Send(new AddTraditionArtifactSpellMessage(spell, typeof(ObsidianDagger)));
    }
}
