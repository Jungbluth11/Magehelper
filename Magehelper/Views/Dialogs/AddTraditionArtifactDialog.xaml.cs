namespace Magehelper.Views.Dialogs;

public sealed partial class AddTraditionArtifactDialog : ContentDialog
{
    public AddTraditionArtifactDialog()
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
}
