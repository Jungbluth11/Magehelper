using Magehelper.Core;

namespace Magehelper.Avalonia
{
    public interface IArtifactData
    {
        public ArtifactSpellsControl ArtifactSpellsControl { get; }

        public abstract ArtifactSpell? AddSpell();
    }
}