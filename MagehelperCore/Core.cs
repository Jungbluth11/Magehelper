using System.Reflection;

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
        XmlDocument xmlDoc = new();
        xmlDoc.LoadXml(xml);

        try
        {
            XmlNode root = xmlDoc.SelectSingleNode("magehelper")!;

            return root.Attributes!["versionCreated"] == null ? "0" : root.Attributes["versionCreated"]!.Value;
        }
        catch
        {
            throw new(path + " is not a valid magehelper file");
        }
    }

    /// <summary>
    ///     Reads a saved file.
    /// </summary>
    /// <param name="path"></param>
    public void ReadFileVersionSelector(string path)
    {
        string xml = File.ReadAllText(path);

        try
        {
            XmlDocument xmlDoc = new();
            xmlDoc.LoadXml(xml);
            bool isLegacy = false;
            string version = GetFileVersion(path);

            if (version == "0")
            {
                isLegacy = true;
            }

            ResetTool();

            if (isLegacy)
            {
                ReadFileLegacy(xmlDoc);
            }
            else
            {
                ReadFile(xmlDoc);
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
        FileName = string.Empty;
        HasSpellStorage = false;
        HasFlameSword = false;
        FileAup = false;
        FileLep = false;
        FileAsp = false;
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
            xw.WriteAttributeString("linkedCharacter", null);
            xw.WriteAttributeString("characterType", null);
        }
        else
        {
            xw.WriteAttributeString("linkedCharacter", Character.LinkedCharacter);
            xw.WriteAttributeString("characterType", Character.LinkedCharacterType.ToString());
        }

        xw.WriteEndElement();
        xw.WriteStartElement("artifacts");

        foreach (TraditionArtifact? artifact in artifacts)
        {
            if (artifact == null)
            {
                continue;
            }

            xw.WriteStartElement("artifact");
            xw.WriteStartElement("data");
            xw.WriteAttributeString("name", artifact.Name);
            xw.WriteAttributeString("boundSpells", artifact.BoundSpells.Count.ToString());

            //TODO
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
        xw.WriteEndElement();
        xw.WriteEndDocument();
        xw.Close();
        FileChanged = false;
    }

    private void ReadFile(XmlDocument xml)
    {
        TraditionArtifact?[] traditionArtifacts = [Bowl, BoneCub, CrystalBall, Staff, RingOfLife, ObsidianDagger];

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

        XmlNode? characterNode = xml.SelectSingleNode("//character");

        if (characterNode != null)
        {
            Character ??= new();

            Character.LinkedCharacterType = characterNode.Attributes!["characterType"]!.Value switch
            {
                "File" => Character.CharacterType.File,
                "HeldenSoftware" => Character.CharacterType.HeldenSoftware,
                _ => Character.CharacterType.None
            };

            if (Character.LinkedCharacterType != Character.CharacterType.None)
            {
                Character.LoadCharacter(characterNode.Attributes["linkedCharacter"]!.Value);
            }
        }


        for (int i = 0; i < traditionArtifacts.Length; i++)
        {
            TraditionArtifact traditionArtifact = traditionArtifacts[i]!;
            XmlNode? traditionArtifactNode = xml.SelectSingleNode("//artifact/data[@name='" + TraditionArtifactNames[i] + "']/..");

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
                        traditionArtifact = Bowl;

                        break;
                    case "Knochenkeule":
                        BoneCub = new();
                        traditionArtifact = BoneCub;

                        break;
                    case "Kristallkugel":
                        CrystalBall = new();
                        traditionArtifact = CrystalBall;

                        break;
                    case "Magierstab":
                        Staff = new();
                        traditionArtifact = Staff;

                        break;
                    case "Ring des Lebens":
                        RingOfLife = new();
                        traditionArtifact = RingOfLife;

                        break;
                    case "Vulkanglasdolch":
                        ObsidianDagger = new();
                        traditionArtifact = ObsidianDagger;

                        break;
                }
            }

            XmlAttributeCollection data = traditionArtifactNode.ChildNodes[0]!.Attributes!;

            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (traditionArtifact is Staff)
            {
                Staff!.Material = int.Parse(data["material"]!.Value);
                Staff.Length = int.Parse(data["length"]!.Value);
                Staff.Pasp = int.Parse(data["pasp"]!.Value);
                Staff.AfvTotal();
                Staff.HammerRkp = int.Parse(data["hammerRkp"]!.Value);
                Staff.IsFlameSwordFive = data["FlameSwordFive"]!.Value == "True";

                if ((traditionArtifactNode.ChildNodes[0] as XmlElement)!.HasAttribute("FlameSwordFour") &&
                    traditionArtifactNode.ChildNodes[0]!.Attributes!["FlameSwordFour"]!.Value == "True")
                {
                    Staff.LostPoints += 7;
                }
            }

            if (traditionArtifact is CrystalBall)
            {
                CrystalBall!.Material = (CrystalBallMaterial)int.Parse(data["material"]!.Value);
            }

            if (traditionArtifact is Bowl)
            {
                Bowl!.Material = (BowlMaterial)int.Parse(data["material"]!.Value);
            }

            traditionArtifact!.HasApport = data["apport"]!.Value == "True";

            foreach (XmlNode boundSpell in traditionArtifactNode.ChildNodes[1]!.ChildNodes)
            {
                if (traditionArtifact is Staff)
                {
                    string guid = boundSpell.Attributes!["guid"]!.Value;
                    string name = boundSpell.Attributes["name"]!.Value;
                    string characteristic = boundSpell.Attributes["characteristic"]!.Value;
                    int points = int.Parse(boundSpell.Attributes["points"]!.Value);
                    Staff!.AddSpell(name, characteristic, points, guid);
                }
                else
                {
                    string guid = boundSpell.Attributes!["guid"]!.Value;
                    string name = boundSpell.Attributes["name"]!.Value;
                    traditionArtifact.AddSpell(name, guid);
                }
            }
        }

        if (SpellStorage != null && xml.SelectSingleNode("//spellStorage")!.HasChildNodes)
        {
            List<int> spellStorages = [];

            spellStorages.AddRange(from XmlNode volume in xml.GetElementsByTagName("volume")
                                   select int.Parse(volume.InnerText));

            SpellStorage.EnableStorage(spellStorages);

            foreach (XmlNode spell in xml.SelectNodes("//spellStorage/spells/spell")!)
            {
                string guid = spell!.Attributes!["guid"]!.Value;
                string name = spell.Attributes["name"]!.Value;
                string characteristics = spell.Attributes["characteristics"]!.Value;
                string komplex = spell.Attributes["komplex"]!.Value;
                int cost = int.Parse(spell.Attributes["cost"]!.Value);
                int storage = int.Parse(spell.Attributes["storage"]!.Value);

                int? zfp = spell.Attributes["zfp"]!.Value == "null"
                    ? null
                    : int.Parse(spell.Attributes["zfp"]!.Value);

                SpellStorage.AddSpell(name, characteristics, komplex, cost, zfp, storage, guid);
            }
        }

        if (Pet != null && xml.SelectSingleNode("//pet")!.HasChildNodes)
        {
            Pet.Species = xml.SelectSingleNode("//species")!.InnerText;
            Pet.IsFlying = xml.SelectSingleNode("//flying")!.InnerText == "True";
            Pet.IsMightyCompanion = xml.SelectSingleNode("//mightyCompanion")!.InnerText == "True";
            Pet.Rkw = int.Parse(xml.SelectSingleNode("//rkp")!.InnerText);
            Pet.Ae = int.Parse(xml.SelectSingleNode("//ae")!.InnerText);

            foreach (XmlNode attribute in xml.GetElementsByTagName("attribute"))
            {
                PropertyInfo[] p = Pet.GetAttribute(attribute!.Attributes!["name"]!.Value);
                p[0].SetValue(Pet, int.Parse(attribute.Attributes["current"]!.Value));
                p[1].SetValue(Pet, int.Parse(attribute.Attributes["start"]!.Value));
            }

            List<PetSpell> knownSpells = [];

            knownSpells.AddRange(from XmlNode spell in xml.SelectNodes("//pet/spells/spell")!
                                 select Pet.SpellsAvailable.Single(p => p.Name == spell.InnerText));

            Pet.Knownspells = knownSpells;
        }

        if (Timers == null || !xml.SelectSingleNode("//timers")!.HasChildNodes)
        {
            return;
        }

        {
            foreach (XmlNode timer in xml.GetElementsByTagName("timer"))
            {
                string guid = timer!.Attributes!["guid"]!.Value;
                string text = timer.Attributes!["text"]!.Value;
                int duration = int.Parse(timer.Attributes!["duration"]!.Value);
                Timers.Add(text, duration, guid);
            }
        }
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
