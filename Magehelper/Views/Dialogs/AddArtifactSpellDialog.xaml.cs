namespace Magehelper.Views.Dialogs;

public sealed partial class AddArtifactSpellDialog : ContentDialog
{
    public AddArtifactSpellDialog()
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
