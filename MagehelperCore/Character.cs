using System.Collections.ObjectModel;
using System.Xml;
using DSAUtils;
using DSAUtils.HeldentoolInterop;
using DSAUtils.Settings.Aventurien;

namespace Magehelper.Core
{
    public class Character
    {
        private int au;
        private int le;
        private int ae;
        private readonly Core core;
        public int MU { get; set; }
        public int KL { get; set; }
        public int IN { get; set; }
        public int CH { get; set; }
        public int FF { get; set; }
        public int GE { get; set; }
        public int KO { get; set; }
        public int KK { get; set; }
        public int MR { get; set; }
        public int AuP { get; set; }
        public int LeP { get; set; }
        public int AsP { get; set; }
        public bool IsLoaded { get; private set; }
        /// <summary>
        /// The skills that character has.
        /// </summary>
        public ReadOnlyCollection<Ability>? Skills { get; private set; }
        /// <summary>
        /// The Spells this Character knows.
        /// </summary>
        public ReadOnlyCollection<CharacterSpell>? Spells { get; private set; }
        /// <summary>
        /// The Rituals this Character knows.
        /// </summary>
        public ReadOnlyCollection<CharacterRitual>? Rituals { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="core">An instance of <see cref="Core"/>.</param>
        public Character(Core core)
        {
            core.Character = this;
            this.core = core;
            if (core.FileAup)
            {
                AuP = core.FileAupValue;
            }
            if (core.FileLep)
            {
                LeP = core.FileLepValue;
            }
            if (core.FileAsp)
            {
                AsP = core.FileAspValue;
            }
        }

        /// <summary>
        /// Loads all characters that can be used.
        /// </summary>
        /// <returns>An array of <see cref="DSAUtils.HeldentoolInterop.Charakter"/></returns>
        /// <exception cref="FileNotFoundException"/>
        public Charakter[] GetCharactersFromTool()
        {
            if (HeldentoolInterop.IsInstalled())
            {
                return HeldentoolInterop.GetByAE();
            }
            throw new FileNotFoundException("\"Heldentool\" is not installed.");
        }

        /// <summary>
        /// Load an character.
        /// </summary>
        /// <param name="character">the chosen character</param>
        public void LoadCharacter(Charakter character)
        {
#pragma warning disable CS8602
            ResetTool();
            XmlDocument xml = new XmlDocument();
            List<CharacterSpell> spells = new List<CharacterSpell>();
            List<CharacterRitual> rituals = new List<CharacterRitual>();
            List<string> ritualList = new List<string>();
            ritualList.AddRange(Aventurien.Rituale.druidenritual);
            ritualList.AddRange(Aventurien.Rituale.durrodunritual);
            ritualList.AddRange(Aventurien.Rituale.elfenlied);
            ritualList.AddRange(Aventurien.Rituale.hexenfluch);
            ritualList.AddRange(Aventurien.Rituale.kristallomantenritual);
            ritualList.AddRange(Aventurien.Rituale.kugelzauber);
            ritualList.AddRange(Aventurien.Rituale.schalenzauber);
            ritualList.AddRange(Aventurien.Rituale.schamanenritual);
            ritualList.AddRange(Aventurien.Rituale.stabzauber);
            ritualList.AddRange(Aventurien.Rituale.trommelzauber);
            ritualList.AddRange(Aventurien.Rituale.zaubertanz);
            ritualList.AddRange(Aventurien.Rituale.zibiljaritual);
            xml.LoadXml(character.XML);
            Skills = character.Talente.ToList().AsReadOnly();
            foreach (Ability ability in character.Eigenschaften)
            {
                switch (ability.Name)
                {
                    case "Mut":
                        MU = ability.Wert;
                        break;
                    case "Klugheit":
                        KL = ability.Wert;
                        break;
                    case "Intuition":
                        IN = ability.Wert;
                        break;
                    case "Charisma":
                        CH = ability.Wert;
                        break;
                    case "Fingerfertigkeit":
                        FF = ability.Wert;
                        break;
                    case "Gewandtheit":
                        GE = ability.Wert;
                        break;
                    case "Konstitution":
                        KO = ability.Wert;
                        break;
                    case "Körperkraft":
                        KK = ability.Wert;
                        break;
                    case "Magieresistenz":
                        MR = ability.Wert;
                        break;
                    case "Ausdauer":
                        au = ability.Wert;
                        if (!core.FileAup)
                        {
                            AuP = ability.Wert;
                        }
                        break;
                    case "Lebensenergie":
                        le = ability.Wert;
                        if (!core.FileLep)
                        {
                            LeP = ability.Wert;
                        }
                        break;
                    case "Astralenergie":
                        ae = ability.Wert;
                        if (!core.FileAsp)
                        {
                            AsP = ability.Wert;
                        }
                        break;
                    default:
                        break;
                }
            }
            foreach (XmlNode node in xml.GetElementsByTagName("zauber"))
            {
                string[] spell = new string[4];
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    if (attribute.Name == "name")
                    {
                        spell[0] = attribute.Value;
                    }
                    else if (attribute.Name == "variante" && !string.IsNullOrEmpty(attribute.Value))
                    {
                        spell[1] += " (" + attribute.Value + ")";
                    }
                    else if (attribute.Name == "repraesentation")
                    {
                        spell[2] = attribute.Value;
                    }
                    else if (attribute.Name == "value")
                    {
                        spell[3] = attribute.Value;
                    }
                }
                if (spell[0] != null)
                {
                    Zauber spellData;
                    if (spell[2] == "Magiedilletant")
                    {
                        spellData = Aventurien.Zauber.GetByName(spell[0]);
                    }
                    else
                    {
                        spellData = Aventurien.Zauber.GetByName(spell[0], Aventurien.Zauber.RepToEnum(spell[2]));
                    }
                    string[] attributes = spellData.Eigenschaften.Split('/');
                    spells.Add(new CharacterSpell { Name = spell[0] + spell[1], Representation = spell[2], Attributes = attributes, Komplex = spellData.Komplexitaet, Characteristics = spellData.MerkmaleToString(), Value = int.Parse(spell[3]) });
                }
            }
            foreach (string sf in character.Sonderfertigkeiten)
            {
                foreach (string ritual in ritualList)
                {
                    if (HeldentoolInterop.Rename(sf, Name.Offi) == ritual)
                    {
                        string ritualName = ritual;
                        string ritualAttributes = string.Empty;
                        string ritualSkill;
                        int[] ritualMod = Array.Empty<int>();
                        RitualType ritualType = RitualType.Ritual;
                        dynamic ritualData = Aventurien.Rituale.GetByName(ritualName);
                        if (!core.UseHeldentoolNames)
                        {
                            ritualName = ((IRitual)ritualData).Name;
                        }
                        if (ritualData is Schamanenritual s)
                        {
                            ritualSkill = s.Ritualkenntnis;
                            ritualType = RitualType.Shaman;
                        }
                        else if (ritualData is Trommelzauber t)
                        {
                            ritualAttributes = t.Ritualprobe;
                            ritualSkill = t.Ritualprobe;
                            ritualMod = new int[] { t.Musizierenprobe, t.RitualErschwernis };
                            ritualType = RitualType.Drum;
                        }
                        else
                        {
                            ritualAttributes = ((Ritual)ritualData).Ritualprobe;
                            ritualSkill = ((Ritual)ritualData).Ritualkenntnis;
                            ritualMod = new int[] { ((Ritual)ritualData).RitualErschwernis };
                        }
                        rituals.Add(new CharacterRitual { Name = ritualName, Attributes = ritualAttributes, Skill = ritualSkill, Mod = ritualMod, Type = ritualType });
                    }
                }
            }
            Spells = spells.AsReadOnly();
            Rituals = rituals.AsReadOnly();
#pragma warning restore CS8602
        }

        /// <summary>
        /// Resets the instance of this class. (Only used from <see cref="Core.ResetTool"/>.)
        /// </summary>
        internal void ResetTool()
        {
            au = 0;
            le = 0;
            ae = 0;
            MU = 0;
            KL = 0;
            IN = 0;
            CH = 0;
            FF = 0;
            GE = 0;
            KO = 8;
            KK = 8;
            MR = 0;
            AuP = 0;
            LeP = 0;
            AsP = 0;
            Skills = null;
            Spells = null;
            Rituals = null;
        }

        public void ResetAuP()
        {
            AuP = au;
            core.FileChanged = true;
        }

        public void ResetLeP()
        {
            LeP = le;
            core.FileChanged = true;
        }

        public void ResetAsP()
        {
            AsP = ae;
            core.FileChanged = true;
        }

        public string[] RollSpell(CharacterSpell spell)
        {
            (int, string, string) result = GetResult(spell.Attributes, spell.Value, 0);
            return new string[] { result.Item1.ToString(), result.Item2, result.Item3 };
        }

        public string[] RollRitual(CharacterRitual ritual)
        {
            string[] returnData;
            if (ritual.Type == RitualType.Shaman)
            {
                (string[], int) skillData = GetSkillData(ritual.Skill);
                (int, string, string) result = GetResult(skillData.Item1, skillData.Item2, 0);
                returnData = new string[] { result.Item1.ToString(), result.Item2, result.Item3 };
            }
            else if (ritual.Type == RitualType.Drum)
            {
                (string[], int) skillData = GetSkillData("Musizieren");
                (int, string, string) skillResult = GetResult(skillData.Item1, skillData.Item2, ritual.Mod[0]);
                if (skillResult.Item1 >= 0 && !skillResult.Item3.Contains("Patzer"))
                {
                    int modvalue = ritual.Mod[1] - skillResult.Item1;
                    (string[], int) ritualData = GetSkillData("Ritualkenntnis: Derwisch");

                    (int, string, string) ritualResult = GetResult(ritual.Attributes.Split('/'), ritualData.Item2, modvalue);
                    returnData = new string[] { ritualResult.Item1.ToString(), ritualResult.Item2, ritualResult.Item3 };
                }
                else
                {
                    string failedText = "Misslungen (Musizieren)";
                    if (skillResult.Item3 != string.Empty)
                    {
                        failedText += "; " + skillResult.Item3;
                    }
                    returnData = new string[] { skillResult.Item1.ToString(), skillResult.Item2, failedText };
                }
            }
            else
            {
                string[] attributes;
                int value;
                if (Sonderfertigkeiten.GetByGroup(Sonderfertigkeitengruppe.Ritualkenntnis).Contains(ritual.Skill))
                {
                    attributes = ritual.Attributes.Split('/');
                    (string[], int) skillData = GetSkillData("Ritualkenntnis: " + ritual.Skill);
                    value = skillData.Item2;
                }
                else
                {
                    (string[], int) skillData = GetSkillData(ritual.Skill);
                    attributes = skillData.Item1;
                    value = skillData.Item2;
                }
                (int, string, string) result = GetResult(attributes, value, ritual.Mod[0]);
                returnData = new string[] { result.Item1.ToString(), result.Item2, result.Item3 };
            }
            return returnData;
        }

        private (int, string, string) GetResult(string[] attributes, int value, int mod)
        {
            int[] attrinuteValues = new int[3];
            string diceResults = string.Empty;
            for (int i = 0; i < 3; i++)
            {
                switch (attributes[i])
                {
                    case "MU":
                        attrinuteValues[i] = MU;
                        break;
                    case "KL":
                        attrinuteValues[i] = KL;
                        break;
                    case "IN":
                        attrinuteValues[i] = IN;
                        break;
                    case "CH":
                        attrinuteValues[i] = CH;
                        break;
                    case "FF":
                        attrinuteValues[i] = FF;
                        break;
                    case "GE":
                        attrinuteValues[i] = GE;
                        break;
                    case "KO":
                        attrinuteValues[i] = KO;
                        break;
                    case "KK":
                        attrinuteValues[i] = KK;
                        break;
                    default:
                        break;
                }
            }
            object[] result = DSA.TaP(attrinuteValues, value, mod);
#pragma warning disable CS8602
            for (int i = 0; i < 3; i++)
            {
                diceResults += (result[1] as dynamic[])[i].ToString();
                if (i < 2)
                {
                    diceResults += ", ";
                }
            }
            return ((int)result[0], diceResults, (result[1] as dynamic[])[3]);
#pragma warning restore CS8602
        }

        private (string[], int) GetSkillData(string skillname)
        {
            Talent skill = Aventurien.GetTalent(skillname);
            string[] attributes = skill.Eigenschaften.Split('/');
#pragma warning disable CS8604
            int value = Skills.Single(s => s.Name == skillname).Wert;
#pragma warning restore CS8604
            return (attributes, value);
        }
    }
}