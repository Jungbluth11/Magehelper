using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Layout;

namespace Magehelper.Avalonia.Views.Controls;

public partial class ArtifactControl : UserControl
{
#if DEBUG
    public ArtifactControl()
    {
        InitializeComponent();
        // used for view in designer
    }
#endif

    public ArtifactControl(string artifactName, UserControl userControl)
    {
        if (userControl is null)
        {
            throw new ArgumentNullException(nameof(userControl));
        }

        InitializeComponent();
        StringArtifactName.Content = artifactName;
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