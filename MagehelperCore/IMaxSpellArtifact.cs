namespace Magehelper.Core
{
    public interface IMaxSpellArtifact
    {
        /// <summary>
        /// Amount of spells that can be put on this artifact in total.
        /// </summary>
        public int MaxSpells { get; }
        /// <summary>
        /// Amount of spells left to put on this artifact.
        /// </summary>
        public int SpellsRemain { get; }
    }
}