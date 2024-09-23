using Avalonia.Controls;
using Magehelper.Avalonia.Models;

namespace Magehelper.Avalonia.Views.Controls;

public partial class StaffControl : UserControl, IArtifactData
{
    public ArtifactSpellsControl ArtifactSpellsControl { get; }

    public StaffControl(Settings settings, Staff staff)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(staff);

        ArtifactSpellsControl = new ArtifactSpellsControl(settings, "Stabzauber", staff, "Verfügbare Volumenpunkte");
        DataContext = new StaffControlViewModel(staff, ArtifactSpellsControl.DataContext as ArtifactSpellsControlViewModel);
        InitializeComponent();

    }
}