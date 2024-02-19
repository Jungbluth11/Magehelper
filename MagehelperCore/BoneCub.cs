namespace Magehelper.Core
{
    public class BoneCub : TraditionalArtifact
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="core">An instance of <see cref="Core"/>.</param>
        public BoneCub(Core core) : base(core, "boneCub.json", "Knochenkeule")
        {
            core.BoneCub = this;
        }
    }
}