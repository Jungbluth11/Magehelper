namespace Magehelper.Views.Controls;

public sealed partial class AddStaffSpellControl : UserControl
{
    public AddStaffSpellControl()
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
