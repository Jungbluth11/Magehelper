namespace Magehelper.ViewModels.Dialogs;

public partial class LoadFromToolDialogViewModel : ObservableObject
{
    [ObservableProperty] private Charakter? _selectedCharacter;
    public Charakter[] Characters { get; } = HeldentoolInterop.GetByAE();

    [RelayCommand]
    private void LoadCharacter()
    {
        if (SelectedCharacter != null)
        {
            WeakReferenceMessenger.Default.Send<Charakter>(SelectedCharacter);
        }
    }
}
