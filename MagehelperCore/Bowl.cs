namespace Magehelper.Core;

public class Bowl : TraditionArtifact
{
    private readonly Dictionary<string, int> _temperatureCategoryValues = new()
    {
        { "Niederhöllen", 0 },
        { "Namenlose Kälte", 1 },
        { "Grimmfrost", 2 },
        { "Firunskälte", 3 },
        { "Eiseskälte", 4 },
        { "Normal", 5 },
        { "Praiossommer", 6 },
        { "Khomglut", 7 },
        { "Kochendes Wasser", 8 },
        { "Backofen", 9 },
        { "Kohlenglut", 10 },
        { "Vulkanglut", 11 },
        { "Eisenschmelze", 12 }
    };

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
        Readfile();
    }

    internal new void Readfile()
    {
        if (_core.XmlDoc == null)
        {
            return;
        }

        XmlNode node = GetTraditionArtifactNode();
        XmlAttributeCollection data = node.ChildNodes[0]!.Attributes!;
        Material = (BowlMaterial)int.Parse(data["material"]!.Value);
        base.Readfile();
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
        int temperatureValueStart = _temperatureCategoryValues[temperatureCategoryStart];
        int temperatureValueTarget = _temperatureCategoryValues[temperatureCategoryTarget];


        return (temperatureValueTarget - temperatureValueStart - 1) * duration;
    }
}
