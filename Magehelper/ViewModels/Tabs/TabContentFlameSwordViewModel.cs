namespace Magehelper.ViewModels.Tabs;

public partial class TabContentFlameSwordViewModel : ObservableObject, IRecipient<FileActionMessage>
{
    private readonly Core.Core _core = Core.Core.GetInstance();
    [ObservableProperty]
    private int _rkpValue;
    [ObservableProperty]
    private int _tpRkp;
    [ObservableProperty]
    private int _tpValue;
    [ObservableProperty]
    private int _attackRkp;
    [ObservableProperty]
    private int _attackRkpMax;
    [ObservableProperty]
    private int _attackValue;
    [ObservableProperty]
    private int _parryRkp;
    [ObservableProperty]
    private int _parryRkpMax;
    [ObservableProperty]
    private int _parryValue;
    [ObservableProperty]
    private int _gsRkp;
    [ObservableProperty]
    private int _gsRkpMax;
    [ObservableProperty]
    private int _gsValue;
    [ObservableProperty]
    private string _buttonText = "Aktivieren";
    [ObservableProperty]
    private bool _flameSwordExist;
    [ObservableProperty]
    private bool _showNoFlameSwordText = true;
    [ObservableProperty]
    private bool _isEnabled;

    public FlameSword FlameSword { get; }

    public TabContentFlameSwordViewModel()
    {
        FlameSword = new();
        TpValue = FlameSword.Tp[0];
        AttackValue = FlameSword.Attack[0];
        AttackRkpMax = FlameSword.AttackMaxPoints;
        ParryValue = FlameSword.Parry[0];
        ParryRkpMax = FlameSword.ParryMaxPoints;
        GsValue = FlameSword.Gs[0];
        GsRkpMax = FlameSword.GsMaxPoints;
        WeakReferenceMessenger.Default.Register(this);
    }

    public void ResetTab()
    {
        RkpValue = 0;
        TpRkp = 0;
        TpValue = FlameSword.Tp[0];
        AttackRkp = 0;
        AttackRkpMax = FlameSword.AttackMaxPoints;
        AttackValue = FlameSword.Attack[0];
        ParryRkp = 0;
        ParryRkpMax = FlameSword.ParryMaxPoints;
        ParryValue = FlameSword.Parry[0];
        GsRkp = 0;
        GsRkpMax = FlameSword.GsMaxPoints;
        GsValue = FlameSword.Gs[0];
        ButtonText = "Aktivieren";
        IsEnabled = false;
        FlameSwordExist = false;
        ShowNoFlameSwordText = true;
        FlameSword.ResetTool();
    }

    public void Activate(int rkp)
    {
        FlameSword.PointsTotal = rkp;
        RkpValue = rkp;
        IsEnabled = true;
    }

    public void Deactivate()
    {
        RkpValue = 0;
        TpRkp = 0;
        TpValue = FlameSword.Tp[0];
        AttackRkp = 0;
        AttackRkpMax = FlameSword.AttackMaxPoints;
        AttackValue = FlameSword.Attack[0];
        ParryRkp = 0;
        ParryRkpMax = FlameSword.ParryMaxPoints;
        ParryValue = FlameSword.Parry[0];
        GsRkp = 0;
        GsRkpMax = FlameSword.GsMaxPoints;
        GsValue = FlameSword.Gs[0];
        ButtonText = "Aktivieren";
        IsEnabled = false;
        FlameSword.ResetTool();
    }

    partial void OnTpRkpChanged(int value)
    {
        FlameSword.ModifyTp(value);
        TpValue = FlameSword.Tp[0];
        RkpValue = FlameSword.PointsRemain;
    }

    partial void OnAttackRkpChanged(int value)
    {
        FlameSword.ModifyAttack(value);
        AttackValue = FlameSword.Attack[0];
        RkpValue = FlameSword.PointsRemain;
    }

    partial void OnParryRkpChanged(int value)
    {
        FlameSword.ModifyParry(value);
        ParryValue = FlameSword.Parry[0];
        RkpValue = FlameSword.PointsRemain;
    }

    partial void OnGsRkpChanged(int value)
    {
        FlameSword.ModifyGs(value);
        GsValue = FlameSword.Gs[0];
        RkpValue = FlameSword.PointsRemain;
    }

    public void Receive(FileActionMessage message)
    {
        switch (message.Value)
        {
            case FileAction.New:
                ResetTab();

                break;
            case FileAction.Loaded:
                {
                    ResetTab();

                    if (_core.HasFlameSword)
                    {
                        FlameSwordExist = true;
                        ShowNoFlameSwordText = false;
                    }

                    break;
                }
        }
    }
}
