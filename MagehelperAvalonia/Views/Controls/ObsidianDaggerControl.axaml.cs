using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Controls;

public partial class ObsidianDaggerControl : UserControl, IArtifactData
{
    public ArtifactSpellsControl ArtifactSpellsControl { get; }
    public ObsidianDaggerControl(Settings settings, ObsidianDagger obsidianDagger)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(obsidianDagger);

        ArtifactSpellsControl = new ArtifactSpellsControl(settings, "Dolchzauber", obsidianDagger, "Verbleibende Zauber:");
        DataContext = new ObsidianDaggerControlViewModel(obsidianDagger);
        InitializeComponent();
    }

}