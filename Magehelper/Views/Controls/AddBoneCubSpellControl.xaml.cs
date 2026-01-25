namespace Magehelper.Views.Controls;

public sealed partial class AddBoneCubSpellControl : UserControl
{
    public AddBoneCubSpellControl()
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
