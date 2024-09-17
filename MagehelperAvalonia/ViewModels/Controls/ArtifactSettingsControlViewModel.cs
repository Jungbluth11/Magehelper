namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class ArtifactSettingsControlViewModel(ArtifactSpell[] artifactSpells) : ObservableObject
    {
        public IEnumerable<ArtifactSpell> ArtifactSpells { get; set; } = artifactSpells;
    }
}
