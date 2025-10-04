

namespace Magehelper.ViewModels.Dialogs
{
    public partial class PetGeneratorWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _mu;
        [ObservableProperty]
        private int _muMin;
        [ObservableProperty]
        private int _muMax;
        [ObservableProperty]
        private int _kl;
        [ObservableProperty]
        private int _klMin;
        [ObservableProperty]
        private int _klMax;
        [ObservableProperty]
        private int _in;
        [ObservableProperty]
        private int _inMin;
        [ObservableProperty]
        private int _inMax;
        [ObservableProperty]
        private int _ch;
        [ObservableProperty]
        private int _chMin;
        [ObservableProperty]
        private int _chMax;
        [ObservableProperty]
        private int _ff;
        [ObservableProperty]
        private int _ffMin;
        [ObservableProperty]
        private int _ffMax;
        [ObservableProperty]
        private int _ge;
        [ObservableProperty]
        private int _geMin;
        [ObservableProperty]
        private int _geMax;
        [ObservableProperty]
        private int _ko;
        [ObservableProperty]
        private int _koMin;
        [ObservableProperty]
        private int _koMax;
        [ObservableProperty]
        private int _kk;
        [ObservableProperty]
        private int _kkMin;
        [ObservableProperty]
        private int _kkMax;
        [ObservableProperty]
        private int _le;
        [ObservableProperty]
        private int _leMin;
        [ObservableProperty]
        private int _leMax;
        [ObservableProperty]
        private int _ae;
        [ObservableProperty]
        private int _aeMin;
        [ObservableProperty]
        private int _aeMax;
        [ObservableProperty]
        private int _au;
        [ObservableProperty]
        private int _auMin;
        [ObservableProperty]
        private int _auMax;
        [ObservableProperty]
        private int _cost;
        [ObservableProperty]
        private int _pointsValue;
        [ObservableProperty]
        private int _pointsRemain;
        [ObservableProperty]
        private string _species;
        [ObservableProperty]
        private bool _isMightyCompanion = false;
        private readonly PetGenerator petGenerator;
        private readonly Action uiAction;
        public IEnumerable<string> SpeciesStrings { get; }

        public PetGeneratorWindowViewModel(Pet pet, Action action)
        {
            petGenerator = new PetGenerator(pet);
            SpeciesStrings = petGenerator.SpeciesStrings;
            Species = petGenerator.Species;
            Cost = petGenerator.Cost;
            uiAction = action;
        }

        partial void OnMuChanged(int value)
        {
            petGenerator.MU = value;
            PointsRemain = petGenerator.PointsRemain;
        }

        partial void OnKlChanged(int value)
        {
            petGenerator.KL = value;
            PointsRemain = petGenerator.PointsRemain;
            Cost = petGenerator.Cost;
        }

        partial void OnInChanged(int value)
        {
            petGenerator.IN = value;
            PointsRemain = petGenerator.PointsRemain;
            Cost = petGenerator.Cost;
        }

        partial void OnChChanged(int value)
        {
            petGenerator.CH = value;
            PointsRemain = petGenerator.PointsRemain;
            Cost = petGenerator.Cost;
        }

        partial void OnFfChanged(int value)
        {
            petGenerator.FF = value;
            PointsRemain = petGenerator.PointsRemain;
            Cost = petGenerator.Cost;
        }

        partial void OnGeChanged(int value)
        {
            petGenerator.GE = value;
            PointsRemain = petGenerator.PointsRemain;
            Cost = petGenerator.Cost;
        }


        partial void OnKoChanged(int value)
        {
            petGenerator.KO = value;
            PointsRemain = petGenerator.PointsRemain;
            Cost = petGenerator.Cost;
        }

        partial void OnKkChanged(int value)
        {
            petGenerator.KK = value;
            PointsRemain = petGenerator.PointsRemain;
            Cost = petGenerator.Cost;
        }

        partial void OnLeChanged(int value)
        {
            petGenerator.LE = value;
            PointsRemain = petGenerator.PointsRemain;
            Cost = petGenerator.Cost;
        }

        partial void OnAeChanged(int value)
        {
            petGenerator.AE = value;
            PointsRemain = petGenerator.PointsRemain;
            Cost = petGenerator.Cost;
        }

        partial void OnAuChanged(int value)
        {
            petGenerator.AU = value;
            PointsRemain = petGenerator.PointsRemain;
            Cost = petGenerator.Cost;
        }

        partial void OnSpeciesChanged(string value)
        {
            petGenerator.Species = value;
            Mu = petGenerator.BaseData.MUStartMin;
            MuMin = petGenerator.BaseData.MUStartMin;
            MuMax = petGenerator.BaseData.MUStartMax;
            Kl = petGenerator.BaseData.KLStartMin;
            KlMin = petGenerator.BaseData.KLStartMin;
            KlMax = petGenerator.BaseData.KLStartMax;
            In = petGenerator.BaseData.INStartMin;
            InMin = petGenerator.BaseData.INStartMin;
            InMax = petGenerator.BaseData.INStartMax;
            Ch = petGenerator.BaseData.CHStartMin;
            ChMin = petGenerator.BaseData.CHStartMin;
            ChMax = petGenerator.BaseData.CHStartMax;
            Ff = petGenerator.BaseData.FFStartMin;
            FfMin = petGenerator.BaseData.FFStartMin;
            FfMax = petGenerator.BaseData.FFStartMax;
            Ge = petGenerator.BaseData.GEStartMin;
            GeMin = petGenerator.BaseData.GEStartMin;
            GeMax = petGenerator.BaseData.GEStartMax;
            Ko = petGenerator.BaseData.KOStartMin;
            KoMin = petGenerator.BaseData.KOStartMin;
            KoMax = petGenerator.BaseData.KOStartMax;
            Kk = petGenerator.BaseData.KKStartMin;
            KkMin = petGenerator.BaseData.KKStartMin;
            KkMax = petGenerator.BaseData.KKStartMax;
            Le = petGenerator.BaseData.LEStartMin;
            LeMin = petGenerator.BaseData.LEStartMin;
            LeMax = petGenerator.BaseData.LEStartMax;
            Ae = petGenerator.BaseData.AEStartMin;
            AeMin = petGenerator.BaseData.AEStartMin;
            AeMax = petGenerator.BaseData.AEStartMax;
            Au = petGenerator.BaseData.AUStartMin;
            AuMin = petGenerator.BaseData.AUStartMin;
            AuMax = petGenerator.BaseData.AUStartMax;
            Cost = petGenerator.Cost;
        }

        partial void OnIsMightyCompanionChanged(bool value)
        {
            petGenerator.IsMightyCompanion = value;
            PointsRemain = petGenerator.PointsRemain;
            Cost = petGenerator.Cost;
        }

        [RelayCommand]
        private void Submit(Window window)
        {
            petGenerator.AddPet();
            uiAction();

            window.Close();
        }
    }
}
