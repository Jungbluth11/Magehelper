namespace Magehelper.Core;

public record struct ArcaneGlyph
{
    public string Guid { get; init; }
    public string Name { get; init; }
    public AdditionalGlyph[] AdditionalGlyphs { get; init; }
    public string AppliedTo { get; init; }
    public double Size { get; init; }
    public int Rkw { get; init; }
    public int Rkp { get; init; }
    public int Cost { get; init; }
    public int? Duration { get; init; }
    public int? RemainingDuration { get; set; }
}
