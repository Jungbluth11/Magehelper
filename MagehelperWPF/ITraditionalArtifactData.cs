using Magehelper.Core;

namespace Magehelper.WPF
{
    public interface ITraditionalArtifactData
    {
        public ArtifactSpellsControl ArtifactSpellsControl { get; }

        public abstract ArtifactSpell? AddSpell();
    }
}