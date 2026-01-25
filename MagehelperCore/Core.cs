using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace Magehelper.Core;

public class Core
{
    private static Core? _instance;
    /// <summary>
    ///     Names of the artifacts.
    /// </summary>
    public ReadOnlyCollection<string> TraditionArtifactNames => new[]
    {
        "Alchemistenschale",
        "Knochenkeule",
        "Kristallkugel",
        "Magierstab",
        "Ring des Lebens",
        "Vulkanglasdolch"
    }.ToList().AsReadOnly();

    /// <summary>
    ///     Base path of the application.
    /// </summary>
    public string BasePath { get; }

    /// <summary>
    ///     Current instance of <see cref="Magehelper.Core.BoneCub" />.
    /// </summary>
    public BoneCub? BoneCub { get; set; }

    /// <summary>
    ///     Current instance of <see cref="Magehelper.Core.Bowl" />.
    /// </summary>
    public Bowl? Bowl { get; set; }

    /// <summary>
    ///     Current instance of <see cref="Magehelper.Core.CrystalBall" />.
    /// </summary>
    public CrystalBall? CrystalBall { get; set; }

    private bool _fileChanged;

    /// <summary>
    ///     Has the application changed?
    /// </summary>
    public bool FileChanged
    {
        get => _fileChanged;
        internal set
        {
            _fileChanged = value;
            OnFileChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    ///     File name of the loaded save or name to create these.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Tabs that loaded for this file.
    /// </summary>
    public ObservableCollection<string> FileTabs { get; } = [];

    /// <summary>
    ///     Has the application currently a flame sword.
    /// </summary>
    public bool HasFlameSword { get; internal set; }

    /// <summary>
    ///     Has the application currently a pet.
    /// </summary>
    public bool HasPet { get; internal set; }

    /// <summary>
    ///     Has the application currently a spell storage.
    /// </summary>
    public bool HasSpellStorage { get; internal set; }

    /// <summary>
    ///     Last Version with changes of file structure/>
    /// </summary>
    public string MagehelperFileVersion => "4.0.0";

    /// <summary>
    ///     Current instance of <see cref="Magehelper.Core.ObsidianDagger" />.
    /// </summary>
    public ObsidianDagger? ObsidianDagger { get; set; }

    /// <summary>
    ///     Current instance of <see cref="Magehelper.Core.RingOfLife" />.
    /// </summary>
    public RingOfLife? RingOfLife { get; set; }

    /// <summary>
    ///     Settings path that be used
    /// </summary>
    public string SettingsPath { get; set; }

    /// <summary>
    ///     Maximum of Points to be used in <see cref="Magehelper.Core.SpellStorage" />
    /// </summary>
    public int SpellStoragePoints { get; set; }

    /// <summary>
    ///     Current instance of <see cref="Magehelper.Core.Staff" />.
    /// </summary>
    public Staff? Staff { get; set; }

    /// <summary>
    ///     if spell names of "Heldentool" are used of not. (default false)
    /// </summary>
    public bool UseHeldentoolNames { get; set; } = false;
    /// <summary>
    ///     Current instance of <see cref="Magehelper.Core.Character" />.
    /// </summary>
    public Character? Character { get; internal set; }
    internal bool FileAsp { get; set; }
    internal int FileAspValue { get; set; }
    internal bool FileAup { get; set; }
    internal int FileAupValue { get; set; }
    internal bool FileLep { get; set; }
    internal int FileLepValue { get; set; }
    internal FlameSword? FlameSword { get; set; }
    public Pet? Pet { get; internal set; }
    public SpellStorage? SpellStorage { get; internal set; }
    public Timers? Timers { get; internal set; }
    public Artifacts? Artifacts { get; set; }
    public ArcaneGlyphs? ArcaneGlyphs { get; set; }

    public delegate void FileChangedHandler(object sender, EventArgs e);

    public event FileChangedHandler? OnFileChanged;

    private Core()
    {
        BasePath = AppContext.BaseDirectory;
#if DEBUG
        SettingsPath = Path.Combine(BasePath, "BaseSettings");
#endif
    }

    public static Core GetInstance()
    {
        _instance ??= new();

        return _instance;
    }

    /// <summary>
    ///     Gets the Version of a Magehelper File
    /// </summary>
    /// <param name="path"> Path to the File</param>
    /// <returns></returns>
    public string GetFileVersion(string path)
    {
        string xml = File.ReadAllText(path);
        XmlDoc = new();
        XmlDoc.LoadXml(xml);

        try
        {
            return GetFileVersion();
        }
        catch
        {
            throw new(path + " is not a valid magehelper file");
        }
    }

    internal string GetFileVersion()
    {
        XmlNode root = XmlDoc!.SelectSingleNode("magehelper")!;
        return root.Attributes!["versionCreated"] == null ? "0" : root.Attributes["versionCreated"]!.Value;
    }

    internal XmlDocument? XmlDoc { get; private set; }

    /// <summary>
    ///     Reads a saved file.
    /// </summary>
    /// <param name="path"></param>
    public void ReadFileVersionSelector(string path)
    {

        try
        {
            ResetTool();

            if (XmlDoc == null)
            {
                string xml = File.ReadAllText(path);
                XmlDoc = new();
                XmlDoc.LoadXml(xml);
            }

            bool isLegacy = false;
            string version = GetFileVersion();

            if (version == "0")
            {
                isLegacy = true;
            }

            if (isLegacy)
            {
                ReadFileLegacy(XmlDoc);
            }
            else
            {
                ReadFile(XmlDoc);
            }

            FileName = path;
        }
        catch (Exception)
        {
            ResetTool();

            throw;
        }
        finally
        {
            FileChanged = false;
        }
    }

    /// <summary>
    ///     Resets the entire tool.
    /// </summary>
    public void ResetTool()
    {
        Staff?.ResetTool();
        CrystalBall?.ResetTool();
        Bowl?.ResetTool();
        BoneCub?.ResetTool();
        RingOfLife?.ResetTool();
        ObsidianDagger?.ResetTool();
        SpellStorage?.ResetTool();
        FlameSword?.ResetTool();
        Character?.ResetTool();
        Pet?.ResetTool();
        Timers?.RemoveAll();
        Artifacts?.DeleteAll();
        ArcaneGlyphs?.RemoveAll();
        FileName = string.Empty;
        HasSpellStorage = false;
        HasFlameSword = false;
        FileAup = false;
        FileLep = false;
        FileAsp = false;
        FileTabs.CollectionChanged -= FileTabs_CollectionChanged;
        FileTabs.Clear();
        XmlDoc = null;
        FileChanged = false;
    }

    /// <summary>
    ///     Write a save file.
    /// </summary>
    public void WriteFile()
    {
        TraditionArtifact?[] artifacts = [Bowl, BoneCub, CrystalBall, Staff, RingOfLife, ObsidianDagger];

        using XmlWriter xw = XmlWriter.Create(FileName);

        xw.WriteStartDocument();
        xw.WriteStartElement("magehelper");
        xw.WriteAttributeString("versionCreated", MagehelperFileVersion);
        xw.WriteElementString("tabs", JsonSerializer.Serialize(FileTabs));

        if (FileAup)
        {
            xw.WriteElementString("aup", FileAupValue.ToString());
        }
        else
        {
            xw.WriteElementString("aup", null);
        }

        if (FileLep)
        {
            xw.WriteElementString("lep", FileLepValue.ToString());
        }
        else
        {
            xw.WriteElementString("lep", null);
        }

        if (FileAsp)
        {
            xw.WriteElementString("asp", FileAspValue.ToString());
        }
        else
        {
            xw.WriteElementString("asp", null);
        }

        xw.WriteStartElement("character");

        if (Character == null || Character.LinkedCharacterType == Character.CharacterType.None)
        {
            xw.WriteAttributeString("linkedCharacter", "null");
            xw.WriteAttributeString("characterType", "null");
        }
        else
        {
            xw.WriteAttributeString("linkedCharacter", Character.LinkedCharacter);
            xw.WriteAttributeString("characterType", Character.LinkedCharacterType.ToString());
        }

        xw.WriteEndElement();
        xw.WriteStartElement("traditionArtifacts");

        foreach (TraditionArtifact? artifact in artifacts)
        {
            if (artifact == null)
            {
                continue;
            }

            xw.WriteStartElement("traditionArtifact");
            xw.WriteStartElement("data");
            xw.WriteAttributeString("name", artifact.Name);
            xw.WriteAttributeString("boundSpells", artifact.BoundSpells.Count.ToString());

            if (artifact.Name is "Kristallkugel" or "Ring des Lebens" or "Vulkanglasdolch")
            {
                xw.WriteAttributeString("maxSpells", (artifact as IMaxSpellArtifact)?.MaxSpells.ToString());
            }

            switch (artifact)
            {
                case Staff staff:
                    xw.WriteAttributeString("material", staff.Material.ToString());
                    xw.WriteAttributeString("length", staff.Length.ToString());
                    xw.WriteAttributeString("pasp", staff.Pasp.ToString());
                    xw.WriteAttributeString("hammerRkp", staff.HammerRkp.ToString());
                    xw.WriteAttributeString("FlameSwordFive", staff.IsFlameSwordFive.ToString());
                    xw.WriteAttributeString("lostPoints", staff.LostPoints.ToString());
                    xw.WriteAttributeString("reptileSkinVariant", staff.ReptileSkinVariant);

                    break;
                case CrystalBall crystalBall:
                    xw.WriteAttributeString("material", ((int)crystalBall.Material).ToString());

                    break;
                case Bowl bowl:
                    xw.WriteAttributeString("material", ((int)bowl.Material).ToString());
                    break;
            }

            xw.WriteAttributeString("apport", artifact.HasApport.ToString());
            xw.WriteEndElement();
            xw.WriteStartElement("boundSpells");

            foreach (ArtifactSpell boundSpell in artifact.BoundSpells)
            {
                xw.WriteStartElement("boundSpell");
                xw.WriteAttributeString("guid", boundSpell.Guid);
                xw.WriteAttributeString("name", boundSpell.Name);
                xw.WriteAttributeString("characteristic", boundSpell.Characteristic);
                xw.WriteAttributeString("points", boundSpell.Points.ToString());
                xw.WriteEndElement();
            }

            xw.WriteEndElement();
            xw.WriteEndElement();
        }

        xw.WriteEndElement();
        xw.WriteStartElement("spellStorage");

        if (HasSpellStorage && SpellStorage != null)
        {
            xw.WriteStartElement("spells");

            foreach (StoragedSpell spell in SpellStorage.Spells)
            {
                xw.WriteStartElement("spell");
                xw.WriteAttributeString("name", spell.Name);
                xw.WriteAttributeString("characteristics", spell.Characteristics);
                xw.WriteAttributeString("komplex", spell.Komplex);
                xw.WriteAttributeString("cost", spell.Cost.ToString());
                xw.WriteAttributeString("zfp", spell.Zfp.ToString());
                xw.WriteAttributeString("storage", spell.Storage.ToString());
                xw.WriteAttributeString("guid", spell.Guid);
                xw.WriteEndElement();
            }

            xw.WriteEndElement();
            xw.WriteStartElement("storageVolume");

            foreach (int volume in SpellStorage.PointsTotal)
            {
                xw.WriteElementString("volume", volume.ToString());
            }

            xw.WriteEndElement();
        }

        xw.WriteEndElement();
        xw.WriteStartElement("pet");

        if (HasPet && Pet != null)
        {
            xw.WriteElementString("species", Pet.Species);
            xw.WriteElementString("flying", Pet.IsFlying.ToString());
            xw.WriteElementString("mightyCompanion", Pet.IsMightyCompanion.ToString());
            xw.WriteElementString("rkp", Pet.Rkw.ToString());
            xw.WriteElementString("ae", Pet.Ae.ToString());
            xw.WriteStartElement("attributes");

            foreach (string attribute in Pet.AttributeStrings)
            {
                if (attribute == "AE")
                {
                    continue;
                }

                PropertyInfo[] p = Pet.GetAttribute(attribute);
                xw.WriteStartElement("attribute");
                xw.WriteAttributeString("name", attribute.ToLower());
                xw.WriteAttributeString("current", p[0].GetValue(Pet)!.ToString());
                xw.WriteAttributeString("start", p[1].GetValue(Pet)!.ToString());
                xw.WriteEndElement();
            }

            xw.WriteEndElement();
            xw.WriteStartElement("spells");

            foreach (PetSpell spell in Pet.KnownSpells)
            {
                xw.WriteElementString("spell", spell.Name);
            }

            xw.WriteEndElement();
        }

        xw.WriteEndElement();
        xw.WriteStartElement("timers");

        if (Timers != null)
        {
            for (int i = 0; i < Timers.Count; i++)
            {
                xw.WriteStartElement("timer");
                xw.WriteAttributeString("text", Timers[i].Text);
                xw.WriteAttributeString("duration", Timers[i].Duration.ToString());
                xw.WriteAttributeString("guid", Timers[i].Guid);
                xw.WriteEndElement();
            }
        }

        xw.WriteEndElement();
        xw.WriteStartElement("artifacts");

        if (Artifacts != null)
        {
            foreach (Artifact artifact in Artifacts)
            {
                xw.WriteStartElement("artifact");
                xw.WriteAttributeString("guid", artifact.Guid);
                xw.WriteAttributeString("name", artifact.Name);
                xw.WriteAttributeString("description", artifact.Description);
                xw.WriteAttributeString("type", artifact.Type.ToString());
                xw.WriteAttributeString("interval", artifact.Interval == null ? "null" : artifact.Interval.ToString());
                xw.WriteAttributeString("currentCharges", artifact.CurrentCharges == null ? "null" : artifact.CurrentCharges.ToString());
                xw.WriteAttributeString("maxCharges", artifact.MaxCharges == null ? "null" : artifact.MaxCharges.ToString());
                xw.WriteEndElement();
            }
        }

        xw.WriteEndElement();
        xw.WriteStartElement("arcaneGlyphs");

        if (ArcaneGlyphs != null)
        {
            foreach (ArcaneGlyph glyph in ArcaneGlyphs)
            {
                xw.WriteStartElement("arcaneGlyph");
                xw.WriteAttributeString("guid", glyph.Guid);
                xw.WriteAttributeString("name", glyph.Name);
                xw.WriteAttributeString("appliedTo", glyph.AppliedTo);
                xw.WriteAttributeString("size", glyph.Size.ToString(CultureInfo.InvariantCulture));
                xw.WriteAttributeString("rkw", glyph.Rkw.ToString());
                xw.WriteAttributeString("rkp", glyph.Rkp.ToString());
                xw.WriteAttributeString("cost", glyph.Cost.ToString());
                xw.WriteAttributeString("duration", glyph.Duration == null ? "null" : glyph.Duration.ToString());
                xw.WriteAttributeString("remainingDuration", glyph.RemainingDuration == null ? "null" : glyph.RemainingDuration.ToString());
                xw.WriteStartElement("additionalGlyphs");
                foreach (AdditionalGlyph additionalGlyph in glyph.AdditionalGlyphs)
                {
                    xw.WriteStartElement("additionalGlyph");
                    xw.WriteAttributeString("name", additionalGlyph.Name);
                    xw.WriteAttributeString("value", additionalGlyph.Value ?? "null");
                    xw.WriteEndElement();
                }
                xw.WriteEndElement();
                xw.WriteEndElement();
            }
        }

        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndDocument();
        xw.Close();
        FileChanged = false;
    }

    private void ReadFile(XmlDocument xml)
    {
        TraditionArtifact?[] traditionArtifacts = [Bowl, BoneCub, CrystalBall, Staff, RingOfLife, ObsidianDagger];

        XmlNode? tabNode = xml.SelectSingleNode("//tabs");
        if (tabNode != null)
        {
            foreach (string tab in JsonSerializer.Deserialize<string[]>(tabNode.FirstChild!.Value!)!)
            {
                FileTabs.Add(tab);
            }
            FileTabs.CollectionChanged += FileTabs_CollectionChanged;
        }

        string? aup = xml.SelectSingleNode("//aup")!.Value;

        if (aup != null)
        {
            FileAup = true;
            FileAupValue = int.Parse(aup);
        }

        string? lep = xml.SelectSingleNode("//lep")!.Value;

        if (lep != null)
        {
            FileLep = true;
            FileLepValue = int.Parse(lep);
        }

        string? asp = xml.SelectSingleNode("//asp")!.Value;

        if (asp != null)
        {
            FileAsp = true;
            FileAspValue = int.Parse(asp);
        }

        Character?.Readfile();
        
        for (int i = 0; i < traditionArtifacts.Length; i++)
        {
            TraditionArtifact traditionArtifact = traditionArtifacts[i]!;
            string traditionArtifactNodeName = GetFileVersion() == "3.0.0" ? "artifact" : "traditionArtifact";
            XmlNode? traditionArtifactNode = xml.SelectSingleNode($"//{traditionArtifactNodeName}/data[@name='{TraditionArtifactNames[i]}']/..");

            if (traditionArtifactNode == null)
            {
                continue;
            }

            if (traditionArtifact is null)
            {
                switch (TraditionArtifactNames[i])
                {
                    case "Alchemistenschale":
                        Bowl = new();

                        break;
                    case "Knochenkeule":
                        BoneCub = new();

                        break;
                    case "Kristallkugel":
                        CrystalBall = new();

                        break;
                    case "Magierstab":
                        Staff = new();

                        break;
                    case "Ring des Lebens":
                        RingOfLife = new();

                        break;
                    case "Vulkanglasdolch":
                        ObsidianDagger = new();

                        break;
                }
            }
            else
            {
                switch (TraditionArtifactNames[i])
                {
                    case "Alchemistenschale":
                        Bowl!.Readfile();

                        break;
                    case "Knochenkeule":
                        BoneCub!.Readfile();

                        break;
                    case "Kristallkugel":
                        CrystalBall!.Readfile();

                        break;
                    case "Magierstab":
                        Staff!.Readfile();

                        break;
                    case "Ring des Lebens":
                        RingOfLife!.Readfile();

                        break;
                    case "Vulkanglasdolch":
                        ObsidianDagger!.Readfile();

                        break;
                }
            }
        }

        SpellStorage?.Readfile();
        Pet?.Readfile();
        Timers?.ReadFile();
        Artifacts?.Readfile();
        ArcaneGlyphs?.Readfile();
    }

    private void FileTabs_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        FileChanged = true;
    }

    private void ReadFileLegacy(XmlDocument xml)
    {
        string? aup = xml.SelectSingleNode("//aup")!.Value;

        if (aup != null)
        {
            FileAup = true;
            FileAupValue = int.Parse(aup);
        }

        string? lep = xml.SelectSingleNode("//lep")!.Value;

        if (lep != null)
        {
            FileLep = true;
            FileLepValue = int.Parse(lep);
        }

        string? asp = xml.SelectSingleNode("//asp")!.Value;

        if (asp != null)
        {
            FileAsp = true;
            FileAspValue = int.Parse(asp);
        }

        Staff = new()
        {
            Material = int.Parse(xml.SelectSingleNode("//material")!.InnerText),
            Length = int.Parse(xml.SelectSingleNode("//laenge")!.InnerText),
            Pasp = int.Parse(xml.SelectSingleNode("//pasp")!.InnerText)
        };

        Staff.AfvTotal();
        Staff.HammerRkp = int.Parse(xml.SelectSingleNode("//hammer_rkp")!.InnerText);
        Staff.LostPoints = xml.SelectSingleNode("//vier")?.Value == "True" ? 7 : 0;
        Staff.IsFlameSwordFive = xml.SelectSingleNode("//fuenf")!.Value == "True";
        Staff.HasApport = xml.SelectSingleNode("//apport")!.Value == "True";
        XmlNode staffspells = xml.SelectSingleNode("//stabzauber")!;

        foreach (XmlNode boundSpell in staffspells.ChildNodes)
        {
            string name = boundSpell!.Attributes!["name"]!.Value;
            string characteristic = boundSpell.Attributes!["merkmal"]!.Value;
            int points = int.Parse(boundSpell.Attributes!["volumenpunkte"]!.InnerText);
            Staff.AddSpell(name, characteristic, points);
        }

        if (SpellStorage != null && xml.SelectSingleNode("//zauberspeicher")!.HasChildNodes)
        {
            List<int> spellStorages = [];

            spellStorages.AddRange(from XmlNode volume in xml.GetElementsByTagName("formatierung")
                                   select int.Parse(volume.InnerText));

            SpellStorage.EnableStorage(spellStorages);

            foreach (XmlNode spell in xml.SelectNodes("//spellStorage/spells/spell")!)
            {
                string name = spell!.Attributes!["name"]!.Value;
                string characteristics = spell.Attributes!["merkmal"]!.Value;
                string komplex = spell.Attributes!["komplex"]!.Value;
                int cost = int.Parse(spell.Attributes!["kosten"]!.InnerText);
                int storage = int.Parse(spell.Attributes!["speicher"]!.InnerText);
                SpellStorage.AddSpell(name, characteristics, komplex, cost, null, storage);
            }

        }

        if (Timers != null && xml.SelectSingleNode("//timers")!.HasChildNodes)
        {
            foreach (XmlNode timer in xml.GetElementsByTagName("timer"))
            {
                string text = timer!.Attributes!["name"]!.Value;
                int duration = int.Parse(timer.Attributes!["kr"]!.InnerText);
                Timers.Add(text, duration);
            }
        }

        XmlNode filePet = xml.GetElementsByTagName("vertrautentier")[0]!;

        if (Pet == null || filePet is not { HasChildNodes: true })
        {
            return;
        }

        {
            XmlNode filePetData = xml.GetElementsByTagName("tier")[0]!;
            Pet.Species = filePetData.Attributes!["tierart"]!.Value;
            Pet.IsMightyCompanion = filePetData.Attributes!["machtvoll"]!.Value == "True";
            Pet.IsFlying = filePetData.Attributes["fliegend"]!.Value == "True";
            Pet.Rkw = int.Parse(filePetData.Attributes["rkp"]!.Value);

            foreach (XmlNode node in xml.GetElementsByTagName("werte")[0]!.ChildNodes)
            {
                switch (node!.Attributes!["name"]!.Value)
                {
                    case "Mut":
                        Pet.MuStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Mu = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "Klugheit":
                        Pet.KlStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Kl = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "Intuition":
                        Pet.InStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.In = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "Charisma":
                        Pet.ChStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Ch = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "Fingerfertigkeit":
                        Pet.FfStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Ff = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "Gewandtheit":
                        Pet.GeStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Ge = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "Konstitution":
                        Pet.KoStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Ko = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "Körperkraft":
                        Pet.KkStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Kk = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "MR":
                        Pet.MrStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Mr = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "LeP":
                        Pet.LeStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Le = int.Parse(node.Attributes["gesamtwert"]!.Value);
                        Pet.LeP = int.Parse(node.Attributes["aktuell"]!.Value);

                        break;
                    case "AuP":
                        Pet.AuStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Au = int.Parse(node.Attributes["gesamtwert"]!.Value);
                        Pet.AuP = int.Parse(node.Attributes["aktuell"]!.Value);

                        break;
                    case "AsP":
                        Pet.Ae = int.Parse(node.Attributes["gesamtwert"]!.Value);
                        Pet.AsP = int.Parse(node.Attributes["aktuell"]!.Value);

                        break;
                    case "attacke":
                        Pet.AttackStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Attack = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "attacke_luft":
                        Pet.AttackFlyingStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.AttackFlying = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "parade":
                        Pet.ParryStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Parry = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "parade_luft":
                        Pet.ParryFlyingStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.ParryFlying = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    case "gs":
                        Pet.GsStart = double.Parse(node.Attributes["startwert"]!.Value);
                        Pet.Gs = double.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                    default:
                        Pet.GsFlyingStart = int.Parse(node.Attributes["startwert"]!.Value);
                        Pet.GsFlying = int.Parse(node.Attributes["gesamtwert"]!.Value);

                        break;
                }
            }

            List<PetSpell> knownSpells = [];

            knownSpells.AddRange(from XmlNode spell in xml.GetElementsByTagName("vertrautenzauber")[0]!
                                 select Pet.SpellsAvailable.Single(p => p.Name == spell.InnerText));

            Pet.Knownspells = knownSpells;
        }
    }
}
