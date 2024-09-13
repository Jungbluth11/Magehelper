using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Magehelper.Core;

namespace Magehelper.Avalonia.Views.Controls;

public partial class ArtifactSettingsControl : UserControl, IArtifactSettingsTab
{
    public string SettingsHeader { get; }

    public ArtifactSettingsControl(ArtifactSpell[] artifactSpells)
    {
        if (artifactSpells is null)
        {
            throw new ArgumentNullException(nameof(artifactSpells));
        }

        DataContext = new ArtifactSettingsControlViewModel(artifactSpells);
        SettingsHeader = artifactSpells[0].Type;
        InitializeComponent();
    }
}