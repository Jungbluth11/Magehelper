namespace Magehelper.Views.Dialogs;
public sealed partial class EnableSpellStorageDialog : ContentDialog
{
    public EnableSpellStorageDialog()
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

    public void SetPoints(int points)
    {
        (DataContext as EnableSpellStorageDialogViewModel)!.PointsTotal = points;
    }
}
