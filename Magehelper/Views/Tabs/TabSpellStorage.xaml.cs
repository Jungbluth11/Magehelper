namespace Magehelper.Views.Tabs;

public sealed partial class TabSpellStorage : TabViewItem
{
    private TabSpellStorageViewModel ViewModel => (TabSpellStorageViewModel) DataContext;
    public TabSpellStorage()
    {
        InitializeComponent();
        ViewModel.PropertyChanging += TabSpellStorage_PropertyChanging;
    }

    private async void TabSpellStorage_PropertyChanging(object? sender, PropertyChangingEventArgs e)
    {
        try
        {
            if (e.PropertyName != "IsSpellStorageEnabled" || ViewModel.SpellStorageList.Any())
            {
                return;
            }

            EnableSpellStorageDialog dialog = new()
            {
                XamlRoot = XamlRoot
            };

            dialog.SetPoints(ViewModel.Points);
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                (DataContext as TabSpellStorageViewModel)!.EnableTab();
            }
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            AddStoragedSpellDialog dialog = new()
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
