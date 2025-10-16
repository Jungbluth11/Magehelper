namespace Magehelper.ViewModels.Controls;

public partial class BowlControlViewModel : ObservableObject, IRecipient<AddArtifactSpellDialogMessage>
{
    private readonly Bowl _bowl;
    [ObservableProperty]
    private string _currentTemperatureCategoryStart = "Normal";
    [ObservableProperty]
    private string _currentTemperatureCategoryTarget = "Normal";
    [ObservableProperty]
    private int _fireAndIceDuration = 1;
    [ObservableProperty]
    private int _fireAndIceCost;
    [ObservableProperty]
    private bool _fireAndIceVisibility;
    public string[] TemperatureCategoryStrings => Bowl.TemperatureCategoryStrings;

    public BowlControlViewModel()
    {
        _bowl = Core.Core.GetInstance().Bowl ?? new Bowl();
        FireAndIceVisibility = _bowl.HasFireAndIce;
    }


    private void FireAndIce()
    {
        FireAndIceCost = _bowl.FireAndIce(CurrentTemperatureCategoryStart, CurrentTemperatureCategoryTarget, FireAndIceDuration);
    }

    partial void OnCurrentTemperatureCategoryStartChanged(string value)
    {
        FireAndIce();
    }

    partial void OnCurrentTemperatureCategoryTargetChanged(string value)
    {
        FireAndIce();
    }

    partial void OnFireAndIceDurationChanged(int value)
    {
        FireAndIce();
    }


    public void Receive(AddArtifactSpellDialogMessage message)
    {
        if (message.ArtifactName != _bowl.Name)
        {
            return;
        }

        ArtifactSpell spell = _bowl.AddSpell(message.SpellName);
        WeakReferenceMessenger.Default.Send(new AddTraditionArtifactSpellMessage(spell, typeof(Bowl)));
    }
}
