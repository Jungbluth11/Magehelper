namespace Magehelper.ViewModels.Controls;

public class CrystalBallControlViewModel : ITraditionArtifact, IRecipient<AddArtifactSpellDialogMessage>
{
    private readonly CrystalBall _crystalBall;

    public string ArtifactName => "Kristallkugel";
    public string Material => CrystalBall.MaterialStrings[(int)_crystalBall.Material];


    public CrystalBallControlViewModel()
    {
        _crystalBall = Core.Core.GetInstance().CrystalBall ?? new CrystalBall();
        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(AddArtifactSpellDialogMessage message)
    {
        if (message.ArtifactName != ArtifactName)
        {
            return;
        }

        string? variantText = null;

        if (message.AdditionalValues.TryGetValue("variant", out string? variant))
        {
            variantText = variant;
        }

        else if (message.AdditionalValues.TryGetValue("variantDescription", out string? variantDescription))
        {
            variantText = variantDescription;
        }

        ArtifactSpell spell = _crystalBall.AddSpell(message.SpellName, variantText);
        WeakReferenceMessenger.Default.Send(new AddTraditionArtifactSpellMessage(spell, typeof(CrystalBall)));
    }

}
