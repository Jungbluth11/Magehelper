using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Controls;

public partial class ArtifactSpellsControl : UserControl
{
    public ArtifactSpellsControl(Settings settings, string artifactSpellName, Artifact artifact, string artifactSpellCounterText = "")
    {
        DataContext = new ArtifactSpellsControlViewModel(settings, artifactSpellName, artifact, artifactSpellCounterText);
        InitializeComponent();
    }
}