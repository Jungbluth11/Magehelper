using Avalonia.Controls;
using Avalonia.Layout;

namespace Magehelper.Avalonia.Views.Controls;

public partial class ArtifactControl : UserControl
{
    public ArtifactControl(string artifactName, UserControl userControl)
    {
        ArgumentNullException.ThrowIfNull(artifactName);
        ArgumentNullException.ThrowIfNull(userControl);

        DataContext = new ArtifactControlViewModel(artifactName);

        InitializeComponent();
        userControl.VerticalAlignment = VerticalAlignment.Top;
        userControl.HorizontalAlignment = HorizontalAlignment.Left;
        ArtifactSpellsControl artifactSpellsControl = ((IArtifactData)userControl).ArtifactSpellsControl;
        artifactSpellsControl.VerticalAlignment = VerticalAlignment.Top;
        artifactSpellsControl.HorizontalAlignment = HorizontalAlignment.Right;
        Grid.SetColumn(userControl, 0);
        Grid.SetRow(userControl, 1);
        Grid.SetColumn(artifactSpellsControl, 1);
        Grid.SetRow(artifactSpellsControl, 1);
        GridContent.Children.Add(userControl);
        GridContent.Children.Add(artifactSpellsControl);
    }
}