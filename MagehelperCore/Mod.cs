namespace Magehelper.Core;

/// <summary>
/// The modification calculator
/// </summary>
public class Mod
{
    private int _modErleichterung;
    private decimal _modErschwerniss;
    private const string _textBor = "nur bei Kenntnis der gildenmagischen Repräsentation";
    private const string _textDru = "AsP Kosten für Erzwingen halbiert (min. 1 AsP)";
    private const string _textGeo = "wenn Zauber Merkmal des passenden Elements hat (Brobim-Geoden: Geister)";
    private const string _textAch = "ohne passenden geschliffenen Edelstein";
    private const string _textSrl = "nur Illusionszauber";
    private const string _textHalving = "gesamt Zuschlag wird halbiert";
    private bool _halvingMod;
    private readonly string[] _modNames =
    [
        "Veränderte Technik",
       "Veränderte Technik, zentral",
       "Halbierte Zauberdauer",
       "Kosten einsparen",
       "unfreiwilliges statt freiwilliges Zielobjekt",
       "Freiwillig statt unfreiwillig",
       "Verdoppelte Zauberdauer",
       "Mehrere Gefährten verzaubern (freiwillig)",
       "Vergrößerung von Reichweite o. Wirkungsradius",
       "Verkleinerung der Reichweite o. Wirkungsradius",
       "Verdopplung der Wirkungsdauer",
       "Halbierung der Wirkungsdauer",
       "Änderung von Aufrechterhalten auf feste Dauer",
       "Erzwingen"
    ];
    private readonly dynamic[][] _modBase =
    [
        ["+7 / Komponente",7],
        ["+12 / Komponente",12],
        ["+5 / Halbierung",5],
        ["+3 / -10% AsP",3],
        ["+5",5],
        ["+2",7],
        ["-3",-3],
        ["+3",3],
        ["+5 / Stufe",5],
        ["+3 / Stufe",3],
        ["+7",7],
        ["+3",3],
        ["+7",7],
        ["-1 pro 1/2/4/8/... AsP",-1]
    ];
    /// <summary>
    /// the MR to use.
    /// </summary>
    public int Mr { get; set; }
    /// <summary>
    /// THe current representation that's been used
    /// </summary>
    public string? CurrentRepresentation { get; private set; }
    /// <summary>
    /// The info text that's been displayed in GUI.
    /// </summary>
    public string? InfoText { get; private set; }
    /// <summary>
    /// If MR is used in calculation or not.
    /// </summary>
    public bool UseMr { get; set; }
    /// <summary>
    /// The names of the representations.
    /// </summary>
    public ReadOnlyCollection<string> Representations => _sourceArray.ToList().AsReadOnly();
    /// <summary>
    /// The modification data thats been used. (depends on representation)
    /// </summary>
    public ReadOnlyCollection<Modification>? Data { get; private set; }
    private readonly string[] _sourceArray =
    [
        "Borbarad",
        "Gildenmagier",
        "Scharlatan",
        "Druide",
        "Elf",
        "Geode",
        "Kristallomant",
        "sonstige"
    ];

    /// <summary>
    /// Constructor
    /// </summary>
    public Mod()
    {
        SetRepresentation("sonstige");
    }

    /// <summary>
    /// Reset the calculator.
    /// </summary>
    public void Reset()
    {
        _modErleichterung = 0;
        _modErschwerniss = 0;
        Mr = 0;
        UseMr = false;
    }

    /// <summary>
    /// Sets the representation and the data for it.
    /// </summary>
    /// <param name="representation">Name of the chosen representation.</param>
    public void SetRepresentation(string representation)
    {
        dynamic[][] modData = _modBase;
        List<Modification> modifications = [];
        CurrentRepresentation = representation;
        InfoText = string.Empty;
        switch (representation)
        {
            case "Borbarad":
                _halvingMod = true;
                InfoText = _textBor + ", " + _textHalving;
                modData[6] = ["-4", -4];
                break;
            case "Gildenmagier":
                _halvingMod = true;
                InfoText = _textHalving;
                modData[6] = ["-4", -4];
                break;
            case "Scharlatan":
                _halvingMod = true;
                InfoText = _textSrl + ", " + _textHalving;
                break;
            case "Druide":
                _halvingMod = false;
                InfoText = _textDru;
                break;
            case "Elf":
                _halvingMod = false;
                modData[10] = ["+4", 4];
                break;
            case "Geode":
                _halvingMod = false;
                InfoText = _textGeo;
                modData[3] = ["+1 / -10% AsP", 1];
                modData[8] = ["+3 / Stufe", 3];
                modData[9] = ["+1 / Stufe", 1];
                modData[10] = ["+5", 5];
                modData[11] = ["+1", 1];
                break;
            case "Kristallomant":
                _halvingMod = false;
                InfoText = _textAch;
                modData =
                [
                    ["+14 / Komponente",14],
                    ["+24 / Komponente",24],
                    ["+10 / Halbierung",10],
                    ["+6 / -10% AsP",6],
                    ["+10",10],
                    ["+4",4],
                    ["-3",-3],
                    ["+6",6],
                    ["+10 / Stufe",10],
                    ["+6 / Stufe",6],
                    ["+14",14],
                    ["+6",6],
                    ["+14",14],
                    ["-1 pro 2/4/8/16/... AsP",-1]
                ];
                break;
            default:
                _halvingMod = false;
                break;
        }
        for (int i = 0; i < 14; i++)
        {
            modifications.Add(new Modification(_modNames[i], modData[i][0], modData[i][1]));
        }
        Data = modifications.AsReadOnly();
    }

    public void Add(int mod)
    {
        if (mod < 0)
        {
            _modErleichterung += mod;
        }
        else
        {
            _modErschwerniss += mod;
        }
    }

    public void Remove(int mod)
    {
        if (mod < 0)
        {
            _modErleichterung -= mod;
        }
        else
        {
            _modErschwerniss -= mod;
        }
    }

    public int Calculate()
    {
        int modGesamt;
        if (_halvingMod)
        {
            modGesamt = (int)Math.Ceiling(_modErschwerniss / 2 + _modErleichterung);
        }
        else
        {
            modGesamt = (int)_modErschwerniss + _modErleichterung;
        }
        if (UseMr)
        {
            modGesamt += Mr;
        }
        return modGesamt;
    }
}
