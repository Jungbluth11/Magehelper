using Avalonia.Controls;
using Magehelper.Avalonia.Models;

namespace Magehelper.Avalonia.Views.Controls;

public partial class StaffSettingsControl : UserControl, IArtifactSettingsTab
{
    public string SettingsHeader { get; }

    public StaffSettingsControl(ArtifactSpell[] artifactSpells)
    {
        ArgumentNullException.ThrowIfNull(artifactSpells);

        DataContext = new StaffSettingsControlViewModel(artifactSpells);
        SettingsHeader = artifactSpells[0].Type;
        InitializeComponent();
    }
}