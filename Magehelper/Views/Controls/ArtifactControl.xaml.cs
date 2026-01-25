namespace Magehelper.Views.Controls;

public sealed partial class ArtifactControl : UserControl
{
    public ArtifactControl()
    {
        InitializeComponent();
    }

    private void BtnDropDown_OnClick(object sender, RoutedEventArgs e) =>
        ArtifactDescription.Visibility = ArtifactDescription.Visibility == Visibility.Visible
            ? Visibility.Collapsed
            : Visibility.Visible;
}
