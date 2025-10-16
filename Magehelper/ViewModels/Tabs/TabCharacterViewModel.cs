namespace Magehelper.ViewModels.Tabs;

public partial class TabCharacterViewModel : ObservableObject, IRecipient<FileActionMessage>, IRecipient<CharacterSelectedMessage>
{
    private readonly Character _character;

    [ObservableProperty] private bool _isCharacterLoaded;
    [ObservableProperty] private int _asp;
    [ObservableProperty] private int _aup;
    [ObservableProperty] private int _ch;
    [ObservableProperty] private int _ff;
    [ObservableProperty] private int _ge;
    [ObservableProperty] private int _in;
    [ObservableProperty] private int _kk;
    [ObservableProperty] private int _kl;
    [ObservableProperty] private int _ko;
    [ObservableProperty] private int _lep;
    [ObservableProperty] private int _mr;
    [ObservableProperty] private int _mu;

    public ObservableCollection<CharacterRitual> Rituals { get; set; } = [];

    public ObservableCollection<CharacterSpell> Spells { get; set; } = [];

    public TabCharacterViewModel()
    {
        _character = Core.Core.GetInstance().Character ?? new();
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    public (int pointsLeft, int[] rollData, string textResult) RollSpell(CharacterSpell spell)
    {
        return _character.RollSpell(spell);
    }

    public (int pointsLeft, int[] rollData, string textResult) RollRitual(CharacterRitual spell)
    {
        return _character.RollRitual(spell);
    }

    public void Receive(CharacterSelectedMessage message)
    {
        ResetTab();
        _character.LoadCharacter(message.Value);
        Mu = _character.Mu;
        Kl = _character.Kl;
        In = _character.In;
        Ch = _character.Ch;
        Ff = _character.Ff;
        Ge = _character.Ge;
        Ko = _character.Ko;
        Kk = _character.Kk;
        Mr = _character.Mr;

        if (_character.Spells != null)
        {
            Spells.AddRange(_character.Spells);
        }

        if (_character.Rituals != null)
        {
            Rituals.AddRange(_character.Rituals);
        }

        IsCharacterLoaded = true;
        WeakReferenceMessenger.Default.Send(new CharacterLoadedMessage(_character.Data!));
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
                Aup = _character.AuP;
                Lep = _character.LeP;
                Asp = _character.AsP;

                break;
        }
    }

    public void ResetTab()
    {
        Mu = 0;
        Kl = 0;
        In = 0;
        Ch = 0;
        Ff = 0;
        Ge = 0;
        Ko = 0;
        Kk = 0;
        Mr = 0;
        Aup = 0;
        Lep = 0;
        Asp = 0;
        IsCharacterLoaded = false;
        Spells.Clear();
        Rituals.Clear();
    }

    partial void OnAspChanged(int value)
    {
        _character.AsP = value;
    }

    partial void OnAupChanged(int value)
    {
        _character.AuP = value;
    }

    partial void OnChChanged(int value)
    {
        _character.Ch = value;
    }

    partial void OnFfChanged(int value)
    {
        _character.Ff = value;
    }

    partial void OnGeChanged(int value)
    {
        _character.Ge = value;
    }

    partial void OnInChanged(int value)
    {
        _character.In = value;
    }

    partial void OnKkChanged(int value)
    {
        _character.Kk = value;
    }

    partial void OnKlChanged(int value)
    {
        _character.Kl = value;
    }

    partial void OnKoChanged(int value)
    {
        _character.Ko = value;
    }

    partial void OnLepChanged(int value)
    {
        _character.LeP = value;
    }

    partial void OnMrChanged(int value)
    {
        _character.Mr = value;
    }

    partial void OnMuChanged(int value)
    {
        _character.Mu = value;
    }

    [RelayCommand]
    private void ResetAsp()
    {
        _character.ResetAsP();
        Asp = _character.AsP;
    }


    [RelayCommand]
    private void ResetAup()
    {
        _character.ResetAuP();
        Aup = _character.LeP;
    }

    [RelayCommand]
    private void ResetLep()
    {
        _character.ResetLeP();
        Lep = _character.LeP;
    }
}