namespace Magehelper.ViewModels.Dialogs;

public partial class EnableSpellStorageDialogViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private int _pointsUsed;
    public int PointsTotal { get; set; }
    private readonly SpellStorage _spellStorage = Core.Core.Instance.SpellStorage!;
    public int PointsRemain => PointsTotal - PointsUsed;
    public ObservableCollection<SpellStorageRepresenter> SpellStorages => [];

    private bool CanSubmit()
    {
        return PointsRemain == 0;
    }

    private string GetText(int index, int points)
    {
        return $"Speicher {index + 1} : {points} AsP";
    }

    [RelayCommand]
    private void AddSpellStorage(int points)
    {
        string text = GetText(SpellStorages.Count, points);
        PointsUsed += points;
        SpellStorages.Add(new() { Text = text, Points = points });
    }

    [RelayCommand]
    private void RemoveSpellStorage(SpellStorageRepresenter spellStorage)
    {
        PointsUsed -= spellStorage.Points;
        SpellStorages.Remove(spellStorage);
        for (int i = 0; i < SpellStorages.Count; i++)
        {
            int points = SpellStorages[i].Points;
            SpellStorages[i] = new() { Text = GetText(i, points), Points = points };
        }
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private void Submit()
    {
        _spellStorage.EnableStorage([.. SpellStorages.Select(s => s.Points)]);
    }
}
