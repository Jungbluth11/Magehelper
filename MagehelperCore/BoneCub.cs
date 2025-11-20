namespace Magehelper.Core;

public class BoneCub : TraditionArtifact
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
         select basedata.TpString + (basedata.Tp + Math.Floor((double)AdditionalMtp / 3))).First();

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

    public (int pointsLeft, int[] diceResult, string text) RollEnsoulEntity(int pAsp, int mod)
    {
        Character character = Core.GetInstance().Character!;
        if (!character.IsLoaded)
        {
            throw new ArgumentException("no character loaded");
        }

        (int pointsLeft, int[] diceResult, string text) = DSA.TaP(character.Kl,
                                                                character.In,
                                                                character.Ch,
                                                                character.Rkw["Geister binden"],
                                                                mod);

        EnsoulEntityLoyalty += pointsLeft < 0 ? 0 : pointsLeft + pAsp;

        return (pointsLeft, diceResult, text);
    }

    public void DeleteEnsoulEntity()
    {
        EnsoulEntityLoyalty = null;
        EnsoulEntityName = string.Empty;

    }

    public (int mod, int? failureResult) RollLoyalty()
    {
        int roll = DSA.Roll(1, 20)[0];

        if (roll < EnsoulEntityLoyalty!)
        {
            return ((int)EnsoulEntityLoyalty! / 2, null);
        }

        int failureResult = DSA.Roll(1, 20)[0];

        if (failureResult > EnsoulEntityLoyalty!)
        {
            EnsoulEntityLoyalty = 0;
        }

        return (-((int)EnsoulEntityLoyalty! / 2), failureResult);

    }

    public (int pointsLeft, int[] diceResult, string text) RollSenseMagic()
    {
        Character character = Core.GetInstance().Character!;
        if (!character.IsLoaded)
        {
            throw new ArgumentException("no character loaded");
        }

        (int pointsLeft, int[] diceResult, string text) = DSA.TaP(character.Kl,
                                                                character.In,
                                                                character.Ch,
                                                                character.Rkw["Geister binden"]);

        if (SenseMagicSkill == 0)
        {
            SenseMagicSkill = (int)Math.Floor((double)pointsLeft / 2);
        }
        else
        {
            SenseMagicSkill += (int)Math.Floor((double)pointsLeft / 4);

        }

        return (pointsLeft, diceResult, text);
    }

    public void DecreaseBf(int points)
    {
        if (Bf == -5)
        {
            Bf = null;
        }
        else
        {
            Bf -= points;
        }
    }
}
