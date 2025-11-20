namespace Magehelper.Views.Dialogs;

public sealed partial class AddArtifactDialog : ContentDialog
{
    public string? FilePath { get; init; }

    public AddArtifactDialog()
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

    private void AddArtifactDialog_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(FilePath))
        {
            return;
        }

        (DataContext as AddArtifactDialogViewModel)!.FilePath = FilePath!;
        Title = "Artefakt von Datei hinzufügen";
    }
}
