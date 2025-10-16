namespace Magehelper.Views.Tabs;

public sealed partial class TabMod : TabViewItem
{
    public TabMod()
    {
        InitializeComponent();
    }

    private async void CheckBox_Checked(object sender, RoutedEventArgs e)
    {
        try
        {
            NumberDialog dialog = new()
            {
                XamlRoot = XamlRoot,
                Title = "höchste beteiligte Magieresistenz",
                SubmitAction = (DataContext as TabModViewModel)!.AddMr
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }
}
