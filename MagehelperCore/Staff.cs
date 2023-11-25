using DSAUtils.HeldentoolInterop;

namespace Magehelper.Core
{
    public class Staff : Artifact
    {
        private readonly List<string> merkmalsfoki = new List<string>();
        private int afvTotal;
        private int afvUsed;
        public int AfvRemain => HasApport ? 0 : afvTotal - afvUsed;
        /// <summary>
        /// Material of the staff.
        /// </summary>
        public int Material { get; set; }
        /// <summary>
        /// Length of the staff.
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Additional pAsP that spend to create the staff.
        /// </summary>
        public int Pasp { get; set; }
        /// <summary>
        /// RkP* of the spell "Hammer des Magus" (if exist).
        /// </summary>
        public int HammerRkp { get; set; }
        public bool IsFlameSwordFour { get; set; }
        public bool IsFlameSwordFive { get; set; }
        /// <summary>
        /// Names of the materials. (SRD 117ff)
        /// </summary>
        public static string[] MaterialStrings => new string[]
        {
            "Normal",
            "Blutulme",
            "Bosparanie",
            "Steineiche",
            "Eisenbaum",
            "Mohagoni",
            "Zyklopenzeder",
            "Raschtulszeder",
            "Mammutbaum",
            "Walnussbaum",
            "Baum der Reisenden"
        };
        /// <summary>
        /// Names of the length. (WdZ 108)
        /// </summary>
        public static string[] LengthStrings => new string[]
        {
            "Magierstab als Stab",
            "Magierstab m. Kristallkugel",
            "Magierstab (kurz)",
            "Magierstab (sehr kurz)"
        };
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="core">An instance of <see cref="Core"/>.</param>
        public Staff(Core core) : base(core, "staff.json", "Magierstab")
        {
            core.Staff = this;
        }

        public void AfvTotal()
        {
            decimal lenghtMod;
            decimal materialMod;
            decimal afv;
            switch (Length)
            {
                case 1:
                    lenghtMod = 27;
                    break;
                case 2:
                    lenghtMod = 18;
                    break;
                case 3:
                    lenghtMod = 15;
                    break;
                default:
                    lenghtMod = 24;
                    break;
            }
            switch (Material)
            {
                case 1:
                case 7:
                    materialMod = 1;
                    break;
                case 2:
                case 5:
                case 8:
                case 9:
                case 10:
                    materialMod = -1;
                    break;
                default:
                    materialMod = 0;
                    break;
            }
            if (!IsFlameSwordFive)
            {
                afv = lenghtMod + materialMod + Pasp / 3;
                if (IsFlameSwordFour)
                {
                    afv += -7;
                }
            }
            else
            {
                afv = 0;
            }
            if (HasApport)
            {
                afv = 0;
            }
            afvTotal = decimal.ToInt32(afv);
            core.FileChanged = true;
        }

        /// <summary>
        /// Adds an spells to this artifact.
        /// </summary>
        /// <param name="spellName">Name of the spell.</param>
        /// <param name="characteristic">Characteristic of the spell.</param>
        /// <param name="points">Points spend for the spell.</param>
        /// <param name="guid">GUID of the spell. (Only used by <see cref="Core.ReadFileVersionSelector"/>)</param>
        /// <returns>An instance of <see cref="ArtifactSpell"/> that contains data about the chosen spell.</returns>
        /// <exception cref="ArgumentException"/>
        /// /// <exception cref="InvalidOperationException"/>
        public ArtifactSpell AddSpell(string spellName, string? characteristic = null, int points = 0, string? guid = null)
        {
            try
            {
                ArtifactSpell spell = SpellsAvailable.Single(s => s.Name == spellName);
                if (points == 0)
                {
                    points = spell.Points;
                }
                switch (HeldentoolInterop.Rename(spellName, DSAUtils.HeldentoolInterop.Name.Offi))
                {
                    case "Merkmalsfokus":
                        characteristic = characteristic == null ? string.Empty : characteristic;
                        spell.Characteristic = characteristic;
                        merkmalsfoki.Add(characteristic);
                        spell.DisplayText = spellName + " (" + characteristic + ")";
                        break;
                    case "Hammer des Magus":
                        spell.DisplayText = spellName + " (3W6+" + HammerRkp.ToString() + ")";
                        break;
                    case "Doppeltes Maß":
                        spell.Points = points;
                        spell.DisplayText = spellName + " (" + points + " Volumenpunkte)";
                        break;
                    case "Zauberspeicher":
                        spell.Points = points;
                        break;
                    case "Flammenschwert":
                        core.HasFlameSword = true;
                        break;
                }
                switch (Material)
                {
                    case 4:
                        if (spellName == "Modifikationsfokus")
                        {
                            spell.Points--;
                        }
                        if (spellName == "Flammenschwert")
                        {
                            spell.Points++;
                        }
                        break;
                    case 6:
                        if (spell.Characteristic == "Antimagie")
                        {
                            spell.Points += -2;
                        }
                        break;
                    case 8:
                        int merkmale = 0;
                        foreach (string s in merkmalsfoki)
                        {
                            if (s.Equals("Einfluss") || s.Equals("Herrschaft"))
                            {
                                merkmale++;
                            }
                        }
                        if (merkmale == 2)
                        {
                            points--;
                        }
                        break;
                }
                afvUsed += points;
                return AddSpell(spell, guid);
            }
            catch (InvalidOperationException e)
            {
                if (e.Message == "Sequence contains no matching element")
                {
                    throw new ArgumentException("Spell doesn't exist", nameof(spellName));
                }
                else
                {
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Remove a spell with the given GUID from this artifact.
        /// </summary>
        /// <param name="guid">The GUID of the that should be removed.</param>
        /// <exception cref="ArgumentException"/>
        public new void RemoveSpell(string guid)
        {
            try
            {
                int points = 0;
                ArtifactSpell spell = boundSpells.Single(a => a.Guid == guid);
                points = spell.Points;
                if (spell.Name == "Merkmalsfokus")
                {
                    merkmalsfoki.Remove(spell.Characteristic);
                }
                else if (spell.Name == "Zauberspeicher")
                {
                    core.HasSpellStorage = false;
                }
                else if (spell.Name == "Flammenschwert")
                {
                    core.HasFlameSword = false;
                }
                if (Material == 8)
                {
                    int merkmale = 0;
                    foreach (string s in merkmalsfoki)
                    {
                        if (s.Equals("Einfluss") || s.Equals("Herrschaft"))
                        {
                            merkmale++;
                        }
                    }
                    if (merkmale == 2)
                    {
                        points--;
                    }
                }
                boundSpells.Remove(spell);
                afvUsed -= points;
                core.FileChanged = true;
            }
            catch
            {
                throw new ArgumentException("GUID doesn't exist", nameof(guid));
            }
        }

        internal new void ResetTool()
        {
            afvTotal = 24;
            afvUsed = 0;
            merkmalsfoki.Clear();
            boundSpells.Clear();
            Material = 0;
            Length = 0;
            Pasp = 0;
            HammerRkp = 0;
            IsFlameSwordFour = false;
            IsFlameSwordFive = false;
            HasApport = false;
        }
    }
}