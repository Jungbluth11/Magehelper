using System.Collections.ObjectModel;
using DSAUtils.HeldentoolInterop;

namespace Magehelper.Avalonia.ViewModels.Tabs
{
    public partial class TabContentCharacterViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _mu = 0;
        [ObservableProperty]
        private int _kl = 0;
        [ObservableProperty]
        private int _in = 0;
        [ObservableProperty]
        private int _ch = 0;
        [ObservableProperty]
        private int _ff = 0;
        [ObservableProperty]
        private int _ge = 0;
        [ObservableProperty]
        private int _ko = 0;
        [ObservableProperty]
        private int _kk = 0;
        [ObservableProperty]
        private int _mr = 0;
        [ObservableProperty]
        private int _aup = 0;
        [ObservableProperty]
        private int _lep = 0;
        [ObservableProperty]
        private int _asp = 0;
        [ObservableProperty]
        private bool _isCharacterLoaded = false;
        private static TabContentCharacterViewModel _instance = new();
        public static TabContentCharacterViewModel Instance => _instance;
        public Character Character { get; set; }
        public ObservableCollection<CharacterSpell> Spells { get; set; } = [];
        public ObservableCollection<CharacterRitual> Rituals { get; set; } = [];

        public TabContentCharacterViewModel()
        {
            Character = new Character(MainWindowViewModel.Instance.Core);
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

        public void LoadCharacter(Charakter character)
        {
            ResetTab();
            Character.LoadCharacter(character);
            Mu = Character.MU;
            Kl = Character.KL;
            In = Character.IN;
            Ch = Character.CH;
            Ff = Character.FF;
            Ge = Character.GE;
            Ko = Character.KO;
            Kk = Character.KK;
            Mr = Character.MR;
            Aup = Character.AuP;
            Lep = Character.LeP;
            Asp = Character.AsP;
            if (Character.Spells != null)
            {
                Spells.AddRange(Character.Spells);
            }
            if (Character.Rituals != null)
            {
                Rituals.AddRange(Character.Rituals);
            }
            IsCharacterLoaded = true;
        }

        partial void OnMuChanged(int value)
        {
            Character.MU = value;
        }

        partial void OnKlChanged(int value)
        {
            Character.KL = value;
        }

        partial void OnInChanged(int value)
        {
            Character.IN = value;
        }

        partial void OnChChanged(int value)
        {
            Character.CH = value;
        }

        partial void OnFfChanged(int value)
        {
            Character.FF = value;
        }

        partial void OnGeChanged(int value)
        {
            Character.GE = value;
        }

        partial void OnKoChanged(int value)
        {
            Character.KO = value;
        }

        partial void OnKkChanged(int value)
        {
            Character.KK = value;
        }

        partial void OnMrChanged(int value)
        {
            Character.MR = value;
        }

        partial void OnAupChanged(int value)
        {
            Character.AuP = value;
        }

        partial void OnLepChanged(int value)
        {
            Character.LeP = value;
        }

        partial void OnAspChanged(int value)
        {
            Character.AsP = value;
        }


        [RelayCommand]
        private void ResetAup()
        {
            Character.ResetAuP();
            Aup = Character.LeP;
        }

        [RelayCommand]
        private void ResetLep()
        {
            Character.ResetLeP();
            Lep = Character.LeP;
        }

        [RelayCommand]
        private void ResetAsp()
        {
            Character.ResetAsP();
            Asp = Character.AsP;
        }
    }
}
