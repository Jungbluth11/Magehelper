using Windows.Storage.Pickers;

namespace Magehelper.Views.Tabs;

public sealed partial class TabArtifact : TabViewItem
{
    public TabArtifact()
    {
        InitializeComponent();
    }

    private async void SplitButtonAddArtifact_OnClick(SplitButton sender, SplitButtonClickEventArgs args)
    {
        try
        {
            await AddArtifact(string.Empty);
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private async void SplitButtonLoadFromFile_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            FileOpenPicker fileOpenPicker = new()
            {
                FileTypeFilter = { ".artefakt" },
                CommitButtonText = "Auswählen"
            };

            StorageFile file = await fileOpenPicker.PickSingleFileAsync();
            
            if (file == null)
            {
                return;
            }

            await AddArtifact(file.Path);

        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private async Task AddArtifact(string filePath)
    {
        AddArtifactDialog dialog = new()
        {
            XamlRoot = XamlRoot,
            FilePath = filePath
        };

        await dialog.ShowAsync();
    }
}
