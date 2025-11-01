namespace Magehelper.ViewModels.Dialogs;

public sealed partial class SelectAttributeDialog : ContentDialog
{
    public string[]? AttributeList { get; set; }
    public string? SelectedAttribute { get; private set; }

    public SelectAttributeDialog()
    {
        InitializeComponent();
        ComboBoxAttributes.ItemsSource = AttributeList ?? DSA.attribute_abk;
        ComboBoxAttributes.SelectedIndex = 0;
    }

    private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        SelectedAttribute = ComboBoxAttributes.SelectedValue.ToString();
    }
}
