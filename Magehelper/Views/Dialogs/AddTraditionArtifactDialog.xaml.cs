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

    private void AddTraditionArtifactDialog_OnLoaded(object sender, RoutedEventArgs e)
    {
        AddTraditionArtifactDialogViewModel viewModel = (AddTraditionArtifactDialogViewModel)DataContext!;
        viewModel.CurrentArtifact = viewModel.Artifacts[0];
    }
}
