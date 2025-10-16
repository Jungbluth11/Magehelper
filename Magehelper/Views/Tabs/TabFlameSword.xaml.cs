namespace Magehelper.Views.Tabs;

public sealed partial class TabFlameSword : TabViewItem
{
    private TabFlameSwordViewModel ViewModel => (TabFlameSwordViewModel) DataContext;

    public TabFlameSword()
    {
        InitializeComponent();
    }

    private void UIElement_OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            XamlRoot!.Content!.Focus(FocusState.Programmatic);
        }
    }

    private async void Button_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (ViewModel.ButtonText == "Aktivieren")
            {
                NumberDialog dialog = new()
                {
                    XamlRoot = XamlRoot,
                    Title = "RkP* des Flammenschwerts",
                    RollFunc = ViewModel.RollActivation,
                    SubmitAction = ViewModel.Activate
                };

                await dialog.ShowAsync();
            }
            else
            {
                ViewModel.Deactivate();
            }
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }
}
