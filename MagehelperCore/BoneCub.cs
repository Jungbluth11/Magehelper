namespace Magehelper.Core;

public class BoneCub : Artifact
{
    private readonly BoneCubBaseData[] _baseDatas =
    [
        new("Kleine Knochenkeule", "1W6+", 2, 4),
        new("Mittelgroße Knochenkeule", "1W6+", 3, 3),
        new("Große Knochenkeule", "2W6+", 2, 2),
        new("Sägefischschwert", "1W6+", 2, 5),
        new("echsisches Szepter", "1W6+", 3, 4)
    ];


    private readonly string _type = string.Empty;

    public int AdditionalMtp { get; set; }

    public int SenseMagicSkill { get; set; }

    public int? Bf { get; set; }

    public int? EnsoulEntityLoyalty { get; set; }

    public string EnsoulEntityName { get; set; } = string.Empty;

    public string MtpString =>
        (from basedata in _baseDatas
         where basedata.Name == Type
         select basedata.TpString + (basedata.Tp + AdditionalMtp)).First();

    public string TpString =>
        (from basedata in _baseDatas
         where basedata.Name == Type
         select basedata.TpString + (basedata.Tp + AdditionalMtp / 3)).First();

    public string Type
    {
        get => _type;
        init
        {
            _type = value;

            Bf = (from basedata in _baseDatas
                  where basedata.Name == value
                  select basedata.Bf).First();
        }
    }

    public static string[] TypeStrings =>
    [
        "Kleine Knochenkeule",
        "Mittelgroße Knochenkeule",
        "Große Knochenkeule",
        "Sägefischschwert",
        "echsisches Szepter"
    ];

    /// <summary>
    ///     Constructor
    /// </summary>
    public BoneCub() : base("boneCub.json", "Knochenkeule")
    {
        _core.BoneCub = this;
    }
}