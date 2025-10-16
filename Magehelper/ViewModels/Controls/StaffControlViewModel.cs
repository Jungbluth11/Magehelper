namespace Magehelper.ViewModels.Controls;

public partial class StaffControlViewModel : ObservableObject, IRecipient<AddArtifactSpellDialogMessage>
{
    private readonly Core.Core _core = Core.Core.GetInstance();
    private readonly Staff _staff;
    [ObservableProperty] private string _flameSwordFailureFourText = string.Empty;
    [ObservableProperty] private bool _isFlameswordFailure;
    [ObservableProperty] private bool _isFlameSwordFailureFourVisible;
    [ObservableProperty] private bool _isFlameSwordFailureFiveVisible;
    public string FlameSwordFailureFiveText => "Mit 5 misslungen (Stab kann keine Zauber mehr aufnehmen)";
    public string Pasp => _staff.Pasp.ToString();
    public string Material => Staff.MaterialStrings[_staff.Material];
    public string Length => Staff.LengthStrings[_staff.Length];

    public StaffControlViewModel()
    {
        _staff = _core.Staff ?? new();

        if (_staff.LostPoints > 0)
        {
            SetFlameSwordFailureFour();
            IsFlameswordFailure = true;
            IsFlameSwordFailureFourVisible = true;
        }

        // ReSharper disable once InvertIf --- improves readability
        if (_staff.IsFlameSwordFive)
        {
            IsFlameswordFailure = true;
            IsFlameSwordFailureFiveVisible = true;
        }

        WeakReferenceMessenger.Default.Register(this);
    }

    private void SetFlameSwordFailureFour()
    {
        if (_staff.LostPoints > 0)
        {
            FlameSwordFailureFourText =
                $"Mit 4 Misslungen ({_staff.LostPoints / 7} mal; {_staff.LostPoints} Volumenpunkte verloren)";

            IsFlameSwordFailureFourVisible = true;
        }
        else
        {
            IsFlameSwordFailureFiveVisible = false;
        }

    }

    public void Receive(AddArtifactSpellDialogMessage message)
    {
        if (message.ArtifactName != _staff.Name)
        {
            return;
        }

        switch (message.SpellName)
        {
            case "Hammer des Magus":
                _staff.HammerRkp = int.Parse(message.AdditionalValues["rkp"]);

                break;
            case "Flammenschwert":
                if (int.Parse(message.AdditionalValues["flameSwordFailure"]) == 0)
                {
                    WeakReferenceMessenger.Default.Send(new EnableTabMessage("Flammenschwert"));
                }
                else
                {
                    switch (int.Parse(message.AdditionalValues["flameSwordFailure"]))
                    {
                        case < 4:
                            if (_core.Character != null && _core.Character.LeP != 0)
                            {
                                _core.Character.LeP -= int.Parse(message.AdditionalValues["flameSwordFailureDamage"]);
                            }

                            break;
                        case 4:
                            _staff.LostPoints += 7;
                            SetFlameSwordFailureFour();

                            break;
                        case 5:
                            _staff.IsFlameSwordFive = true;

                            break;
                        case 6:
                            _core.Staff = null;
                            WeakReferenceMessenger.Default.Send(new DeleteTraditionArtifactMessage(typeof(Staff)));

                            break;
                    }

                    return;
                }

                break;
            case "Schuppenhaut":
                _staff.ReptileSkinVariant = message.AdditionalValues["reptileSkinVariant"];

                break;
            case "Zauberspeicher":
                WeakReferenceMessenger.Default.Send(
                    new EnableTabMessage("Zauberspeicher", int.Parse(message.AdditionalValues["spellPoints"])));

                break;
        }

        ArtifactSpell spell = _staff.AddSpell(message.SpellName,
            message.AdditionalValues["characteristic"],
            int.Parse(message.AdditionalValues["spellPoints"]));

        WeakReferenceMessenger.Default.Send(new AddTraditionArtifactSpellMessage(spell, typeof(Staff)));
    }
}
