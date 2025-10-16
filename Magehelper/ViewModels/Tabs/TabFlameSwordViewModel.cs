namespace Magehelper.ViewModels.Tabs;

public partial class TabFlameSwordViewModel : ObservableObject, IRecipient<FileActionMessage>
{
    private readonly Core.Core _core = Core.Core.GetInstance();
    private readonly FlameSword _flameSword;

    [ObservableProperty] private bool _flameSwordExist;
    [ObservableProperty] private bool _isEnabled;
    [ObservableProperty] private bool _showNoFlameSwordText = true;
    [ObservableProperty] private int _attackRkp;
    [ObservableProperty] private int _attackRkpMax;
    [ObservableProperty] private int _attackValue;
    [ObservableProperty] private int _gsRkp;
    [ObservableProperty] private int _gsRkpMax;
    [ObservableProperty] private int _gsValue;
    [ObservableProperty] private int _parryRkp;
    [ObservableProperty] private int _parryRkpMax;
    [ObservableProperty] private int _parryValue;
    [ObservableProperty] private int _rkpValue;
    [ObservableProperty] private int _tpRkp;
    [ObservableProperty] private int _tpValue;
    [ObservableProperty] private string _buttonText = "Aktivieren";

    public TabFlameSwordViewModel()
    {
        _flameSword = new();
        TpValue = _flameSword.Tp[0];
        AttackValue = _flameSword.Attack[0];
        AttackRkpMax = _flameSword.AttackMaxPoints;
        ParryValue = _flameSword.Parry[0];
        ParryRkpMax = _flameSword.ParryMaxPoints;
        GsValue = _flameSword.Gs[0];
        GsRkpMax = _flameSword.GsMaxPoints;
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

                if (_core.HasFlameSword)
                {
                    FlameSwordExist = true;
                    ShowNoFlameSwordText = false;
                }

                break;
        }
    }

    public void Activate(int rkp)
    {
        _flameSword.PointsTotal = rkp;
        RkpValue = rkp;
        IsEnabled = true;
    }

    public void Deactivate()
    {
        RkpValue = 0;
        TpRkp = 0;
        TpValue = _flameSword.Tp[0];
        AttackRkp = 0;
        AttackRkpMax = _flameSword.AttackMaxPoints;
        AttackValue = _flameSword.Attack[0];
        ParryRkp = 0;
        ParryRkpMax = _flameSword.ParryMaxPoints;
        ParryValue = _flameSword.Parry[0];
        GsRkp = 0;
        GsRkpMax = _flameSword.GsMaxPoints;
        GsValue = _flameSword.Gs[0];
        ButtonText = "Aktivieren";
        IsEnabled = false;
        _flameSword.ResetTool();
    }

    private void ResetTab()
    {
        RkpValue = 0;
        TpRkp = 0;
        TpValue = _flameSword.Tp[0];
        AttackRkp = 0;
        AttackRkpMax = _flameSword.AttackMaxPoints;
        AttackValue = _flameSword.Attack[0];
        ParryRkp = 0;
        ParryRkpMax = _flameSword.ParryMaxPoints;
        ParryValue = _flameSword.Parry[0];
        GsRkp = 0;
        GsRkpMax = _flameSword.GsMaxPoints;
        GsValue = _flameSword.Gs[0];
        ButtonText = "Aktivieren";
        IsEnabled = false;
        FlameSwordExist = false;
        ShowNoFlameSwordText = true;
        _flameSword.ResetTool();
    }

    public (int, string) RollActivation()
    {
        (int item1, int[] item2, string item3) = _flameSword.RollActivation();

        return (item1, $"{item2[0]}/{item2[1]}/{item2[2]}\n {item3}");
    }

    partial void OnAttackRkpChanged(int value)
    {
        _flameSword.ModifyAttack(value);
        AttackValue = _flameSword.Attack[0];
        RkpValue = _flameSword.PointsRemain;
    }

    partial void OnGsRkpChanged(int value)
    {
        _flameSword.ModifyGs(value);
        GsValue = _flameSword.Gs[0];
        RkpValue = _flameSword.PointsRemain;
    }

    partial void OnParryRkpChanged(int value)
    {
        _flameSword.ModifyParry(value);
        ParryValue = _flameSword.Parry[0];
        RkpValue = _flameSword.PointsRemain;
    }

    partial void OnTpRkpChanged(int value)
    {
        _flameSword.ModifyTp(value);
        TpValue = _flameSword.Tp[0];
        RkpValue = _flameSword.PointsRemain;
    }
}