using System.Collections.ObjectModel;

namespace Magehelper.Core
{
    /// <summary>
    /// The modification calculator
    /// </summary>
    public class Mod
    {
        private int mod_erleichterung = 0;
        private decimal mod_erschwerniss = 0;
        private readonly string text_bor = "nur bei Kenntnis der gildenmagischen Repräsentation";
        private readonly string text_dru = "AsP Kosten für Erzwingen halbiert (min. 1 AsP)";
        private readonly string text_geo = "wenn Zauber Merkmal des passenden Elements hat (Brobim-Geoden: Geister)";
        private readonly string text_ach = "ohne passenden geschliffenen Edelstein";
        private readonly string text_srl = "nur Illusionszauber";
        private readonly string text_halving = "gesamt Zuschlag wird halbiert";
        private bool halvingMod = false;
        private readonly string[] mod_names = new string[]
        {
           "Veränderte Technik",
           "Veränderte Technik, zentral",
           "Halbierte Zauberdauer",
           "Kosten einsparen",
           "unfreiwilliges statt freiwilliges Zielobjekt",
           "Freiwillig statt unfreiwillig",
           "Verdoppelte Zauberdauer",
           "Mehrere Gefährten verzaubern (freiwillig)",
           "Vergrößerung von Reichweite o. Wirkungsradius",
           "Verkleinerung der Reichweite o. Wirkungsradius",
           "Verdopplung der Wirkungsdauer",
           "Halbierung der Wirkungsdauer",
           "Änderung von Aufrechterhalten auf feste Dauer",
           "Erzwingen"
        };
        private readonly dynamic[][] mod_base = new dynamic[][]
        {
            new dynamic[]{"+7 / Komponente",7 },
            new dynamic[]{"+12 / Komponente",12 },
            new dynamic[]{"+5 / Halbierung",5 },
            new dynamic[]{"+3 / -10% AsP",3 },
            new dynamic[]{"+5",5 },
            new dynamic[]{"+2",7 },
            new dynamic[]{"-3",-3 },
            new dynamic[]{"+3",3 },
            new dynamic[]{"+5 / Stufe",5 },
            new dynamic[]{"+3 / Stufe",3 },
            new dynamic[]{"+7",7 },
            new dynamic[]{"+3",3 },
            new dynamic[]{"+7",7 },
            new dynamic[]{"-1 pro 1/2/4/8/... AsP",-1}
        };
        /// <summary>
        /// the MR to use.
        /// </summary>
        public int MR { get; set; }
        /// <summary>
        /// THe current representation thats been used
        /// </summary>
        public string? CurrentRepresentation { get; private set; }
        /// <summary>
        /// The info text thats been displayed in GUI.
        /// </summary>
        public string? InfoText { get; private set; }
        /// <summary>
        /// If MR is used in calculation or not.
        /// </summary>
        public bool UseMr { get; set; }
        /// <summary>
        /// The names of the representations.
        /// </summary>
        public ReadOnlyCollection<string> Representations { get; } = new string[]
        {
            "Borbarad",
            "Gildenmagier",
            "Scharlatan",
            "Druide",
            "Elf",
            "Geode",
            "Kristallomant",
            "sonstige"
        }.ToList().AsReadOnly();
        /// <summary>
        /// The modification data thats been used. (depends on representation)
        /// </summary>
        public ReadOnlyCollection<Modification>? Data { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Mod()
        {
            SetRepresentation("sonstige");
        }

        /// <summary>
        /// Reset the calculator.
        /// </summary>
        public void Reset()
        {
            mod_erleichterung = 0;
            mod_erschwerniss = 0;
            MR = 0;
            UseMr = false;
        }

        /// <summary>
        /// Sets the representation and the data for it.
        /// </summary>
        /// <param name="representation">Name of the chosen representation.</param>
        public void SetRepresentation(string representation)
        {
            dynamic[][] mod_data = mod_base;
            List<Modification> modifications = new List<Modification>();
            CurrentRepresentation = representation;
            InfoText = string.Empty;
            switch (representation)
            {
                case "Borbarad":
                    halvingMod = true;
                    InfoText = text_bor + ", " + text_halving;
                    mod_data[6] = new dynamic[] { "-4", -4 };
                    break;
                case "Gildenmagier":
                    halvingMod = true;
                    InfoText = text_halving;
                    mod_data[6] = new dynamic[] { "-4", -4 };
                    break;
                case "Scharlatan":
                    halvingMod = true;
                    InfoText = text_srl + ", " + text_halving;
                    break;
                case "Druide":
                    halvingMod = false;
                    InfoText = text_dru;
                    break;
                case "Elf":
                    halvingMod = false;
                    mod_data[10] = new dynamic[] { "+4", 4 };
                    break;
                case "Geode":
                    halvingMod = false;
                    InfoText = text_geo;
                    mod_data[3] = new dynamic[] { "+1 / -10% AsP", 1 };
                    mod_data[8] = new dynamic[] { "+3 / Stufe", 3 };
                    mod_data[9] = new dynamic[] { "+1 / Stufe", 1 };
                    mod_data[10] = new dynamic[] { "+5", 5 };
                    mod_data[11] = new dynamic[] { "+1", 1 };
                    break;
                case "Kristallomant":
                    halvingMod = false;
                    InfoText = text_ach;
                    mod_data = new dynamic[][]
                    {
                        new dynamic[]{"+14 / Komponente",14 },
                        new dynamic[]{"+24 / Komponente",24 },
                        new dynamic[]{"+10 / Halbierung",10 },
                        new dynamic[]{"+6 / -10% AsP",6 },
                        new dynamic[]{"+10",10 },
                        new dynamic[]{"+4",4 },
                        new dynamic[]{"-3",-3 },
                        new dynamic[]{"+6",6 },
                        new dynamic[]{"+10 / Stufe",10 },
                        new dynamic[]{"+6 / Stufe",6 },
                        new dynamic[]{"+14",14 },
                        new dynamic[]{"+6",6 },
                        new dynamic[]{"+14",14 },
                        new dynamic[]{"-1 pro 2/4/8/16/... AsP",-1}
                    };
                    break;
                default:
                    halvingMod = false;
                    break;
            }
            for (int i = 0; i < 14; i++)
            {
                modifications.Add(new Modification(mod_names[i], mod_data[i][0], mod_data[i][1]));
            }
            Data = modifications.AsReadOnly();
        }

        public void Add(int mod)
        {
            if (mod < 0)
            {
                mod_erleichterung += mod;
            }
            else
            {
                mod_erschwerniss += mod;
            }
        }

        public void Remove(int mod)
        {
            if (mod < 0)
            {
                mod_erleichterung -= mod;
            }
            else
            {
                mod_erschwerniss -= mod;
            }
        }

        public int Calculate()
        {
            int mod_gesamt;
            if (halvingMod)
            {
                mod_gesamt = (int)Math.Ceiling(mod_erschwerniss / 2 + mod_erleichterung);
            }
            else
            {
                mod_gesamt = (int)mod_erschwerniss + mod_erleichterung;
            }
            if (UseMr)
            {
                mod_gesamt += MR;
            }
            return mod_gesamt;
        }
    }
}