using Magehelper.Core;

namespace Magehelper.WPF
{
    public interface IArtifactData
    {
        public ArtifactSpellsControl ArtifactSpellsControl { get; }

        public abstract ArtifactSpell? AddSpell();
    }
}