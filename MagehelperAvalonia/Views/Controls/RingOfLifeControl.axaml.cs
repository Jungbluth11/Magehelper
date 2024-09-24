using Avalonia.Controls;
using Magehelper.Avalonia.Models;

namespace Magehelper.Avalonia.Views.Controls;

public partial class RingOfLifeControl : UserControl, IArtifactData
{
    public ArtifactSpellsControl ArtifactSpellsControl { get; }

    public RingOfLifeControl(Settings settings, RingOfLife ringOfLife)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(ringOfLife);

        ArtifactSpellsControl = new ArtifactSpellsControl(settings, "Schlangenringzauber", ringOfLife, "Verbleibende Zauber:");
        DataContext = new RingOfLifeControlViewModel(ringOfLife, ArtifactSpellsControl.DataContext as ArtifactSpellsControlViewModel);
        InitializeComponent();
    }
}