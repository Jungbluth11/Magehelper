using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Controls;

public partial class ArtifactSpellsControl : UserControl
{
    public ArtifactSpellsControl(Settings settings, string artifactSpellName, Artifact artifact, Func<ArtifactSpell?> addSpellFunction, string artifactSpellCounterText = "")
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(artifact);
        ArgumentNullException.ThrowIfNull(addSpellFunction);

        if (string.IsNullOrEmpty(artifactSpellName))
        {
            throw new ArgumentException($"\"{nameof(artifactSpellName)}\" kann nicht NULL oder leer sein.", nameof(artifactSpellName));
        }

        DataContext = new ArtifactSpellsControlViewModel(settings, artifactSpellName, artifact, addSpellFunction, artifactSpellCounterText);
        InitializeComponent();
    }
}