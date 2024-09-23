using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Controls;

public partial class BowlControl : UserControl, IArtifactData
{
    public ArtifactSpellsControl ArtifactSpellsControl { get; }

    public BowlControl(Settings settings, Bowl bowl)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(bowl);

        ArtifactSpellsControl = new ArtifactSpellsControl(settings, "Schalenzauber", bowl);
        DataContext = new BowlControlViewModel(bowl, ArtifactSpellsControl.DataContext as ArtifactSpellsControlViewModel);
        InitializeComponent();
        DropdownTemperatureCategoryStart.ItemsSource = Bowl.TemperatureCategoryStrings;
        DropdownTemperatureCategoryTarget.ItemsSource = Bowl.TemperatureCategoryStrings;
    }
}