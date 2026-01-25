namespace Magehelper.Core;

public record struct Artifact
{
    public string Guid { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public ArtifactType Type { get; init; }
    public ArtifactInterval? Interval { get; init; }
    public int? CurrentCharges { get; set; }
    public int? MaxCharges { get; init; }

    public string TypeString => Type switch
    {
        ArtifactType.Single => "Einmalig",
        ArtifactType.Rechargeable => "Wiederaufladbar",
        ArtifactType.Semipermanent => "Semipermanent",
        _ => "Permanent"
    };

    public string? IntervalString => Interval switch
    {
        ArtifactInterval.Day => "Tag",
        ArtifactInterval.Week => "Woche",
        ArtifactInterval.Month => "Monat",
        ArtifactInterval.Year => "Jahr",
        _ => null
    };
}
