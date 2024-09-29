namespace Magehelper.Avalonia.ViewModels.Tabs
{
    public partial class TabContentFlameSwordViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _rkpValue = 0;
        [ObservableProperty]
        private int _tpRkp = 0;
        [ObservableProperty]
        private int _tpValue;
        [ObservableProperty]
        private int _attackRkp = 0;
        [ObservableProperty]
        private int _attackRkpMax;
        [ObservableProperty]
        private int _attackValue;
        [ObservableProperty]
        private int _parryRkp = 0;
        [ObservableProperty]
        private int _parryRkpMax;
        [ObservableProperty]
        private int _parryValue;
        [ObservableProperty]
        private int _gsRkp = 0;
        [ObservableProperty]
        private int _gsRkpMax;
        [ObservableProperty]
        private int _gsValue;
        [ObservableProperty]
        private string _buttonText = "Aktivieren";
        [ObservableProperty]
        private bool _flameSwordExist = false;
        [ObservableProperty]
        private bool _isEnabled = false;
        private static TabContentFlameSwordViewModel _instance = new();
        public static TabContentFlameSwordViewModel Instance => _instance;
        public FlameSword FlameSword { get; }

        public TabContentFlameSwordViewModel()
        {
            FlameSword = new FlameSword(MainWindowViewModel.Instance.Core);
            TpValue = FlameSword.TP[0];
            AttackValue = FlameSword.Attack[0];
            AttackRkpMax = FlameSword.AttackMaxPoints;
            ParryValue = FlameSword.Parry[0];
            ParryRkpMax = FlameSword.ParryMaxPoints;
            GsValue = FlameSword.GS[0];
            GsRkpMax = FlameSword.GSMaxPoints;
        }

        public void ResetTab()
        {
            RkpValue = 0;
            TpRkp = 0;
            TpValue = FlameSword.TP[0];
            AttackRkp = 0;
            AttackRkpMax = FlameSword.AttackMaxPoints;
            AttackValue = FlameSword.Attack[0];
            ParryRkp = 0;
            ParryRkpMax = FlameSword.ParryMaxPoints;
            ParryValue = FlameSword.Parry[0];
            GsRkp = 0;
            GsRkpMax = FlameSword.GSMaxPoints;
            GsValue = FlameSword.GS[0];
            ButtonText = "Aktivieren";
            IsEnabled = false;
            FlameSword.ResetTool();
        }

        public void Enable(int rkp)
        {
            FlameSword.PointsTotal = rkp;
            RkpValue = rkp;
            IsEnabled = true;
        }

        partial void OnTpRkpChanged(int value)
        {
            FlameSword.ModifyTp(value);
            TpValue = FlameSword.TP[0];
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
            GsValue = FlameSword.GS[0];
            RkpValue = FlameSword.PointsRemain;
        }
    }
}
