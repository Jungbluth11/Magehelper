namespace Magehelper.ViewModels.Tabs;

public partial class TabSpellStorageViewModel : ObservableObject, IRecipient<FileActionMessage>, IRecipient<EnableTabMessage>
{
    private readonly Core.Core _core = Core.Core.Instance;
    [ObservableProperty]
    private bool _isSpellStorageEnabled;
    [ObservableProperty]
    private bool _showNoSpellStorageText = true;

    public int Points { get; private set; }
    public ObservableCollection<SpellStorageControlViewModel> SpellStorageList { get; } = [];
    private readonly SpellStorage _spellStorage;

    public TabSpellStorageViewModel()
    {
        _spellStorage = _core.SpellStorage ?? new();

        if (_core.HasSpellStorage)
        {
            EnableTab();
        }
        WeakReferenceMessenger.Default.RegisterAll(this);
    }


    public void ResetTab()
    {
        IsSpellStorageEnabled = false;
        ShowNoSpellStorageText = true;
        SpellStorageList.Clear();
    }

    public void EnableTab()
    {
        for (int i = 0; i < _spellStorage.StorageCount; i++)
        {
            

            SpellStorageList.Add(new(i, _spellStorage));
        }

        IsSpellStorageEnabled = true;
        ShowNoSpellStorageText = false;
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

                if (_core.HasSpellStorage)
                {
                    EnableTab();
                }

                break;
        }
    }

    public void Receive(EnableTabMessage message)
    {
        if (message.TabName == "Zauberspeicher")
        {
            Points = message.Points;
        }
    }
}
