using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Controls;

public partial class BoneCubControl : UserControl, IArtifactData
{
    public ArtifactSpellsControl ArtifactSpellsControl { get; }

    public BoneCubControl(Settings settings, BoneCub boneCub)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(boneCub);

        ArtifactSpellsControl = new ArtifactSpellsControl(settings, "Keulenzauber", boneCub, "Verbleibende Zauber:");
        DataContext = new BoneCubControlViewModel(boneCub);
        InitializeComponent();
    }
}