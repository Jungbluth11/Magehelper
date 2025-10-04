namespace Magehelper.Views.Tabs;

public sealed partial class TabArtifact : TabViewItem
{
    public TabArtifact()
    {
        InitializeComponent();
    }

    private async void Button_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            AddTraditionArtifactDialog dialog = new()
            {
                XamlRoot = XamlRoot
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }
}
