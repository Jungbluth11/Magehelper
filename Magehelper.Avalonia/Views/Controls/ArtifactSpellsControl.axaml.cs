using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Magehelper.Avalonia.Views.Controls;

public partial class ArtifactSpellsControl : UserControl
{
    public ArtifactSpellsControl(string artifactSpellName, Artifact artifact, Func<ArtifactSpell?> addSpellFunction, string artifactSpellCounterText = null)
    {
        DataContext = new ArtifactSpellsControlViewModel(artifactSpellName, artifact, addSpellFunction, artifactSpellCounterText);
        InitializeComponent();
    }
}