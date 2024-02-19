using Magehelper.Core;

namespace Magehelper.WPF
{
    public interface ITraditionalArtifactSettingsTab
    {
        public string SettingsHeader { get; }
        public ArtifactSpell[] ArtifactSpells { get; set; }
    }
}