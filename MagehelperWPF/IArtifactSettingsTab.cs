using Magehelper.Core;

namespace Magehelper.WPF
{
    public interface IArtifactSettingsTab
    {
        public string SettingsHeader { get; }
        public ArtifactSpell[] ArtifactSpells { get; set; }
    }
}