namespace Magehelper.Views.Pages;

public sealed partial class PetGeneratorPage : Page
{
    public PetGeneratorPage()
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

    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        (DataContext as PetGeneratorPageViewModel)!.Submit();
        Frame.Navigate(typeof(MainPage), typeof(TabPet));
    }
}
