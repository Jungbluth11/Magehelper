namespace Magehelper.Core
{
    public class ObsidianDagger : Artifact, IMaxSpellArtifact
    {
        public int MaxSpells => 7;
        public int SpellsRemain => HasApport ? 0 : MaxSpells - boundSpells.Count;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="core">An instance of <see cref="Core"/>.</param>
        public ObsidianDagger(Core core) : base(core, "obsidianDagger.json", "Vulkanglasdolch")
        {
            core.ObsidianDagger = this;
        }
    }
}