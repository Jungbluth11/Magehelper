using System.ComponentModel;

namespace Magehelper.Views.Tabs;

public sealed partial class TabSpellStorage : TabViewItem
{
    private TabSpellStorageViewModel ViewModel => (TabSpellStorageViewModel)DataContext;
    public TabSpellStorage()
    {
        InitializeComponent();
        ViewModel.PropertyChanging += TabSpellStorage_PropertyChanging;
    }

    private async void TabSpellStorage_PropertyChanging(object? sender, PropertyChangingEventArgs e)
    {
        try
        {
            if (e.PropertyName != "IsSpellStorageEnabled")
            {
                return;
            }

            EnableSpellStorageDialog dialog = new();
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
}
