using System.Collections.ObjectModel;
using System.Reflection;
using DSAUtils;
using DSAUtils.Settings.Aventurien;

namespace Magehelper.Core
{
    public class Pet
    {
        private string? _species;
        /// <summary>
        /// Names of the attributes.
        /// </summary>
        internal string[] AttributeStrings => new string[]
        {
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
        };
        internal int MUStart { get; set; }
        internal int KLStart { get; set; }
        internal int INStart { get; set; }
        internal int CHStart { get; set; }
        internal int FFStart { get; set; }
        internal int GEStart { get; set; }
        internal int KOStart { get; set; }
        internal int KKStart { get; set; }
        internal int LeStart { get; set; }
        internal int AuStart { get; set; }
        internal int MRStart { get; set; }
        internal int AttackStart { get; set; }
        internal int AttackFlyingStart { get; set; }
        internal int ParryStart { get; set; }
        internal int ParryFlyingStart { get; set; }
        internal double GSStart { get; set; }
        internal double GSFlyingStart { get; set; }
        internal Core Core { get; }
        /// <summary>
        /// the spells the pet already knows.
        /// </summary>
        internal List<PetSpell> Knownspells { get; set; } = new List<PetSpell>();
        public int LeP { get; set; }
        public int AuP { get; set; }
        public int AsP { get; set; }
        public int MU { get; set; }
        public int KL { get; set; }
        public int IN { get; set; }
        public int CH { get; set; }
        public int FF { get; set; }
        public int GE { get; set; }
        public int KO { get; set; }
        public int KK { get; set; }
        public int LE { get; internal set; }
        public int AE { get; internal set; }
        public int AU { get; internal set; }
        public int MR { get; set; }
        public int RKW { get; internal set; }
        public int Attack { get; internal set; }
        public int AttackFlying { get; internal set; }
        public int Parry { get; internal set; }
        public int ParryFlying { get; internal set; }
        public double GS { get; internal set; }
        public double GSFlying { get; internal set; }
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
        /// <param name="core">An instance of <see cref="Core"/>.</param>
#pragma warning disable CS8618
        // program files are corrupted if SpellsAvailable is null
        public Pet(Core core)
#pragma warning restore CS8618
        {
            Core = core;
            core.Pet = this;
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
#pragma warning disable CS8600
#pragma warning disable CS8602
                object dataValue = t.GetProperty(AttributeStrings[i] + "Start").GetValue(pet);
#pragma warning restore CS8600
#pragma warning restore CS8602
                p[0].SetValue(this, dataValue);
                p[1].SetValue(this, dataValue);
            }
            LeP = LE;
            AsP = AE;
            AuP = AU;
            RKW = 3;
            LearnSpell("Zwiegespräch");
            if (pet.Species == "Kröte")
            {
                LearnSpell("Krötenschlag");
            }
            Core.HasPet = true;
            Core.FileChanged = true;
        }

        /// <summary>
        /// Increase the attribute of a pet
        /// </summary>
        /// <param name="attribute">Name of the attribute.</param>
        public void IncreaseAttribute(string attribute)
        {
#pragma warning disable CS8605
            PropertyInfo[] p = GetAttribute(attribute);
            int currentValue = (int)p[0].GetValue(this);
            currentValue++;
            if (attribute != "RKW" && attribute != "AE")
            {
                int maxValue = (int)((double)p[1].GetValue(this) * 1.5);
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
            Core.FileChanged = true;
#pragma warning restore CS8605
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
            AuP = AU;
            Core.FileChanged = true;
        }

        public void ResetLeP()
        {
            LeP = LE;
            Core.FileChanged = true;
        }

        public void ResetAsP()
        {
            AsP = AE;
            Core.FileChanged = true;
        }

        public string[] RollSpell(PetSpell spell)
        {
            (int, string, string) result = GetResult(spell.Attributes, RKW, 0);
            return new string[] { result.Item1.ToString(), result.Item2, result.Item3 };
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

        private void SetSpells(string? species)
        {
            List<PetSpell> petSpells = new List<PetSpell>();
            foreach (Zauber zauber in Aventurien.Zauber.VertrautenzauberTier(species))
            {
                petSpells.Add(new PetSpell { Name = zauber.Name, Attributes = zauber.Eigenschaften.Split('/'), Characteristics = zauber.MerkmaleToString() });
            }
            SpellsAvailable = petSpells.AsReadOnly();
        }

        /// <summary>
        /// Resets the instance of this class. (Only used from <see cref="Core.ResetTool"/>.)
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
            SpellsAvailable = new PetSpell[0].ToList().AsReadOnly();
            Core.HasPet = false;
        }

        internal PropertyInfo[] GetAttribute(string attribute)
        {
#pragma warning disable CS8601
            PropertyInfo[] p = new PropertyInfo[2];
            Type t = typeof(Pet);
            p[0] = t.GetProperty(attribute, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (attribute != "RKW" && attribute != "AE")
            {
                p[1] = t.GetProperty(attribute + "start", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
            }
            return p;
#pragma warning restore CS8601
        }
    }
}