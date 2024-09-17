using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Controls;

public partial class StaffControl : UserControl, IArtifactData
{
    public ArtifactSpellsControl ArtifactSpellsControl { get; }

    public StaffControl(Settings settings, Staff staff, MainWindowViewModel mainWindowViewModel)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(staff);
        ArgumentNullException.ThrowIfNull(mainWindowViewModel);

        ArtifactSpellsControl = new ArtifactSpellsControl(settings, "Stabzauber", staff, "Verfügbare Volumenpunkte");
        DataContext = new StaffControlViewModel(staff, mainWindowViewModel, ArtifactSpellsControl.DataContext as ArtifactSpellsControlViewModel);
        InitializeComponent();

    }
}