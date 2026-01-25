namespace Magehelper.Views.Tabs;

public sealed partial class TabArcaneGlyphs : TabViewItem
{
    public TabArcaneGlyphs()
    {
        InitializeComponent();
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        (DataContext as TabArcaneGlyphsViewModel)!.ShowArcaneGlyph(((ArcaneGlyphListItemControlViewModel)e.AddedItems[0]).Guid);
    }

    private void BtnAddArcaneGlyph_OnClick(object sender, RoutedEventArgs e)
    {
        (XamlRoot!.Content as Frame)!.Navigate(typeof(ArcaneGlyphGeneratorPage));

    }
}
