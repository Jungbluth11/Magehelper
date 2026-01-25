namespace Magehelper.ViewModels.Controls;

public partial class ArcaneGlyphControlViewModel : ObservableObject, IRecipient<ArcaneGlyphDurationChangedMessage>
{
    private readonly string _guid = string.Empty;

    [ObservableProperty] private string _duration = string.Empty;
    [ObservableProperty] private bool _isReactivateButtonEnabled;
    [ObservableProperty] private bool _isReactivateButtonVisible = true;

    public string Name { get; } = string.Empty;
    public string AppliedTo { get; } = string.Empty;
    public List<string> AdditionalGlyphs { get; } = [];
    public string Size { get; set; } = string.Empty;
    public string Rkw { get; } = string.Empty;
    public string Rkp { get; } = string.Empty;
    public string Cost { get; } = string.Empty;

    public ArcaneGlyphControlViewModel(ArcaneGlyph arcaneGlyph)
    {
        if (string.IsNullOrEmpty(arcaneGlyph.Guid))
        {
            IsReactivateButtonVisible = false;
            return;
        }

        _guid = arcaneGlyph.Guid;
        Name = arcaneGlyph.Name;
        AppliedTo = $" Angebracht an/auf: {arcaneGlyph.AppliedTo}";
        Rkw = $"RkW bei Erschaffung: {arcaneGlyph.Rkw}";
        Rkp = $"RkP*: {arcaneGlyph.Rkp}";
        Cost = $"Kosten: {arcaneGlyph.Cost}";
        Size = $"Größe: {arcaneGlyph.Size / 2} Finger";

        if (arcaneGlyph.AdditionalGlyphs != null)
        {
            for (int i = 0; i < arcaneGlyph.AdditionalGlyphs.Length; i++)
            {
                string additionalGlyph = arcaneGlyph.AdditionalGlyphs[i].Name;

                if (additionalGlyph == "Zusatzzeichen Zielbeschränkung")
                {
                    additionalGlyph += $" ({arcaneGlyph.AdditionalGlyphs![i].Value})";
                }

                if (additionalGlyph == "Zusatzzeichen Verkleinerung" && int.Parse(arcaneGlyph.AdditionalGlyphs![i].Value) > 1)
                {
                    Size = $"Größe: {arcaneGlyph.Size} Halbfinger";
                }

                AdditionalGlyphs.Add(additionalGlyph);
            }
        }

        if (arcaneGlyph.Duration == null && arcaneGlyph.AdditionalGlyphs!.Any(a => a.Name == "Zusatzzeichen Satinavs Siegel"))
        {
            Duration = "bis zur nächsten Wintersonnenwende aktiv";
        }
        else if (arcaneGlyph.Duration == null || arcaneGlyph.AdditionalGlyphs!.Any(a => a.Name == "Zusatzzeichen Kraftquellenspeisung"))
        {
            Duration = "Permanent aktiv";
            IsReactivateButtonVisible = false;
        }
        else
        {
            SetDuration((int)arcaneGlyph.RemainingDuration!);
        }

        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(ArcaneGlyphDurationChangedMessage message)
    {
        if (message.Guid != _guid)
        {
            return;
        }

        SetDuration(message.Duration);
    }

    private void SetDuration(int duration)
    {
        string unit = duration switch
        {
            >= 60 => "Monate",
            > 30 => "Monat",
            > 1 => "Tage",
            _ => "Tag",
        };

        Duration = duration == 0 ? "Zauberzeichen erloschen!" : $"{duration} {unit} verbleibend";
        IsReactivateButtonEnabled = duration == 0;
    }

    [RelayCommand]
    private void Reactivate()
    {
        Core.Core.GetInstance().ArcaneGlyphs!.Reactivate(_guid);
        int newDuration = (int)Core.Core.GetInstance().ArcaneGlyphs![_guid].Duration!;
        SetDuration(newDuration);

        WeakReferenceMessenger.Default.Send(new ArcaneGlyphDurationChangedMessage(_guid, newDuration));
    }
}
