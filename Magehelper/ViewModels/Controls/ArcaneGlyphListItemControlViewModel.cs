namespace Magehelper.ViewModels.Controls;

public partial class ArcaneGlyphListItemControlViewModel : ObservableObject, IRecipient<ArcaneGlyphDurationChangedMessage>
{
    public string Guid { get; }

    [ObservableProperty] private string _duration = string.Empty;

    public string Name { get; }
    public string AppliedTo { get; }

    public ArcaneGlyphListItemControlViewModel(ArcaneGlyph arcaneGlyph)
    {
        Guid = arcaneGlyph.Guid;
        Name = arcaneGlyph.Name;
        AppliedTo = $" Angebracht an/auf: {arcaneGlyph.AppliedTo}";

        if (arcaneGlyph.Duration == null && arcaneGlyph.AdditionalGlyphs.Any(a => a.Name == "Zusatzzeichen Satinavs Siegel"))
        {
            Duration = "(bis zur nächsten Wintersonnenwende aktiv)";
        }
        else if (arcaneGlyph.Duration == null || arcaneGlyph.AdditionalGlyphs.Any(a => a.Name == "Zusatzzeichen Kraftquellenspeisung"))
        {
            Duration = "(Permanent aktiv)";
        }
        else
        {
            SetDuration((int)arcaneGlyph.RemainingDuration!);
        }

        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(ArcaneGlyphDurationChangedMessage message)
    {
        if (message.Guid != Guid)
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

        Duration = duration == 0 ? "(Erloschen)" : $"({duration} {unit} verbleibend)";
    }

    [RelayCommand]
    private void Delete()
    {
        Core.Core.Instance.ArcaneGlyphs!.Remove(Guid);
        WeakReferenceMessenger.Default.Send(new DeleteArcaneGlyphMessage(this));
    }
}
