using Avalonia.Controls;
using Magehelper.Avalonia.Models;

namespace Magehelper.Avalonia.Views.Controls;

public partial class ArtifactSettingsControl : UserControl, IArtifactSettingsTab
{
    public string SettingsHeader { get; }

    public ArtifactSettingsControl(ArtifactSpell[] artifactSpells)
    {
        ArgumentNullException.ThrowIfNull(artifactSpells);

        DataContext = new ArtifactSettingsControlViewModel(artifactSpells);
        SettingsHeader = artifactSpells[0].Type;
        InitializeComponent();
    }
}