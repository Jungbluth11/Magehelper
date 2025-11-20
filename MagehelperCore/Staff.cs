namespace Magehelper.Core;

public class Staff : TraditionArtifact
{
    private readonly List<string> _merkmalsfoki = [];
    private int _afvTotal;
    private int _afvUsed;
    public int AfvRemain => HasApport ? 0 : _afvTotal - _afvUsed - LostPoints;
    /// <summary>
    /// Material of the staff.
    /// </summary>
    public int Material { get; set; }
    /// <summary>
    /// Length of the staff.
    /// </summary>
    public int Length { get; set; }
    /// <summary>
    /// Additional pAsP that spend to create the staff.
    /// </summary>
    public int Pasp { get; set; }
    /// <summary>
    /// RkP* of the spell "Hammer des Magus" (if exist).
    /// </summary>
    public int HammerRkp { get; set; }
    public bool IsFlameSwordFive { get; set; }
    /// <summary>
    /// Names of the materials. (SRD 117ff)
    /// </summary>
    public static string[] MaterialStrings =>
    [
        "Normal",
        "Blutulme",
        "Bosparanie",
        "Steineiche",
        "Eisenbaum",
        "Mohagoni",
        "Zyklopenzeder",
        "Raschtulszeder",
        "Mammutbaum",
        "Walnussbaum",
        "Baum der Reisenden"
    ];
    /// <summary>
    /// Names of the length. (WdZ 108)
    /// </summary>
    public static string[] LengthStrings =>
    [
        "Magierstab als Stab",
        "Magierstab m. Kristallkugel",
        "Magierstab (kurz)",
        "Magierstab (sehr kurz)"
    ];

    public string ReptileSkinVariant { get; set; } = string.Empty;

    public int LostPoints { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public Staff() : base("staff.json", "Magierstab")
    {
        _core.Staff = this;
    }

    public void AfvTotal()
    {
        double afv;

        double lenghtMod = Length switch
        {
            1 => 27,
            2 => 18,
            3 => 15,
            _ => 24
        };

        double materialMod = Material switch
        {
            1 or 7 => 1,
            2 or 5 or 8 or 9 or 10 => -1,
            _ => 0
        };

        if (!IsFlameSwordFive)
        {
            afv = lenghtMod + materialMod + (double)Pasp / 3;
        }
        else
        {
            afv = 0;
        }
        if (HasApport)
        {
            afv = 0;
        }
        _afvTotal = (int)afv;
        _core.FileChanged = true;
    }

    /// <summary>
    /// Adds an spells to this artifact.
    /// </summary>
    /// <param name="spellName">Name of the spell.</param>
    /// <param name="characteristic">Characteristic of the spell.</param>
    /// <param name="points">Points spend for the spell.</param>
    /// <param name="guid">GUID of the spell. (Only used by <see cref="Core.ReadFileVersionSelector"/>)</param>
    /// <returns>An instance of <see cref="ArtifactSpell"/> that contains data about the chosen spell.</returns>
    /// <exception cref="ArgumentException"/>
    /// /// <exception cref="InvalidOperationException"/>
    public ArtifactSpell AddSpell(string spellName, string? characteristic = null, int points = 0, string? guid = null)
    {
        try
        {
            ArtifactSpell spell = SpellsAvailable.Single(s => s.Name == spellName);
            if (points == 0)
            {
                points = spell.Points;
            }
            switch (HeldentoolInterop.Rename(spellName, DSAUtils.HeldentoolInterop.Name.Offi))
            {
                case "Merkmalsfokus":
                    characteristic ??= string.Empty;
                    spell.Characteristic = characteristic;
                    _merkmalsfoki.Add(characteristic);
                    spell.DisplayText = spellName + " (" + characteristic + ")";
                    break;
                case "Hammer des Magus":
                    spell.DisplayText = spellName + " (3W6+" + HammerRkp + ")";
                    break;
                case "Doppeltes Maß":
                    spell.Points = points;
                    spell.DisplayText = spellName + " (" + points + " Volumenpunkte)";
                    break;
                case "Zauberspeicher":
                    spell.Points = points;
                    break;
                case "Flammenschwert":
                    _core.HasFlameSword = true;
                    break;
                case "Schuppenhaut":
                    spell.DisplayText = spellName + " (" + ReptileSkinVariant + ")";
                    break;
            }
            switch (Material)
            {
                case 4:
                    switch (spellName)
                    {
                        case "Modifikationsfokus":
                            spell.Points--;

                            break;
                        case "Flammenschwert":
                            spell.Points++;

                            break;
                    }

                    break;
                case 6:
                    if (spell.Characteristic == "Antimagie")
                    {
                        spell.Points += -2;
                    }
                    break;
                case 8:
                    int merkmale = _merkmalsfoki.Count(s => s.Equals("Einfluss") || s.Equals("Herrschaft"));

                    if (merkmale == 2)
                    {
                        points--;
                    }
                    break;
            }
            _afvUsed += points;
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

    /// <summary>
    /// Remove a spell with the given GUID from this artifact.
    /// </summary>
    /// <param name="guid">The GUID of the that should be removed.</param>
    /// <exception cref="ArgumentException"/>
    public new void RemoveSpell(string guid)
    {
        try
        {
            ArtifactSpell spell = boundSpells.Single(a => a.Guid == guid);
            int points = spell.Points;
            switch (spell.Name)
            {
                case "Merkmalsfokus":
                    _merkmalsfoki.Remove(spell.Characteristic);

                    break;
                case "Zauberspeicher":
                    _core.HasSpellStorage = false;

                    break;
                case "Flammenschwert":
                    _core.HasFlameSword = false;

                    break;
            }
            if (Material == 8)
            {
                int merkmale = _merkmalsfoki.Count(s => s.Equals("Einfluss") || s.Equals("Herrschaft"));

                if (merkmale == 2)
                {
                    points--;
                }
            }
            boundSpells.Remove(spell);
            _afvUsed -= points;
            _core.FileChanged = true;
        }
        catch
        {
            throw new ArgumentException("GUID doesn't exist", nameof(guid));
        }
    }

    public (int dice, int damage) RollFlameSwordFailure()
    {
        int flameSwordFailure = DSA.Roll(1, 6)[0];
        int damage = 0;

        if (flameSwordFailure < 4)
        {
            damage = DSA.Roll(1, 20)[0] + _core.Character!.Rkw["Gildenmagie"] / 2;
        }

        return (flameSwordFailure, damage);
    }

    internal new void ResetTool()
    {
        _afvTotal = 24;
        _afvUsed = 0;
        _merkmalsfoki.Clear();
        boundSpells.Clear();
        Material = 0;
        Length = 0;
        Pasp = 0;
        HammerRkp = 0;
        IsFlameSwordFive = false;
        HasApport = false;
        ReptileSkinVariant = string.Empty;
    }
}
