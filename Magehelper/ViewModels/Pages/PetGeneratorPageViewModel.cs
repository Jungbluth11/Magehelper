namespace Magehelper.ViewModels.Pages;

public partial class PetGeneratorPageViewModel : ObservableObject
{
    private readonly PetGenerator _petGenerator;

    [ObservableProperty] private bool _isMightyCompanion;
    [ObservableProperty] private int _ae;
    [ObservableProperty] private int _aeMax;
    [ObservableProperty] private int _aeMin;
    [ObservableProperty] private int _au;
    [ObservableProperty] private int _auMax;
    [ObservableProperty] private int _auMin;
    [ObservableProperty] private int _ch;
    [ObservableProperty] private int _chMax;
    [ObservableProperty] private int _chMin;
    [ObservableProperty] private int _cost;
    [ObservableProperty] private int _ff;
    [ObservableProperty] private int _ffMax;
    [ObservableProperty] private int _ffMin;
    [ObservableProperty] private int _ge;
    [ObservableProperty] private int _geMax;
    [ObservableProperty] private int _geMin;
    [ObservableProperty] private int _in;
    [ObservableProperty] private int _inMax;
    [ObservableProperty] private int _inMin;
    [ObservableProperty] private int _kk;
    [ObservableProperty] private int _kkMax;
    [ObservableProperty] private int _kkMin;
    [ObservableProperty] private int _kl;
    [ObservableProperty] private int _klMax;
    [ObservableProperty] private int _klMin;
    [ObservableProperty] private int _ko;
    [ObservableProperty] private int _koMax;
    [ObservableProperty] private int _koMin;
    [ObservableProperty] private int _le;
    [ObservableProperty] private int _leMax;
    [ObservableProperty] private int _leMin;
    [ObservableProperty] private int _mu;
    [ObservableProperty] private int _muMax;
    [ObservableProperty] private int _muMin;
    [ObservableProperty] private int _pointsRemain;
    [ObservableProperty] private int _pointsValue;
    [ObservableProperty] private string _species;

    public IEnumerable<string> SpeciesStrings { get; }

    public PetGeneratorPageViewModel()
    {
        _petGenerator = new();
        SpeciesStrings = _petGenerator.SpeciesStrings;
        Species = _petGenerator.Species;
        Cost = _petGenerator.Cost;
    }

    partial void OnAeChanged(int value)
    {
        _petGenerator.AE = value;
        PointsRemain = _petGenerator.PointsRemain;
        Cost = _petGenerator.Cost;
    }

    partial void OnAuChanged(int value)
    {
        _petGenerator.AU = value;
        PointsRemain = _petGenerator.PointsRemain;
        Cost = _petGenerator.Cost;
    }

    partial void OnChChanged(int value)
    {
        _petGenerator.CH = value;
        PointsRemain = _petGenerator.PointsRemain;
        Cost = _petGenerator.Cost;
    }

    partial void OnFfChanged(int value)
    {
        _petGenerator.FF = value;
        PointsRemain = _petGenerator.PointsRemain;
        Cost = _petGenerator.Cost;
    }

    partial void OnGeChanged(int value)
    {
        _petGenerator.GE = value;
        PointsRemain = _petGenerator.PointsRemain;
        Cost = _petGenerator.Cost;
    }

    partial void OnInChanged(int value)
    {
        _petGenerator.IN = value;
        PointsRemain = _petGenerator.PointsRemain;
        Cost = _petGenerator.Cost;
    }

    partial void OnIsMightyCompanionChanged(bool value)
    {
        _petGenerator.IsMightyCompanion = value;
        PointsRemain = _petGenerator.PointsRemain;
        Cost = _petGenerator.Cost;
    }

    partial void OnKkChanged(int value)
    {
        _petGenerator.KK = value;
        PointsRemain = _petGenerator.PointsRemain;
        Cost = _petGenerator.Cost;
    }

    partial void OnKlChanged(int value)
    {
        _petGenerator.KL = value;
        PointsRemain = _petGenerator.PointsRemain;
        Cost = _petGenerator.Cost;
    }


    partial void OnKoChanged(int value)
    {
        _petGenerator.KO = value;
        PointsRemain = _petGenerator.PointsRemain;
        Cost = _petGenerator.Cost;
    }

    partial void OnLeChanged(int value)
    {
        _petGenerator.LE = value;
        PointsRemain = _petGenerator.PointsRemain;
        Cost = _petGenerator.Cost;
    }

    partial void OnMuChanged(int value)
    {
        _petGenerator.MU = value;
        PointsRemain = _petGenerator.PointsRemain;
    }

    partial void OnSpeciesChanged(string value)
    {
        _petGenerator.Species = value;
        Mu = _petGenerator.BaseData.MUStartMin;
        MuMin = _petGenerator.BaseData.MUStartMin;
        MuMax = _petGenerator.BaseData.MUStartMax;
        Kl = _petGenerator.BaseData.KLStartMin;
        KlMin = _petGenerator.BaseData.KLStartMin;
        KlMax = _petGenerator.BaseData.KLStartMax;
        In = _petGenerator.BaseData.INStartMin;
        InMin = _petGenerator.BaseData.INStartMin;
        InMax = _petGenerator.BaseData.INStartMax;
        Ch = _petGenerator.BaseData.CHStartMin;
        ChMin = _petGenerator.BaseData.CHStartMin;
        ChMax = _petGenerator.BaseData.CHStartMax;
        Ff = _petGenerator.BaseData.FFStartMin;
        FfMin = _petGenerator.BaseData.FFStartMin;
        FfMax = _petGenerator.BaseData.FFStartMax;
        Ge = _petGenerator.BaseData.GEStartMin;
        GeMin = _petGenerator.BaseData.GEStartMin;
        GeMax = _petGenerator.BaseData.GEStartMax;
        Ko = _petGenerator.BaseData.KOStartMin;
        KoMin = _petGenerator.BaseData.KOStartMin;
        KoMax = _petGenerator.BaseData.KOStartMax;
        Kk = _petGenerator.BaseData.KKStartMin;
        KkMin = _petGenerator.BaseData.KKStartMin;
        KkMax = _petGenerator.BaseData.KKStartMax;
        Le = _petGenerator.BaseData.LEStartMin;
        LeMin = _petGenerator.BaseData.LEStartMin;
        LeMax = _petGenerator.BaseData.LEStartMax;
        Ae = _petGenerator.BaseData.AEStartMin;
        AeMin = _petGenerator.BaseData.AEStartMin;
        AeMax = _petGenerator.BaseData.AEStartMax;
        Au = _petGenerator.BaseData.AUStartMin;
        AuMin = _petGenerator.BaseData.AUStartMin;
        AuMax = _petGenerator.BaseData.AUStartMax;
        Cost = _petGenerator.Cost;
    }

    public void Submit()
    {
        _petGenerator.AddPet();
        WeakReferenceMessenger.Default.Send(new FileActionMessage(FileAction.Changed));
    }
}