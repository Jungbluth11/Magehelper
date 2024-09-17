using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Controls;

public partial class ArtifactSpellsControl : UserControl
{
    public ArtifactSpellsControl(Settings settings, string artifactSpellName, Artifact artifact, string artifactSpellCounterText = "")
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(artifact);

        if (string.IsNullOrEmpty(artifactSpellName))
        {
            throw new ArgumentException($"\"{nameof(artifactSpellName)}\" kann nicht NULL oder leer sein.", nameof(artifactSpellName));
        }

        DataContext = new ArtifactSpellsControlViewModel(settings, artifactSpellName, artifact, artifactSpellCounterText);
        InitializeComponent();
    }
}