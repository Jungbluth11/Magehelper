namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class ArtifactSettingsControlViewModel : ObservableObject
    {
        public IEnumerable<ArtifactSpell> ArtifactSpells { get; set; }

        public ArtifactSettingsControlViewModel(ArtifactSpell[] artifactSpells)
        {
            ArtifactSpells = artifactSpells ?? throw new ArgumentNullException(nameof(artifactSpells));
        }

    }
}
