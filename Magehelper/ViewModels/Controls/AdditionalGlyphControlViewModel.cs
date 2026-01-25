namespace Magehelper.ViewModels.Controls;

public partial class AdditionalGlyphControlViewModel : ObservableObject
{
    /// <inheritdoc/>
    public AdditionalGlyphControlViewModel(string text)
    {
        Text = text;
    }

    public string Text { get; }

    [RelayCommand]
    private void Delete()
    {
        WeakReferenceMessenger.Default.Send(new DeleteAdditionalGlyphMessage(this));
    }
}
