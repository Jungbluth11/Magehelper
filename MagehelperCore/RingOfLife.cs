namespace Magehelper.Core
{
    public class RingOfLife : Artifact, IMaxSpellArtifact
    {
        public int MaxSpells => 6;
        public int SpellsRemain => MaxSpells - boundSpells.Count;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="core">An instance of <see cref="Core"/>.</param>
        public RingOfLife(Core core) : base(core, "ringOfLife.json", "Ring des Lebens")
        {
            core.RingOfLife = this;
        }
    }
}