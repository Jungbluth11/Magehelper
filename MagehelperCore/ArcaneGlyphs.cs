using System.Globalization;

namespace Magehelper.Core;

public class ArcaneGlyphs : IEnumerable<ArcaneGlyph>
{
    private readonly Core _core = Core.GetInstance();
    private readonly List<ArcaneGlyph> _arcaneGlyphs = [];

    public static MainGlyphData[] MainGlyphs =>
    [
        new("Auge der Ewigen Wacht", ["Dämonisch (Thargunitoth)", "Temporal"], 9),
        new("Auge des Basilisken", ["Elementar (Erz)", "Form"], 9),
        new("Auge des Mondes", ["Hellsicht", "Illusion", "Kraft"], 5),
        new("Fallensiegel", ["Elementar (Einzelelement)", "Schaden"], 3),
        new("Fanal der Herrschaft", ["Eigenschaften", "Einfluss"], 7),
        new("Fixierungszeichen", ["Objekt"], 6),
        new("Gezücht des Meister", ["Hellsicht", "Herrschaft", "Temporal"], 8),
        new("Glyphe der elementaren Attraktion", ["Elementar", "Umwelt"], 4),
        new("Glyphe der elementaren Bannung", ["Antimagie", "Elementar"], 5),
        new("Glyphe des verfluchten Goldes", ["Objekt", "Eigenschaften", "Schaden"], 8),
        new("Hermetisches Siegel", ["Objekt", "Temporal"], 3),
        new("Hypnotisches Zeichen", ["Einfluss"], 8),
        new("Leuchtendes Zeichen", ["Umwelt"], 1),
        new("Markierung des Todes", ["Objekt", "Telekinese"], 7),
        new("Siegel der Seelenruhe", ["Einfluss"], 3),
        new("Siegel der Stille", ["Umwelt"], 2),
        new("Siegel der zweiten Haut", ["Kraft", "Objekt"], 3),
        new("Sigille der Schatten", ["Umwelt"], 2),
        new("Sigille des unsichtbaren Trägers", ["Elementar (Luft)"], 4),
        new("Sigille des unsichtbaren Weges", ["Elementar (Luft)", "Umwelt"], 4),
        new("Singendes Zeichen", ["Illusion"], 2),
        new("Ungesehenes Zeichen", ["Einfluss", "Illusion"], 5),
        new("Verderben des Magiers", ["Kraft", "Schaden", "Verständigung"], 8),
        new("Verständigungszeichen", ["Verständigung"], 5),
        new("Wimmelndes Zeichen", ["Dämonisch (Mishkara)", "Herbeirufung"], 5),
        new("Wunschglyphe", ["Einfluss", "Hellsicht", "Illusion"], 6),
        new("Zähne des Feuers", ["Objekt", "Schaden"], 6),
        new("Zeichen der Zauberschmiede", ["Objekt", "Schaden"], 6),
        new("Zeichen des Handwerks", ["Objekt"], 5),
        new("Zeichen des Stillstands", ["Elementar (Eis)", "Umwelt"], 6),
        new("Zeichen des versperrten Blicks", ["Antimagie", "Hellsicht"], 7),
        new("Zeichen gegen Magie", ["Metamagie", "Antimagie", "Objekt"], 4)
    ];

    public static AdditionalGlyphData[] AdditionalGlyphs =>
    [
        new("Zusatzzeichen Kraftquellenspeisung", ["Temporal", "Metamagie", "Kraft"], 5, 1),
        new("Zusatzzeichen Magiewiderstand", ["Metamagie", "Antimagie", "Objekt"], 2, 1),
        new("Zusatzzeichen Potenzierung", ["Kraft", "Metamagie"], 2, 4),
        new("Zusatzzeichen Satinavs Siegel", ["Metamagie", "Temporal"], 1, 3),
        new("Zusatzzeichen Schutzsiegel", ["Elementar", "Schaden"], 3, 1),
        new("Zusatzzeichen Tarnung", ["Illusion"], 3, 1),
        new("Zusatzzeichen Verkleinerung", ["Metamagie"], 1, 3),
        new("Zusatzzeichen Zielbeschränkung", ["Hellsicht"], 2, 1)
    ];

    public static string[] UsableTalentNames =>
    [
        "Malen/Zeichnen",
        "Holzbearbeitung",
        "Steinmetz",
        "Feinmechanik",
        "Schneidern",
        "Webkunst"
    ];

    public static Dictionary<string, int> ToolModifiers => new()
    {
        {"Improvisiert", -7},
        {"fehlendes Spezialwerkzeug", -3},
        {"Normal", 0},
        {"Hochwertig", 3},
        {"Außergewöhnlich", 7}
    };

    public static string[] IntervalStrings =>
    [
        "Monat",
        "Quartal",
        "bis zur nächsten Wintersonnenwende"
    ];

    public static string[] SizeStrings =>
    [
        "1 Finger",
        "1 Halbfinger",
        "0,5 Halbfinger"
    ];

    public static string[] PotenzStrings =>
    [
        "doppelter Radius oder Kutsche",
        "vierfacher Radius oder Schiff",
        "achtfacher Radius oder Burg",
        "sechzehnfacher Radius oder Berg"
    ];

    public Dictionary<string, int> UsableTalents { get; private set; } = [];
    public int Rkw { get; set; } = 3;
    public int Count => _arcaneGlyphs.Count;
    public ArcaneGlyph this[int i] => _arcaneGlyphs[i];
    public ArcaneGlyph this[string guid] => _arcaneGlyphs.SingleOrDefault(a => a.Guid == guid);

    public ArcaneGlyphs()
    {
        _core.ArcaneGlyphs = this;
        Readfile();
    }

    internal void Readfile()
    {
        if (_core.XmlDoc?.SelectSingleNode("//arcaneGlyphs") == null || !_core.XmlDoc.SelectSingleNode("//arcaneGlyphs")!.HasChildNodes)
        {
            return;
        }
        
        foreach (XmlNode arcaneGlyph in _core.XmlDoc.GetElementsByTagName("arcaneGlyph"))
        {
            string guid = arcaneGlyph!.Attributes!["guid"]!.Value;
            string name = arcaneGlyph.Attributes!["name"]!.Value;
            string appliedTo = arcaneGlyph.Attributes!["appliedTo"]!.Value;
            double size = double.Parse(arcaneGlyph.Attributes!["size"]!.Value, CultureInfo.InvariantCulture);
            int rkw = int.Parse(arcaneGlyph.Attributes!["rkw"]!.Value);
            int rkp = int.Parse(arcaneGlyph.Attributes!["rkp"]!.Value);
            int cost = int.Parse(arcaneGlyph.Attributes!["cost"]!.Value);
            int? duration = arcaneGlyph.Attributes["duration"]!.Value == "null"
                ? null
                : int.Parse(arcaneGlyph.Attributes!["duration"]!.Value);
            int? remainingDuration = arcaneGlyph.Attributes["remainingDuration"]!.Value == "null"
                ? null
                : int.Parse(arcaneGlyph.Attributes!["remainingDuration"]!.Value);
            AdditionalGlyph[] additionalGlyphs = [];

            additionalGlyphs = arcaneGlyph.ChildNodes[0]!.ChildNodes.Cast<XmlNode>()
                .Aggregate(additionalGlyphs,
                    (current, additionalGlyph) =>
                    [
                        .. current,
                        new(additionalGlyph!.Attributes!["name"]!.Value,
                            additionalGlyph.Attributes!["value"]!.Value == "null"
                                ? null
                                : additionalGlyph.Attributes!["value"]!.Value),
                    ]);

            Add(name,
                appliedTo,
                additionalGlyphs,
                size,
                rkw,
                rkp,
                cost,
                duration,
                remainingDuration,
                guid);
        }
    }

    public ArcaneGlyph Add(string name, string appliedTo, AdditionalGlyph[] additionalGlyphs, double size, int rkw, int rkp, int cost, int? duration, int? remainingDuration, string? guid = null)
    {
        ArcaneGlyph arcaneGlyph = new()
        {
            Guid = guid ?? Guid.NewGuid().ToString(),
            Name = name,
            AppliedTo = appliedTo,
            AdditionalGlyphs = additionalGlyphs,
            Size = size,
            Rkw = rkw,
            Rkp = rkp,
            Cost = cost,
            Duration = duration,
            RemainingDuration = remainingDuration
        };

        _arcaneGlyphs.Add(arcaneGlyph);
        _core.FileChanged = true;
        return arcaneGlyph;
    }

    public (int complexity, double size, int cost, int? duration) Calculate(string name, AdditionalGlyph[] additionalGlyphs)
    {
        int? duration = Rkw / 2;
        int complexity = (from mainGlyph in MainGlyphs where mainGlyph.Name == name select mainGlyph.Complexity).First();

        foreach (AdditionalGlyph a in additionalGlyphs)
        {
            int c = (from additionalGlyph in AdditionalGlyphs
                     where additionalGlyph.Name == a.Name
                     select additionalGlyph.AdditionalComplexity).First();

            if (a.Name != "Zusatzzeichen Zielbeschränkung" && !string.IsNullOrEmpty(a.Value))
            {
                c *= int.Parse(a.Value);
            }

            complexity += c;
        }

        double size = complexity * 4;

        foreach (AdditionalGlyph additionalGlyph in additionalGlyphs)
        {
            if (additionalGlyph.Name == "Zusatzzeichen Verkleinerung")
            {
                double baseComplexity = complexity - int.Parse(additionalGlyph.Value!);

                size = int.Parse(additionalGlyph.Value!) switch
                {
                    1 => baseComplexity * 2,
                    2 => baseComplexity * 1,
                    _ => baseComplexity * 0.5
                };
            }

            else if (additionalGlyph.Name == "Zusatzzeichen Satinavs Siegel")
            {
                duration = int.Parse(additionalGlyph.Value!) switch
                {
                    1 => 30,
                    2 => 90,
                    _ => null
                };
            }
        }

        return (complexity, size, complexity * 3, duration);
    }

    public void LoadFromCharacter()
    {
        if (_core.Character == null || !_core.Character!.IsLoaded)
        {
            return;
        }

        string[] rkwList = ["Gildenmagie", "Kristallomantie", "Alchimie", "Zibilja", "Geister binden"];

        foreach (string rkw in rkwList)
        {
            if (!_core.Character.Rkw.TryGetValue(rkw, out int value))
            {
                continue;
            }

            Rkw = value;
            break;
        }

        UsableTalents = _core.Character!.Skills!
            .Where(a => UsableTalentNames.Contains(a.Name))
            .ToDictionary(a => a.Name, a => a.Wert);
    }

    public (int pointsLeft, int[] rollData, string textResult) RollCreate(int value1, int value2, int value3, int talentValue, int mod)
    {
        return DSA.TaP(value1, value2, value3, talentValue, mod);
    }

    public (int pointsLeft, int[] rollData, string textResult) RollActivate(int value1, int value2, int value3, int mod)
    {
        return DSA.TaP(value1, value2, value3, Rkw, mod);
    }

    public int DecreaseDuration(string guid, int amount = 1)
    {
        for (int i = 0; i < _arcaneGlyphs.Count; i++)
        {
            if (_arcaneGlyphs[i].Guid != guid)
            {
                continue;
            }

            ArcaneGlyph arcaneGlyph = _arcaneGlyphs[i];
            arcaneGlyph.RemainingDuration -= amount;
            _arcaneGlyphs[i] = arcaneGlyph;
            _core.FileChanged = true;

            return (int)arcaneGlyph.RemainingDuration!;
        }

        throw new ArgumentException("Guid doesn't exist");
    }

    public void Reactivate(string guid)
    {
        for (int i = 0; i < _arcaneGlyphs.Count; i++)
        {
            if (_arcaneGlyphs[i].Guid != guid)
            {
                continue;
            }

            ArcaneGlyph arcaneGlyph = _arcaneGlyphs[i];
            arcaneGlyph.RemainingDuration = arcaneGlyph.Duration;
            _arcaneGlyphs[i] = arcaneGlyph;
            _core.FileChanged = true;
            break;
        }
    }

    public void Remove(string guid)
    {
        _arcaneGlyphs.Remove(_arcaneGlyphs.Single(a => a.Guid == guid));
        _core.FileChanged = true;
    }

    public void RemoveAll()
    {
        _arcaneGlyphs.Clear();
        _core.FileChanged = true;
    }

    public IEnumerator<ArcaneGlyph> GetEnumerator()
    {
        return _arcaneGlyphs.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _arcaneGlyphs.GetEnumerator();
    }
}
