namespace Magehelper.Core;

public partial class Artifacts : IEnumerable<Artifact>
{
    private readonly Core _core = Core.GetInstance();

    private readonly Dictionary<string, string> _artifactAbilities = new()
    {
        {"siegel", "Siegel und Zertifikat"},
        {"unzerbrechlich", "Unzerbrechlich"},
        {"gespuer", "Gespür des Schöpfers"},
        {"apport", "Apport"},
        {"ferngespuer", "Ferngespür"},
        {"resistent", "Resistenz gegen profanen Schaden"},
        {"reperatur", "Selbstreparatur"},
        {"reversalis", "Reversalis"},
        {"variablerausloeser", "Variabler Auslöser"},
        {"verschleierung", "Verschleierung"},
        {"verzehrend", "Verzehrender Zauber"}
    };

    private readonly List<Artifact> _artifacts = [];

    public static string[] IntervalStrings =>
    [
        "Tag",
        "Woche",
        "Monat",
        "Jahr"
    ];

    public Artifact this[int i] => _artifacts[i];
    public Artifact this[string guid] => _artifacts.SingleOrDefault(a => a.Guid == guid);

    public static string[] TypeStrings =>
    [
        "Einmalig",
        "Wiederaufladbar",
        "Semipermanent",
        "Permanent"
    ];

    public Artifacts()
    {
        _core.Artifacts = this;
    }

    public IEnumerator<Artifact> GetEnumerator()
    {
        return _artifacts.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _artifacts.GetEnumerator();
    }

    public void ActivateArtifact(string guid)
    {
        for (int i = 0; i < _artifacts.Count; i++)
        {
            if (_artifacts[i].Guid != guid)
            {
                continue;
            }

            Artifact artifact = _artifacts[i];
            artifact.CurrentCharges -= 1;
            _artifacts[i] = artifact;
            _core.FileChanged = true;

            break;
        }
    }

    public Artifact CreateArtifact(string name, string description, ArtifactType type, int? charges,
        ArtifactInterval? interval = null, string? guid = null)
    {
        guid ??= Guid.NewGuid().ToString();

        Artifact artifact = type switch
        {
            ArtifactType.Single => new()
            {
                Guid = guid,
                Name = name,
                Description = description,
                Type = type,
                CurrentCharges = 1,
                MaxCharges = 1
            },
            ArtifactType.Rechargeable => new()
            {
                Guid = guid,
                Name = name,
                Description = description,
                Type = type,
                CurrentCharges = charges,
                MaxCharges = charges
            },
            ArtifactType.Semipermanent => new()
            {
                Guid = guid,
                Name = name,
                Description = description,
                Type = type,
                CurrentCharges = null,
                MaxCharges = null,
                Interval = interval
            },
            _ => new()
            {
                Guid = guid,
                Name = name,
                Description = description,
                Type = type,
                CurrentCharges = null,
                MaxCharges = null
            }
        };

        _artifacts.Add(artifact);
        _core.FileChanged = true;

        return artifact;
    }

    public void DeleteAll()
    {
        _artifacts.Clear();
        _core.FileChanged = true;
    }

    public void DeleteArtifact(string guid)
    {
        _artifacts.Remove(_artifacts.Single(a => a.Guid == guid));
        _core.FileChanged = true;
    }

    public Artifact LoadArtifactFromFile(string path)
    {
        string name = Path.GetFileNameWithoutExtension(path);
        XmlDocument xml = new();
        xml.Load(path);

        ArtifactType type = xml.SelectSingleNode("//typ")!.InnerText switch
        {
            "NORMAL" => ArtifactType.Single,
            "RECHARGE" => ArtifactType.Rechargeable,
            "SEMI" => ArtifactType.Semipermanent,
            _ => ArtifactType.Single
        };

        ArtifactInterval? interval = null;

        if (type == ArtifactType.Semipermanent)
        {
            interval = xml.SelectSingleNode("//semi_typ")!.InnerText switch
            {
                "TAG" => ArtifactInterval.Day,
                "WOCHE" => ArtifactInterval.Week,
                "MONAT" => ArtifactInterval.Month,
                _ => ArtifactInterval.Year
            };
        }

        int? charges = null;

        if (type == ArtifactType.Rechargeable)
        {
            charges = int.Parse(xml.SelectSingleNode("//loads")!.InnerText);
        }

        string description = string.Empty;

        XmlNode spellNode = xml.SelectSingleNode("DasArtefakt/zauber")!;

        if (spellNode.HasChildNodes)
        {
            description += "Wirkende Sprüche:\n";

            foreach (XmlNode childNode in spellNode.ChildNodes)
            {
                string spell = childNode.ChildNodes[0]!.InnerText;

                string staple = int.Parse(childNode.ChildNodes[2]!.InnerText) > 1
                    ? $"{childNode.ChildNodes[2]!.InnerText} Stapel; "
                    : string.Empty;

                string asp = childNode.ChildNodes[3]!.InnerText;
                description += $"{spell} ({staple}{asp} AsP)\n";
            }

            description += "\n";
        }

        XmlNode[] artifactAbilityNodes =
        [
            .. xml.SelectNodes("//*")!
                .Cast<XmlNode>()
                .Where(n => n.Name.Contains("spezial_"))
                .Where(node => node.InnerText == "true")
        ];

        // ReSharper disable once InvertIf --- creates duplicate code
        if (artifactAbilityNodes.Length > 0)
        {
            description += "Artefakteigenschaften:\n";

            description = artifactAbilityNodes.Aggregate(description,
                (current, node) => current + $"{_artifactAbilities[node.Name.Split('_')[1]]}\n");
        }

        return new()
        {
            Name = name,
            Description = description,
            Type = type,
            CurrentCharges = charges,
            MaxCharges = charges,
            Interval = interval
        };
    }

    public void RechargeArtifact(string guid)
    {
        for (int i = 0; i < _artifacts.Count; i++)
        {
            if (_artifacts[i].Guid != guid)
            {
                continue;
            }

            Artifact artifact = _artifacts[i];
            artifact.CurrentCharges = artifact.MaxCharges;
            _artifacts[i] = artifact;
            _core.FileChanged = true;

            break;
        }
    }
}
