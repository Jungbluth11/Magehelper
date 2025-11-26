using DSAUtils.Settings.Aventurien;

namespace Magehelper.ViewModels.Pages;

public partial class ArcaneGlyphGeneratorPageViewModel : ObservableObject, IRecipient<DeleteAdditionalGlyphMessage>
{
    private readonly ArcaneGlyphs _arcaneGlyphs = Core.Core.GetInstance().ArcaneGlyphs!;
    private readonly Character? _character = Core.Core.GetInstance().Character;
    private readonly List<AdditionalGlyph> _additionalGlyphs = [];
    private double _size;
    private int _complexity;
    private int _cost;
    private int _rkp;
    private int _rollCreatePointsLeft;
    private int _toolModifier;
    private int? _duration;
    [ObservableProperty] private bool _isAdditionalGlyphValueVisible;
    [ObservableProperty] private bool _isIntervalVisible;
    [ObservableProperty] private bool _isPotenzVisible;
    [ObservableProperty] private bool _isSizeVisible;
    [ObservableProperty] private bool _isTargetStringVisible;
    [ObservableProperty] private int _ch = 8;
    [ObservableProperty] private int _currentAdditionalGlyphValueIndex;
    [ObservableProperty] private int _ff = 8;
    [ObservableProperty] private int _ge = 8;
    [ObservableProperty] private int _in = 8;
    [ObservableProperty] private int _kk = 8;
    [ObservableProperty] private int _kl = 8;
    [ObservableProperty] private int _ko = 8;
    [ObservableProperty] private int _mu = 8;
    [ObservableProperty] private int _reliefValue;
    [ObservableProperty] private int _rkw;
    [ObservableProperty] private int _talentValue;
    [ObservableProperty] private string _appliedTo = string.Empty;
    [ObservableProperty] private string _complexityString = string.Empty;
    [ObservableProperty] private string _costString = string.Empty;
    [ObservableProperty] private string _currentAdditionalGlyphName;
    [ObservableProperty] private string _currentMainGlyphName;
    [ObservableProperty] private string _currentToolModifier;
    [ObservableProperty] private string _currentUsedTalent;
    [ObservableProperty] private string _durationString = string.Empty;
    [ObservableProperty] private string _rollActivateResult = string.Empty;
    [ObservableProperty] private string _rollActivateString = "Probe: KL/IN/FF";
    [ObservableProperty] private string _rollCreateResult = string.Empty;
    [ObservableProperty] private string _rollCreateString = string.Empty;
    [ObservableProperty] private string _sizeString = string.Empty;
    [ObservableProperty] private string _targetString = string.Empty;
    public IEnumerable<string> AdditinoalGlyphNames { get; }
    public ObservableCollection<AdditionalGlyphControlViewModel> AdditionalGlyphs { get; } = [];
    public string[] IntervalStrings => ArcaneGlyphs.IntervalStrings;
    public IEnumerable<string> MainGlyphNames { get; }
    public string[] PotenzStrings => ArcaneGlyphs.PotenzStrings;
    public string[] SizeStrings => ArcaneGlyphs.SizeStrings;
    public IEnumerable<string> ToolModifierStrings { get; }
    public IEnumerable<string> UsableTalentNames { get; }

    public ArcaneGlyphGeneratorPageViewModel()
    {
        if (_character is { IsLoaded: true })
        {
            _arcaneGlyphs.LoadFromCharacter();

            Mu = _character!.Mu;
            Kl = _character.Kl;
            In = _character.In;
            Ch = _character.Ch;
            Ff = _character.Ff;
            Ge = _character.Ge;
            Ko = _character.Ko;
            Kk = _character.Kk;

            MainGlyphNames =
                from mainGlyph in ArcaneGlyphs.MainGlyphs
                let r = _character.Rituals!.Where(c => c.Name.Contains(mainGlyph.Name))
                where r.Any()
                select mainGlyph.Name;

            AdditinoalGlyphNames =
                from additionalGlyph in ArcaneGlyphs.AdditionalGlyphs
                let r = _character.Rituals!.Where(c => c.Name.Contains(additionalGlyph.Name))
                where r.Any()
                select additionalGlyph.Name;

            UsableTalentNames = from KeyValuePair<string, int> u in _arcaneGlyphs.UsableTalents select u.Key;
        }
        else
        {
            MainGlyphNames = from mainGlyph in ArcaneGlyphs.MainGlyphs select mainGlyph.Name;
            AdditinoalGlyphNames = from additionalGlyph in ArcaneGlyphs.AdditionalGlyphs select additionalGlyph.Name;
            UsableTalentNames = ArcaneGlyphs.UsableTalentNames;
        }

        ToolModifierStrings = from KeyValuePair<string, int> t in ArcaneGlyphs.ToolModifiers select t.Key;

        CurrentMainGlyphName = MainGlyphNames.First();
        CurrentAdditionalGlyphName = AdditinoalGlyphNames.First();
        CurrentUsedTalent = UsableTalentNames.First();
        CurrentToolModifier = "Normal";
        CurrentAdditionalGlyphValueIndex = 0;
        Rkw = _arcaneGlyphs.Rkw;

        AdditionalGlyphs.CollectionChanged += AdditionalGlyphs_CollectionChanged;

        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(DeleteAdditionalGlyphMessage message)
    {
        for (int i = 0; i < _additionalGlyphs.Count; i++)
        {
            if (!message.Value.Text.Contains(_additionalGlyphs[i].Name))
            {
                continue;
            }

            _additionalGlyphs.Remove(_additionalGlyphs[i]);
            break;
        }

        AdditionalGlyphs.Remove(message.Value);
        Calculate();
    }

    private void AdditionalGlyphs_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        Calculate();
    }

    public void AddGlyph()
    {
        ArcaneGlyph arcaneGlyph = _arcaneGlyphs.Add(
            CurrentMainGlyphName,
            AppliedTo,
            [.. _additionalGlyphs],
            _size,
            Rkw,
            _rkp,
            _cost,
            _duration,
            _duration);

        WeakReferenceMessenger.Default.Send(new AddArcaneGlyphMessage(arcaneGlyph));
    }

    private void Calculate()
    {
        (_complexity, _size, _cost, _duration) = _arcaneGlyphs.Calculate(CurrentMainGlyphName, [.. _additionalGlyphs]);
        ComplexityString = $"Komplexität: {_complexity}";
        SizeString = $"Größe: {_size} Finger";
        CostString = $"Kosten: {_cost} AsP";

        if (_additionalGlyphs.Any(a => a.Name != "Zusatzzeichen Satinavs Siegel") ||
            _additionalGlyphs.Any(a => a.Name != "Zusatzzeichen Kraftquellenspeisung"))
        {
            foreach (AdditionalGlyph additionalGlyph in _additionalGlyphs)
            {
                DurationString = additionalGlyph.Name switch
                {
                    "Zusatzzeichen Satinavs Siegel" => $"Dauer: {ArcaneGlyphs.IntervalStrings[int.Parse(additionalGlyph.Value) - 1]}",
                    "Zusatzzeichen Kraftquellenspeisung" => "Dauer: Permanent",
                    _ => DurationString
                };
            }
        }
        else
        {
            DurationString = $"Dauer: {_duration} Tage";
        }
    }

    private int GetAttribute(string attributeName)
    {
        return attributeName switch
        {
            "MU" => Mu,
            "KL" => Kl,
            "IN" => In,
            "CH" => Ch,
            "FF" => Ff,
            "GE" => Ge,
            "KO" => Ko,
            _ => Kk
        };
    }

    partial void OnCurrentAdditionalGlyphNameChanged(string value)
    {
        CurrentAdditionalGlyphValueIndex = 0;

        switch (value)
        {
            case "Zusatzzeichen Satinavs Siegel":
                IsIntervalVisible = true;
                IsPotenzVisible = false;
                IsSizeVisible = false;
                IsTargetStringVisible = false;

                break;
            case "Zusatzzeichen Potenzierung":
                IsIntervalVisible = false;
                IsPotenzVisible = true;
                IsSizeVisible = false;
                IsTargetStringVisible = false;

                break;
            case "Zusatzzeichen Verkleinerung":
                IsIntervalVisible = false;
                IsPotenzVisible = false;
                IsSizeVisible = true;
                IsTargetStringVisible = false;

                break;
            case "Zusatzzeichen Zielbeschränkung":
                IsIntervalVisible = false;
                IsPotenzVisible = false;
                IsSizeVisible = false;
                IsTargetStringVisible = true;

                break;
            default:
                IsIntervalVisible = false;
                IsPotenzVisible = false;
                IsSizeVisible = false;
                IsTargetStringVisible = false;

                break;
        }
    }

    partial void OnCurrentMainGlyphNameChanged(string value)
    {
        Calculate();
    }

    partial void OnCurrentToolModifierChanged(string value)
    {
        _toolModifier = ArcaneGlyphs.ToolModifiers[value];
        SetRollCreateString();
    }

    partial void OnCurrentUsedTalentChanged(string value)
    {
        if (_character!.IsLoaded)
        {
            TalentValue = _arcaneGlyphs.UsableTalents[value];
        }

        SetRollCreateString();
    }

    partial void OnReliefValueChanged(int value)
    {
        string modifier = value switch
        {
            > 0 => $"+{value}",
            < 0 => value.ToString(),
            _ => string.Empty
        };

        RollActivateString = $"Probe: KL/IN/FF {modifier}";
    }

    partial void OnRkwChanged(int value)
    {
        _arcaneGlyphs.Rkw = value;
        Calculate();
    }

    private void SetRollCreateString()
    {
        Talent talent = Aventurien.GetTalent(CurrentUsedTalent);
        string[] attributes = talent.Eigenschaften.Split('/');

        string modifier = _toolModifier switch
        {
            > 0 => $"+{_toolModifier}",
            < 0 => _toolModifier.ToString(),
            _ => string.Empty
        };

        RollCreateString = $"Probe: {attributes[0]}/{attributes[1]}/{attributes[2]} {modifier}";
    }

    [RelayCommand]
    private void AddAdditionalGlyph()
    {

        if (_additionalGlyphs.Any(additionalGlyph => CurrentAdditionalGlyphName == additionalGlyph.Name))
        {
            return;
        }

        string displayValue;
        string value;

        switch (CurrentAdditionalGlyphName)
        {
            case "Zusatzzeichen Satinavs Siegel":
                displayValue =
                    $"{CurrentAdditionalGlyphName} ({ArcaneGlyphs.IntervalStrings[CurrentAdditionalGlyphValueIndex]})";

                value = (CurrentAdditionalGlyphValueIndex + 1).ToString();

                break;
            case "Zusatzzeichen Potenzierung":
                displayValue =
                    $"{CurrentAdditionalGlyphName} ({ArcaneGlyphs.PotenzStrings[CurrentAdditionalGlyphValueIndex]})";

                value = (CurrentAdditionalGlyphValueIndex + 1).ToString();

                break;
            case "Zusatzzeichen Verkleinerung":
                displayValue =
                    $"{CurrentAdditionalGlyphName} ({ArcaneGlyphs.SizeStrings[CurrentAdditionalGlyphValueIndex]} pro Punkt Komplexität)";

                value = (CurrentAdditionalGlyphValueIndex + 1).ToString();

                break;
            case "Zusatzzeichen Zielbeschränkung":
                displayValue = $"{CurrentAdditionalGlyphName} ({TargetString})";
                value = TargetString;

                break;
            default:
                displayValue = CurrentAdditionalGlyphName;
                value = string.Empty;

                break;
        }

        _additionalGlyphs.Add(new(CurrentAdditionalGlyphName, value));
        AdditionalGlyphs.Add(new(displayValue));
    }

    [RelayCommand]
    private void RollActivate()
    {
        (_rkp, _, string textResult) = _arcaneGlyphs.RollCreate(Kl, In, Ff, TalentValue, _toolModifier);

        RollActivateResult = $"{_rkp} Punkte über {textResult} ";
    }

    [RelayCommand]
    private void RollCreate()
    {
        Talent talent = Aventurien.GetTalent(CurrentUsedTalent);
        string[] attributes = talent.Eigenschaften.Split('/');
        int attributeValue1 = GetAttribute(attributes[0]);
        int attributeValue2 = GetAttribute(attributes[1]);
        int attributeValue3 = GetAttribute(attributes[2]);

        (_rollCreatePointsLeft, _, string textResult) = _arcaneGlyphs.RollCreate(
            attributeValue1,
            attributeValue2,
            attributeValue3,
            TalentValue,
            _toolModifier);

        RollCreateResult = $"{_rollCreatePointsLeft} Punkte über {textResult}";
    }
}
