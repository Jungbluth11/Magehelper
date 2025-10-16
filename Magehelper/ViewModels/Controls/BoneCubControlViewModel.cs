
namespace Magehelper.ViewModels.Controls;

public partial class BoneCubControlViewModel : ObservableObject, IRecipient<AddArtifactSpellDialogMessage>
{
    private readonly BoneCub _boneCub;
    [ObservableProperty] private string _bf;
    [ObservableProperty] private string _tp;
    [ObservableProperty] private string _mtp;
    [ObservableProperty] private string _senseMagicSkill = string.Empty;
    [ObservableProperty] private string _ensoulEntityName = string.Empty;
    [ObservableProperty] private string _ensoulEntityNameLoyalty = string.Empty;
    [ObservableProperty] private bool _isSenseMagicSkillVisible;
    [ObservableProperty] private bool _isEnsoulEntityVisible;
    public string BoneCubType => _boneCub.Type;

    public BoneCubControlViewModel()
    {
        _boneCub = Core.Core.GetInstance().BoneCub ?? new();
        Bf = _boneCub.Bf == null ? "Unzerbrechlich" : _boneCub.Bf.ToString()!;
        Tp = _boneCub.TpString;
        Mtp = _boneCub.MtpString;

        WeakReferenceMessenger.Default.Register(this);
    }


    public void Receive(AddArtifactSpellDialogMessage message)
    {
        if (message.ArtifactName != _boneCub.Name)
        {
            return;
        }

        switch (message.SpellName)
        {
            case "Geist der Keule":
                //TODO
                EnsoulEntityName = _boneCub.EnsoulEntityName;
                EnsoulEntityNameLoyalty = _boneCub.EnsoulEntityLoyalty == null
                    ? string.Empty
                    : _boneCub.EnsoulEntityLoyalty.ToString()!;

                break;
            case "Gespür der Keule":
                //TODO
                SenseMagicSkill = _boneCub.SenseMagicSkill.ToString();

                break;
            case "Härte der Keule":
                //TODO
                Bf = _boneCub.Bf == null ? "Unzerbrechlich" : _boneCub.Bf.ToString()!;

                break;

            case "Kraft der Keule":
                //TODO
                Tp = _boneCub.TpString;
                Mtp = _boneCub.MtpString;
                break;
        }

        ArtifactSpell spell = _boneCub.AddSpell(message.SpellName);
        WeakReferenceMessenger.Default.Send(new AddTraditionArtifactSpellMessage(spell, typeof(BoneCub)));
    }
}
