namespace Magehelper.Core;

public class Bowl : Artifact
{
    private readonly int[] _temperatureCategoryValues =
    [
        -5,
        -4,
        -3,
        -2,
        -1,
        0,
        1,
        2,
        3,
        4,
        5,
        6,
        7
    ];

    public bool HasFireAndIce { get; private set; }

    public static string[] TemperatureCategoryStrings =>
    [
        "Niederhöllen",
        "Namenlose Kälte",
        "Grimmfrost",
        "Firunskälte",
        "Eiseskälte",
        "Normal",
        "Praiossommer",
        "Khomglut",
        "Kochendes Wasser",
        "Backofen",
        "Kohlenglut",
        "Vulkanglut",
        "Eisenschmelze"
    ];

    public static string[] MaterialStrings => ["Silber", "Mondsilber"];

    public BowlMaterial Material { get; set; } = BowlMaterial.Silber;

    public Bowl() : base("bowl.json", "Alchemistenschale")
    {
        _core.Bowl = this;
    }

    public new ArtifactSpell AddSpell(string spellName, string? guid = null)
    {
        try
        {
            ArtifactSpell spell = spellsAvailable!.Single(a => a.Name == spellName);
            if (spellName == "Feuer und Eis")
            {
                HasFireAndIce = true;
            }
            return AddSpell(spell, guid);
        }
        catch (InvalidOperationException e)
        {
            if (e.Message == "Sequence contains no matching element")
            {
                throw new ArgumentException("Spell doesn't exist", nameof(spellName));
            }

            throw;
        }
    }

    public int FireAndIce(string temperatureCategoryStart, string temperatureCategoryTarget, int duration)
    {
        int temperatureValueStart = _temperatureCategoryValues[Array.IndexOf(TemperatureCategoryStrings, temperatureCategoryStart)];
        int temperatureValueTarget = _temperatureCategoryValues[Array.IndexOf(TemperatureCategoryStrings, temperatureCategoryTarget)];
        return (Math.Abs(temperatureValueStart) + Math.Abs(temperatureValueTarget) - 1) * duration;
    }
}
