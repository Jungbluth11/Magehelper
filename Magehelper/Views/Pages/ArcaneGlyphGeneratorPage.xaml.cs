namespace Magehelper.Views.Pages;

public sealed partial class ArcaneGlyphGeneratorPage : Page
{
    public ArcaneGlyphGeneratorPage()
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

    private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
    {
        (DataContext as ArcaneGlyphGeneratorPageViewModel)!.AddGlyph();
        Frame.Navigate(typeof(MainPage), typeof(TabArcaneGlyphs));
    }

    private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(MainPage), typeof(TabArcaneGlyphs));
    }
}
