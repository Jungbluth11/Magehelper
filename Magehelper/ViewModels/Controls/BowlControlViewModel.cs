namespace Magehelper.ViewModels.Controls;

public partial class BowlControlViewModel : ObservableObject, IRecipient<AddArtifactSpellDialogMessage>
{
    private readonly Bowl _bowl;

    [ObservableProperty] private bool _fireAndIceVisibility;
    [ObservableProperty] private int _fireAndIceCost;
    [ObservableProperty] private int _fireAndIceDuration = 1;
    [ObservableProperty] private string _currentTemperatureCategoryStart = "Normal";
    [ObservableProperty] private string _currentTemperatureCategoryTarget = "Normal";
    public string Material => Bowl.MaterialStrings[(int)_bowl.Material];
    public string[] TemperatureCategoryStrings => Bowl.TemperatureCategoryStrings;

    public BowlControlViewModel()
    {
        _bowl = Core.Core.GetInstance().Bowl!;
        FireAndIceVisibility = _bowl.HasFireAndIce;
        WeakReferenceMessenger.Default.Register(this);
    }


    public void Receive(AddArtifactSpellDialogMessage message)
    {
        if (message.ArtifactName != _bowl.Name)
        {
            return;
        }

        if (message.SpellName == "Feuer und Eis")
        {
            FireAndIceVisibility = true;
        }

        ArtifactSpell spell = _bowl.AddSpell(message.SpellName);
        WeakReferenceMessenger.Default.Send(new AddTraditionArtifactSpellMessage(spell, typeof(Bowl)));
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
}
