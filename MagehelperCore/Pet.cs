using System.Reflection;
using DSAUtils.Settings.Aventurien;

namespace Magehelper.Core;

public class Pet
{
    private string? _species;
    /// <summary>
    /// Names of the attributes.
    /// </summary>
    internal string[] AttributeStrings =>
    [
        "MU",
        "KL",
        "IN",
        "CH",
        "FF",
        "GE",
        "KO",
        "KK",
        "LE",
        "AE",
        "AU",
        "MR",
        "Attack",
        "AttackFlying",
        "Parry",
        "ParryFlying",
        "GS",
        "GSFlying"
    ];
    internal int MuStart { get; set; }
    internal int KlStart { get; set; }
    internal int InStart { get; set; }
    internal int ChStart { get; set; }
    internal int FfStart { get; set; }
    internal int GeStart { get; set; }
    internal int KoStart { get; set; }
    internal int KkStart { get; set; }
    internal int LeStart { get; set; }
    internal int AuStart { get; set; }
    internal int MrStart { get; set; }
    internal int AttackStart { get; set; }
    internal int AttackFlyingStart { get; set; }
    internal int ParryStart { get; set; }
    internal int ParryFlyingStart { get; set; }
    internal double GsStart { get; set; }
    internal double GsFlyingStart { get; set; }
    private readonly Core _core = Core.GetInstance();
    /// <summary>
    /// the spells the pet already knows.
    /// </summary>
    internal List<PetSpell> Knownspells { get; set; } = [];
    public int LeP { get; set; }
    public int AuP { get; set; }
    public int AsP { get; set; }
    public int Mu { get; set; }
    public int Kl { get; set; }
    public int In { get; set; }
    public int Ch { get; set; }
    public int Ff { get; set; }
    public int Ge { get; set; }
    public int Ko { get; set; }
    public int Kk { get; set; }
    public int Le { get; internal set; }
    public int Ae { get; internal set; }
    public int Au { get; internal set; }
    public int Mr { get; set; }
    public int Rkw { get; internal set; }
    public int Attack { get; internal set; }
    public int AttackFlying { get; internal set; }
    public int Parry { get; internal set; }
    public int ParryFlying { get; internal set; }
    public double Gs { get; internal set; }
    public double GsFlying { get; internal set; }
    /// <summary>
    /// The species of the pet.
    /// </summary>
    public string? Species
    {
        get => _species;
        internal set
        {
            _species = value;
            SetSpells(value);
        }
    }
    /// <summary>
    /// If its a flying pet or not.
    /// </summary>
    public bool IsFlying { get; internal set; }
    /// <summary>
    /// if pet is a mighty companion of not.
    /// </summary>
    public bool IsMightyCompanion { get; internal set; }
    /// <summary>
    /// the spells the pet already knows.
    /// </summary>
    public ReadOnlyCollection<PetSpell> KnownSpells => Knownspells.AsReadOnly();
    /// <summary>
    /// the spells that can be learned by the pet.
    /// </summary>
    public ReadOnlyCollection<PetSpell> SpellsAvailable { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
#pragma warning disable CS8618 // program files are corrupted if SpellsAvailable is null
    public Pet()
#pragma warning restore CS8618
    {
        _core.Pet = this;
        Readfile();
    }

    internal void Readfile()
    {
        if (_core.XmlDoc == null || !_core.XmlDoc.SelectSingleNode("//pet")!.HasChildNodes)
        {
            return;
        }

        Species = _core.XmlDoc.SelectSingleNode("//species")!.InnerText;
        IsFlying = _core.XmlDoc.SelectSingleNode("//flying")!.InnerText == "True";
        IsMightyCompanion = _core.XmlDoc.SelectSingleNode("//mightyCompanion")!.InnerText == "True";
        Rkw = int.Parse(_core.XmlDoc.SelectSingleNode("//rkp")!.InnerText);
        Ae = int.Parse(_core.XmlDoc.SelectSingleNode("//ae")!.InnerText);

        foreach (XmlNode attribute in _core.XmlDoc.GetElementsByTagName("attribute"))
        {
            PropertyInfo[] p = GetAttribute(attribute!.Attributes!["name"]!.Value);
            p[0].SetValue(this, int.Parse(attribute.Attributes["current"]!.Value));
            p[1].SetValue(this, int.Parse(attribute.Attributes["start"]!.Value));
        }

        List<PetSpell> knownSpells = [];

        knownSpells.AddRange(from XmlNode spell in _core.XmlDoc.SelectNodes("//pet/spells/spell")!
            select SpellsAvailable.Single(p => p.Name == spell.InnerText));

        Knownspells = knownSpells;
    }

    /// <summary>
    /// Add a pet form <see cref="PetGenerator"/> or a save file.
    /// </summary>
    /// <param name="pet">An instance of <see cref="PetData"/></param>
    /// <param name="isMightyCompanion">if pet is a mighty companion of not.</param>
    /// <param name="attributes">the current attributes the pet has.</param>
    public void AddPet(PetData pet, bool isMightyCompanion, int[] attributes)
    {
        Species = pet.Species;
        IsMightyCompanion = isMightyCompanion;
        IsFlying = pet.IsFlying;
        Type t = typeof(PetData);
        for (int i = 0; i < 11; i++)
        {
            PropertyInfo[] p = GetAttribute(AttributeStrings[i]);
            p[0].SetValue(this, attributes[i]);
            if (AttributeStrings[i] != "AE")
            {
                p[1].SetValue(this, attributes[i]);
            }
        }
        for (int i = 11; i < 18; i++)
        {
            PropertyInfo[] p = GetAttribute(AttributeStrings[i]);


            object dataValue = t.GetProperty(AttributeStrings[i] + "Start")!.GetValue(pet)!;


            p[0].SetValue(this, dataValue);
            p[1].SetValue(this, dataValue);
        }
        LeP = Le;
        AsP = Ae;
        AuP = Au;
        Rkw = 3;
        LearnSpell("Zwiegespräch");
        if (pet.Species == "Kröte")
        {
            LearnSpell("Krötenschlag");
        }
        _core.HasPet = true;
        _core.FileChanged = true;
    }

    /// <summary>
    /// Increase the attribute of a pet
    /// </summary>
    /// <param name="attribute">Name of the attribute.</param>
    public void IncreaseAttribute(string attribute)
    {
        PropertyInfo[] p = GetAttribute(attribute);
        int currentValue = Convert.ToInt32(p[0].GetValue(this));
        currentValue++;
        if (attribute != "RKW" && attribute != "AE")
        {
            int maxValue = Convert.ToInt32(Convert.ToInt32(p[1].GetValue(this)) * 1.5);

            if (currentValue <= maxValue)
            {
                p[0].SetValue(this, currentValue);
            }
            else
            {
                throw new Exception("Maximum reached");
            }
        }
        else
        {
            p[0].SetValue(this, currentValue);
        }
        _core.FileChanged = true;
    }

    /// <summary>
    /// Add a spell.
    /// </summary>
    /// <param name="name">Name of the spell</param>
    public void LearnSpell(string name)
    {
        Knownspells.Add(SpellsAvailable.Single(p => p.Name == name));
    }

    public void ResetAuP()
    {
        AuP = Au;
        _core.FileChanged = true;
    }

    public void ResetLeP()
    {
        LeP = Le;
        _core.FileChanged = true;
    }

    public void ResetAsP()
    {
        AsP = Ae;
        _core.FileChanged = true;
    }

    public (int, int[], string) RollSpell(PetSpell spell)
    {
        return GetResult(spell.Attributes, Rkw, 0);
    }

    private (int, int[], string) GetResult(string[] attributes, int value, int mod)
    {
        int[] attrinuteValues = new int[3];

        for (int i = 0; i < 3; i++)
        {
            attrinuteValues[i] = attributes[i] switch
            {
                "MU" => Mu,
                "KL" => Kl,
                "IN" => In,
                "CH" => Ch,
                "FF" => Ff,
                "GE" => Ge,
                "KO" => Ko,
                "KK" => Kk,
                _ => attrinuteValues[i]
            };
        }

        return DSA.TaP(attrinuteValues, value, mod);

    }

    private void SetSpells(string? species)
    {
        List<PetSpell> petSpells = [];

        if (species != null)
        {
            petSpells.AddRange(Aventurien.Zauber.VertrautenzauberTier(species).Select(zauber => new PetSpell
            {
                Name = zauber.Name,
                Attributes = zauber.Eigenschaften.Split('/'),
                Characteristics = zauber.MerkmaleToString()
            }));
        }

        SpellsAvailable = petSpells.AsReadOnly();
    }

    /// <summary>
    /// Resets the instance of this class. (only used by <see cref="Core.ResetTool"/>.)
    /// </summary>
    internal void ResetTool()
    {
        foreach (string attribute in AttributeStrings)
        {
            PropertyInfo[] p = GetAttribute(attribute);
            p[0].SetValue(this, 0);
            if (attribute != "AE")
            {
                p[1].SetValue(this, 0);
            }
        }
        _species = null;
        IsFlying = false;
        IsMightyCompanion = false;
        Knownspells.Clear();
        SpellsAvailable = Array.Empty<PetSpell>().ToList().AsReadOnly();
        _core.HasPet = false;
    }

    internal PropertyInfo[] GetAttribute(string attribute)
    {

        PropertyInfo[] p = new PropertyInfo[2];
        Type t = typeof(Pet);
        p[0] = t.GetProperty(attribute, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)!;
        if (attribute != "RKW" && attribute != "AE")
        {
            p[1] = t.GetProperty(attribute + "start", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance)!;
        }
        return p;

    }
}
