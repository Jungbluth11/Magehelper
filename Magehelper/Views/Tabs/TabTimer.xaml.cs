namespace Magehelper.Views.Tabs;

public sealed partial class TabTimer : TabViewItem
{
    public TabTimer()
    {
        InitializeComponent();
    }

    private async void BtnAddTimer_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            AddTimerDialog dialog = new()
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
