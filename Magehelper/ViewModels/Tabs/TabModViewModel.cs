using System.Xml;

namespace Magehelper.ViewModels.Tabs;

public partial class TabModViewModel : ObservableObject, IRecipient<CharacterLoadedMessage>
{
    [ObservableProperty]
    private string _infoText;
    [ObservableProperty]
    private string _representation;
    [ObservableProperty]
    private string _modValueTotal = string.Empty;
    [ObservableProperty]
    private bool _useMr;
    private readonly Mod _mod = new();
    public ObservableCollection<Modification> ModList { get; set; } = [];
    public ObservableCollection<Modification> ModDataList { get; set; } = [];
    public IEnumerable<string> RepresentationList { get; set; }

    public TabModViewModel()
    {
        RepresentationList = _mod.Representations;
        if (_mod.Data != null)
        {
            ModDataList.AddRange(_mod.Data);
        }
        Representation = _mod.CurrentRepresentation ?? RepresentationList.First();
        InfoText = _mod.InfoText ?? string.Empty;
        ModList.CollectionChanged += ModList_CollectionChanged!;
        WeakReferenceMessenger.Default.Register(this);
    }

    private void ModList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action != NotifyCollectionChangedAction.Reset && ModList.Count > 0)
        {
            ModValueTotal = _mod.Calculate().ToString();
        }
        else
        {
            ModValueTotal = string.Empty;
        }
    }

    public void AddMr(int mr)
    {
        _mod.Mr = mr;
        _mod.UseMr = true;
        ModList.Add(new($"Magieresistenz: {mr}", "MR", mr));
    }

    partial void OnRepresentationChanged(string value)
    {
        _mod.SetRepresentation(value);
        InfoText = _mod.InfoText ?? string.Empty;
        ModDataList.Clear();
        ModDataList.AddRange(_mod.Data!);
    }

    [RelayCommand]
    private void AddMod(Modification modification)
    {
        _mod.Add(modification.Amount);
        ModList.Add(modification);
    }

    [RelayCommand]
    private void RemoveMod(Modification modification)
    {
        if (modification.Description != "MR")
        {
            _mod.Remove(modification.Amount);
        }
        else
        {
            _mod.Mr = 0;
            _mod.UseMr = false;
            UseMr = false;
        }
        ModList.Remove(modification);
    }

    [RelayCommand]
    private void Reset()
    {
        ModList.Clear();
        _mod.Reset();
    }

    public void Receive(CharacterLoadedMessage message)
    {
        XmlDocument xml = new();
        xml.LoadXml(message.Value.XML);
        string profession = xml.SelectSingleNode("//ausbildung[@art='Hauptprofession']")!
                                .Attributes!["name"]!
                                .Value
                                .Replace("helden.model.profession.", "");

        Representation = profession == "Magier" ? "Gildenmagier" : profession;
    }
}
