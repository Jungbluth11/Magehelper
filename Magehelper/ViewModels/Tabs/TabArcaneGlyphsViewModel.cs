namespace Magehelper.ViewModels.Tabs;

public partial class TabArcaneGlyphsViewModel : ObservableObject,
    IRecipient<FileActionMessage>,
    IRecipient<AddArcaneGlyphMessage>,
    IRecipient<DeleteArcaneGlyphMessage>
{
    private readonly ArcaneGlyphs _arcaneGlyphs;

    [ObservableProperty] private ArcaneGlyphControlViewModel _currentArcaneGlyph = new(new());
    public ObservableCollection<ArcaneGlyphListItemControlViewModel> ArcaneGlyphs { get; } = [];

    public TabArcaneGlyphsViewModel()
    {
        _arcaneGlyphs = Core.Core.Instance.ArcaneGlyphs ?? [];

        foreach (ArcaneGlyph arcaneGlyph in _arcaneGlyphs)
        {
            ArcaneGlyphs.Add(new(arcaneGlyph));
        }

        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    public void Receive(AddArcaneGlyphMessage message)
    {
        ArcaneGlyphs.Add(new(message.Value));
    }

    public void Receive(DeleteArcaneGlyphMessage message)
    {
        ArcaneGlyphs.Remove(message.Value);
    }

    public void Receive(FileActionMessage message)
    {
        switch (message.Value)
        {
            case FileAction.New:
                ResetTab();

                break;
            case FileAction.Loaded:
                ResetTab();

                foreach (ArcaneGlyph arcaneGlyph in _arcaneGlyphs)
                {
                    ArcaneGlyphs.Add(new(arcaneGlyph));
                }

                break;
        }
    }

    public void ResetTab()
    {
        ArcaneGlyphs.Clear();
    }


    public void ShowArcaneGlyph(string guid)
    {
        CurrentArcaneGlyph = new(_arcaneGlyphs[guid]);
    }

    [RelayCommand]
    private void ToNextDay()
    {
        foreach (ArcaneGlyph arcaneGlyph in _arcaneGlyphs)
        {
            int newDuration = _arcaneGlyphs.DecreaseDuration(arcaneGlyph.Guid);
            WeakReferenceMessenger.Default.Send(new ArcaneGlyphDurationChangedMessage(arcaneGlyph.Guid, newDuration));
        }
    }
}
