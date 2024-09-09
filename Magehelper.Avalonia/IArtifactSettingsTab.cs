using Magehelper.Core;

namespace Magehelper.Avalonia
{
    public interface IArtifactSettingsTab
    {
        public string SettingsHeader { get; }
        public ArtifactSpell[] ArtifactSpells { get; set; }
    }
}