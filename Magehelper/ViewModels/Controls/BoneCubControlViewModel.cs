

namespace Magehelper.ViewModels.Controls;

public partial class BoneCubControlViewModel : ObservableObject, ITraditionArtifact, IRecipient<AddArtifactSpellDialogMessage>
{
    private readonly BoneCub _boneCub;

    public BoneCubControlViewModel()
    {
        _boneCub = Core.Core.GetInstance().BoneCub ?? new BoneCub();
        WeakReferenceMessenger.Default.Register(this);
    }

    public string ArtifactName => "Knochenkeule";

    public void Receive(AddArtifactSpellDialogMessage message)
    {
        if (message.ArtifactName != ArtifactName)
        {
            return;
        }

        ArtifactSpell spell = _boneCub.AddSpell(message.SpellName);
        WeakReferenceMessenger.Default.Send(new AddTraditionArtifactSpellMessage(spell, typeof(BoneCub)));
    }
}
