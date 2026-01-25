using System.Globalization;

namespace Magehelper.ViewModels.Tabs;

public partial class TabPetViewModel : ObservableObject, IRecipient<FileActionMessage>
{
    private readonly Pet _pet;

    [ObservableProperty] private bool _isPetLoaded;
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
    [ObservableProperty] private int _rkw;
    [ObservableProperty] private string _attack = string.Empty;
    [ObservableProperty] private string _buttonText = "Vertrauten binden";
    [ObservableProperty] private string _gs = string.Empty;
    [ObservableProperty] private string _parry = string.Empty;

    public ObservableCollection<PetSpell> Spells { get; set; } = [];

    public TabPetViewModel()
    {
        _pet = Core.Core.GetInstance().Pet ?? new();

        if (_pet.Species != null)
        {
            AddPet();
        }

        WeakReferenceMessenger.Default.Register(this);
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
                AddPet();

                break;

            case FileAction.Changed when !IsPetLoaded:
                AddPet();
                break;
        }
    }

    public void IncreaseAttack()
    {
        string[] values = Attack.Split("/");
        Attack = (int.Parse(values[0]) + 1).ToString();

        if (_pet.IsFlying)
        {
            Attack += "/" + (int.Parse(values[1]) + 1);
        }
    }

    public bool IncreaseAttribute(string attribute)
    {
        bool canIncrease = true;

        try
        {
            _pet.IncreaseAttribute(attribute);
        }
        catch (Exception ex)
        {
            if (ex.Message != "Maximum reached")
            {
                throw;
            }

            canIncrease = false;
        }

        return canIncrease;
    }

    public void IncreaseGs()
    {
        string[] values = Gs.Split("/");
        Gs = (int.Parse(values[0]) + 1).ToString();

        if (_pet.IsFlying)
        {
            Gs += "/" + (int.Parse(values[1]) + 1);
        }
    }

    public void IncreaseParry()
    {
        string[] values = Parry.Split("/");
        Parry = (int.Parse(values[0]) + 1).ToString();

        if (_pet.IsFlying)
        {
            Parry += "/" + (int.Parse(values[1]) + 1);
        }
    }

    public (int pointsLeft, int[] rollData, string textResult) RollSpell(PetSpell spell)
    {
        return _pet.RollSpell(spell);
    }

    private void AddPet()
    {
        string attack = _pet.Attack.ToString();
        string parry = _pet.Parry.ToString();
        string gs = _pet.Gs.ToString(CultureInfo.InvariantCulture);

        if (_pet.IsFlying)
        {
            attack += "/" + _pet.AttackFlying;
            parry += "/" + _pet.ParryFlying;
            gs += "/" + _pet.GsFlying;
        }

        Mu = _pet.Mu;
        Kl = _pet.Kl;
        In = _pet.In;
        Ch = _pet.Ch;
        Ff = _pet.Ff;
        Ge = _pet.Ge;
        Ko = _pet.Ko;
        Kk = _pet.Kk;
        Mr = _pet.Mr;
        Aup = _pet.AuP;
        Lep = _pet.LeP;
        Asp = _pet.AsP;
        Rkw = _pet.Rkw;
        Attack = attack;
        Parry = parry;
        Gs = gs;
        Spells.AddRange(_pet.KnownSpells);
        ButtonText = "Vertrautenzauber hinzufügen";
        IsPetLoaded = true;
    }

    partial void OnAspChanged(int value)
    {
        _pet.AsP = value;
    }

    partial void OnAupChanged(int value)
    {
        _pet.AuP = value;
    }

    partial void OnChChanged(int value)
    {
        _pet.Ch = value;
    }

    partial void OnFfChanged(int value)
    {
        _pet.Ff = value;
    }

    partial void OnGeChanged(int value)
    {
        _pet.Ge = value;
    }

    partial void OnInChanged(int value)
    {
        _pet.In = value;
    }

    partial void OnKkChanged(int value)
    {
        _pet.Kk = value;
    }

    partial void OnKlChanged(int value)
    {
        _pet.Kl = value;
    }

    partial void OnKoChanged(int value)
    {
        _pet.Ko = value;
    }

    partial void OnLepChanged(int value)
    {
        _pet.LeP = value;
    }

    partial void OnMrChanged(int value)
    {
        _pet.Mr = value;
    }

    partial void OnMuChanged(int value)
    {
        _pet.Mu = value;
    }

    private void ResetTab()
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
        Rkw = 0;
        Attack = string.Empty;
        Parry = string.Empty;
        Gs = string.Empty;
        Spells.Clear();
        ButtonText = "Vertrauten binden";
        IsPetLoaded = false;
    }

    [RelayCommand]
    private void ResetAsp()
    {
        _pet.ResetAsP();
        Asp = _pet.AsP;
    }

    [RelayCommand]
    private void ResetAup()
    {
        _pet.ResetAuP();
        Aup = _pet.LeP;
    }

    [RelayCommand]
    private void ResetLep()
    {
        _pet.ResetLeP();
        Lep = _pet.LeP;
    }
}
