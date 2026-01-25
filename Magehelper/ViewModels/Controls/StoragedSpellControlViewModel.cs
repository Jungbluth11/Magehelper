namespace Magehelper.ViewModels.Controls;

public partial class StoragedSpellControlViewModel(StoragedSpell spell) : ObservableObject
{
    public string Guid { get; } = spell.Guid;
    public string Text { get; } = spell.DisplayText;

    [RelayCommand]
    private void RemoveSpell()
    {
        WeakReferenceMessenger.Default.Send(new RemoveStoragedSpellMessage(this));
    }
}
