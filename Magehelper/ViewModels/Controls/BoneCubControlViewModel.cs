namespace Magehelper.ViewModels.Controls;

public partial class BoneCubControlViewModel : ObservableObject, IRecipient<AddArtifactSpellDialogMessage>
{
    private readonly BoneCub _boneCub;
    [ObservableProperty] private string _bf;
    [ObservableProperty] private string _tp;
    [ObservableProperty] private string _mtp;
    [ObservableProperty] private string _senseMagicSkill = string.Empty;
    [ObservableProperty] private string _ensoulEntityName = string.Empty;
    [ObservableProperty] private string _ensoulEntityLoyalty = string.Empty;
    [ObservableProperty] private bool _isSenseMagicSkillVisible;
    [ObservableProperty] private bool _isEnsoulEntityVisible;
    public string BoneCubType => _boneCub.Type;

    public BoneCubControlViewModel()
    {
        _boneCub = Core.Core.Instance.BoneCub ?? new();
        Bf = _boneCub.Bf == null ? "Unzerbrechlich" : _boneCub.Bf.ToString()!;
        Tp = _boneCub.TpString;
        Mtp = _boneCub.MtpString;

        if (_boneCub.SenseMagicSkill > 0)
        {
            SenseMagicSkill = _boneCub.SenseMagicSkill.ToString();
            IsSenseMagicSkillVisible = true;
        }

        if (_boneCub.EnsoulEntityName != string.Empty)
        {
            EnsoulEntityName = _boneCub.EnsoulEntityName;
            EnsoulEntityLoyalty = _boneCub.EnsoulEntityLoyalty.ToString()!;
            IsEnsoulEntityVisible = true;
        }

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
                if (string.IsNullOrEmpty(message.AdditionalValues["ensoulEntityName"]))
                {
                    _boneCub.EnsoulEntityName = message.AdditionalValues["ensoulEntityName"];
                    EnsoulEntityName = message.AdditionalValues["ensoulEntityName"];
                }

                EnsoulEntityLoyalty = _boneCub.EnsoulEntityLoyalty.ToString()!;

                break;
            case "Gespür der Keule":
                if (message.AdditionalValues.TryGetValue("points", out string? points))
                {
                    _boneCub.SenseMagicSkill = int.Parse(points);
                }

                SenseMagicSkill = _boneCub.SenseMagicSkill.ToString();

                break;
            case "Härte der Keule":
                _boneCub.DecreaseBf(int.Parse(message.AdditionalValues["points"]));
                Bf = _boneCub.Bf == null ? "Unzerbrechlich" : _boneCub.Bf.ToString()!;

                break;

            case "Kraft der Keule":
                _boneCub.AdditionalMtp += int.Parse(message.AdditionalValues["points"]);
                Tp = _boneCub.TpString;
                Mtp = _boneCub.MtpString;
                break;
        }

        if (message.AdditionalValues.TryGetValue("isRollLoyaltyFailure", out _))
        {
            EnsoulEntityLoyalty = "0";
        }

        ArtifactSpell spell = _boneCub.AddSpell(message.SpellName);
        WeakReferenceMessenger.Default.Send(new AddTraditionArtifactSpellMessage(spell, typeof(BoneCub)));
    }


    [RelayCommand]
    private void DeleteEnsoulEntity()
    {
        _boneCub.DeleteEnsoulEntity();
        EnsoulEntityName = string.Empty;
        EnsoulEntityLoyalty = string.Empty;
        IsEnsoulEntityVisible = false;
    }
}
