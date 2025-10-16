namespace Magehelper.ViewModels.Dialogs;

public partial class LoadFromToolDialogViewModel : ObservableObject
{
    [ObservableProperty] private Charakter? _selectedCharacter;
    public Charakter[] Characters { get; }



    public LoadFromToolDialogViewModel()
    {
        var test = HeldentoolInterop.GetByAE();
        Characters = test;
    }

    [RelayCommand]
    private void LoadCharacter()
    {
        if (SelectedCharacter != null)
        {
            WeakReferenceMessenger.Default.Send(SelectedCharacter);
        }
    }
}
