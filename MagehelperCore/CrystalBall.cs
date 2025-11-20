namespace Magehelper.Core;

public class CrystalBall : TraditionArtifact, IMaxSpellArtifact
{
    private CrystalBallMaterial _material = CrystalBallMaterial.Glass;
    public int MaxSpells { get; private set; } = 4;
    public int SpellsRemain => HasApport ? 0 : MaxSpells - boundSpells.Count;
    /// <summary>
    /// the material the crystal ball is made of.
    /// </summary>
    public CrystalBallMaterial Material
    {
        get => _material;
        set
        {
            _material = value;
            _core.FileChanged = true;

            MaxSpells = value switch
            {
                CrystalBallMaterial.ArtificialCrystal => 6,
                CrystalBallMaterial.NaturalCrystal => 7,
                _ => 4
            };
        }
    }
    /// <summary>
    /// Names of the materials.
    /// </summary>
    public static string[] MaterialStrings =>
    [
        "Glas",
        "Künstlicher Kristall",
        "Natürlicher Kristall"
    ];
    /// <summary>
    /// Constructor
    /// </summary>
    public CrystalBall() : base("crystalBall.json", "Kristallkugel")
    {
        _core.CrystalBall = this;
    }

    /// <summary>
    /// Adds an spells to this artifact.
    /// </summary>
    /// <param name="spellName">Name of the spell.</param>
    /// <param name="variant">The variant of the spell (if available).</param>
    /// <param name="guid">GUID of the spell. (Only used by <see cref="Core.ReadFileVersionSelector"/>)</param>
    /// <returns>An instance of <see cref="ArtifactSpell"/> that contains data about the chosen spell.</returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="InvalidOperationException"/>
    public ArtifactSpell AddSpell(string spellName, string? variant = null, string? guid = null)
    {
        try
        {
            ArtifactSpell spell = spellsAvailable!.Single(a => a.Name == spellName);
            if (variant != null)
            {
                spell.DisplayText = spellName + "( Variante: " + variant + ")";
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

    /// <summary>
    /// Resets the instance of this class. (only used by <see cref="Core.ResetTool"/>.)
    /// </summary>
    internal new void ResetTool()
    {
        Material = CrystalBallMaterial.Glass;
        base.ResetTool();
    }
}
