namespace Magehelper.Core;

public class RingOfLife : TraditionArtifact, IMaxSpellArtifact
{
    public int MaxSpells => 6;
    public int SpellsRemain => MaxSpells - boundSpells.Count;
    /// <summary>
    /// Constructor
    /// </summary>
    public RingOfLife() : base("ringOfLife.json", "Ring des Lebens")
    {
        _core.RingOfLife = this;
        Readfile();
    }
}
