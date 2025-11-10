namespace Magehelper.ViewModels.Controls;

public partial class RingOfLifeControlViewModel : ObservableObject, IRecipient<AddArtifactSpellDialogMessage>
{
    private readonly RingOfLife _ringOfLife;

    public string ArtifactName => "Ring des Lebens";

    public RingOfLifeControlViewModel()
    {
        _ringOfLife = Core.Core.GetInstance().RingOfLife!;
        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(AddArtifactSpellDialogMessage message)
    {
        if (message.ArtifactName != _ringOfLife.Name)
        {
            return;
        }

        ArtifactSpell spell = _ringOfLife.AddSpell(message.SpellName);
        WeakReferenceMessenger.Default.Send(new AddTraditionArtifactSpellMessage(spell, typeof(RingOfLife)));
    }
}
