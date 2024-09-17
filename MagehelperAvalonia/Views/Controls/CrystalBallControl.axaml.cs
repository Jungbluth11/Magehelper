using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Controls;

public partial class CrystalBallControl : UserControl, IArtifactData
{
    public ArtifactSpellsControl ArtifactSpellsControl { get; }

    public CrystalBallControl(Settings settings, CrystalBall crystalBall)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(crystalBall);

        ArtifactSpellsControl = new ArtifactSpellsControl(settings, "Kugelzauber", crystalBall, "Verbleibende Kugelzauber");
        DataContext = new CrystalBallControlViewModel(crystalBall);
        InitializeComponent();
    }
}