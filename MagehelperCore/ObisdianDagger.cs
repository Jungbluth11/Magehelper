namespace Magehelper.Core;

public class ObsidianDagger : TraditionArtifact, IMaxSpellArtifact
{
    public int MaxSpells => 7;
    public int SpellsRemain => HasApport ? 0 : MaxSpells - boundSpells.Count;

    /// <summary>
    /// Constructor
    /// </summary>
    public ObsidianDagger() : base("obsidianDagger.json", "Vulkanglasdolch")
    {
        _core.ObsidianDagger = this;
        Readfile();
    }
}
