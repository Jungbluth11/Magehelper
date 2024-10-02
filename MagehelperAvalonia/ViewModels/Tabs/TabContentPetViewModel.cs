﻿using System.Collections.ObjectModel;

namespace Magehelper.Avalonia.ViewModels.Tabs
{
    public partial class TabContentPetViewModel : ObservableObject
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
        private int _rkw = 0;
        [ObservableProperty]
        private string _attack = string.Empty;
        [ObservableProperty]
        private string _parry = string.Empty;
        [ObservableProperty]
        private string _gs = string.Empty;
        [ObservableProperty]
        private string _buttonText = "Vertrauten binden";
        [ObservableProperty]
        private bool _isPetLoaded = false;
        private static TabContentPetViewModel _instance = new();
        public static TabContentPetViewModel Instance => _instance;
        public Pet Pet { get; }
        public ObservableCollection<PetSpell> Spells { get; set; } = [];

        public TabContentPetViewModel()
        {
            Pet = new Pet(MainWindowViewModel.Instance.Core);
            MainWindowViewModel.Instance.Core.AddPetGUIAction = AddPet;
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
            Rkw = 0;
            Attack = string.Empty;
            Parry = string.Empty;
            Gs = string.Empty;
            Spells.Clear();
            ButtonText = "Vertrauten binden";
            IsPetLoaded = false;
        }

        public void AddPet()
        {
            string attack = Pet.Attack.ToString();
            string parry = Pet.Parry.ToString();
            string gs = Pet.GS.ToString();
            if (Pet.IsFlying)
            {
                attack += "/" + Pet.AttackFlying.ToString();
                parry += "/" + Pet.ParryFlying.ToString();
                gs += "/" + Pet.GSFlying.ToString();
            }
            Mu = Pet.MU;
            Kl = Pet.KL;
            In = Pet.IN;
            Ch = Pet.CH;
            Ff = Pet.FF;
            Ge = Pet.GE;
            Ko = Pet.KO;
            Kk = Pet.KK;
            Mr = Pet.MR;
            Aup = Pet.AuP;
            Lep = Pet.LeP;
            Asp = Pet.AsP;
            Rkw = Pet.RKW;
            Attack = attack;
            Parry = parry;
            Gs = gs;
            Spells.AddRange(Pet.KnownSpells);
            ButtonText = "Vertrautenzauber hinzufügen";
            IsPetLoaded = true;
        }

        public bool IncreaseAttribute(string attribute)
        {
            bool CanIncrease = true;
            try
            {
                Pet.IncreaseAttribute(attribute);
            }
            catch(Exception e)
            {
                if(e.Message != "Maximum reached")
                {
                    ErrorMessages.Error(e.Message);
                }
                CanIncrease = false;
            }
            return CanIncrease;
        }

        public void IncreaseAttack()
        {
            string[] values = Attack.Split("/");
            Attack = (int.Parse(values[0]) + 1).ToString();
            if (Pet.IsFlying)
            {
                Attack += "/" + (int.Parse(values[1]) + 1).ToString();
            }
        }

        public void IncreaseParry()
        {
            string[] values = Parry.Split("/");
            Parry = (int.Parse(values[0]) + 1).ToString();
            if (Pet.IsFlying)
            {
                Parry += "/" + (int.Parse(values[1]) + 1).ToString();
            }
        }

        public void IncreaseGs()
        {
            string[] values = Gs.Split("/");
            Gs = (int.Parse(values[0]) + 1).ToString();
            if (Pet.IsFlying)
            {
                Gs += "/" + (int.Parse(values[1]) + 1).ToString();
            }
        }

        partial void OnMuChanged(int value)
        {
            Pet.MU = value;
        }

        partial void OnKlChanged(int value)
        {
            Pet.KL = value;
        }

        partial void OnInChanged(int value)
        {
            Pet.IN = value;
        }

        partial void OnChChanged(int value)
        {
            Pet.CH = value;
        }

        partial void OnFfChanged(int value)
        {
            Pet.FF = value;
        }

        partial void OnGeChanged(int value)
        {
            Pet.GE = value;
        }

        partial void OnKoChanged(int value)
        {
            Pet.KO = value;
        }

        partial void OnKkChanged(int value)
        {
            Pet.KK = value;
        }

        partial void OnMrChanged(int value)
        {
            Pet.MR = value;
        }

        partial void OnAupChanged(int value)
        {
            Pet.AuP = value;
        }

        partial void OnLepChanged(int value)
        {
            Pet.LeP = value;
        }

        partial void OnAspChanged(int value)
        {
            Pet.AsP = value;
        }

        [RelayCommand]
        private void ResetAup()
        {
            Pet.ResetAuP();
            Aup = Pet.LeP;
        }

        [RelayCommand]
        private void ResetLep()
        {
            Pet.ResetLeP();
            Lep = Pet.LeP;
        }

        [RelayCommand]
        private void ResetAsp()
        {
            Pet.ResetAsP();
            Asp = Pet.AsP;
        }
    }
}
