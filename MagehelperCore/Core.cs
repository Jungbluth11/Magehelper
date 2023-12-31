﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace Magehelper.Core
{
    public class Core
    {
        internal int FileAupValue { get; set; }
        internal int FileLepValue { get; set; }
        internal int FileAspValue { get; set; }
        internal bool FileAup { get; set; } = false;
        internal bool FileLep { get; set; } = false;
        internal bool FileAsp { get; set; } = false;
        internal FlameSword FlameSword { get; set; }
        internal SpellStorage SpellStorage { get; set; }
        internal Character Character { get; set; }
        internal Pet Pet { get; set; }
        internal Timers Timers { get; set; }
        /// <summary>
        /// Maximum of Points to be used in <see cref="Magehelper.Core.SpellStorage"/>
        /// </summary>
        public int SpellStoragePoints { get; set; }
        /// <summary>
        /// Base path of the application.
        /// </summary>
        public string BasePath { get; }
        /// <summary>
        /// Settings path that be used
        /// </summary>
        public string SettingsPath { get; set; }
        /// <summary>
        /// File name of the loaded save or name to create these.
        /// </summary>
        public string FileName { get; set; } = string.Empty;
        /// <summary>
        /// Has the application changed?
        /// </summary>
        public bool FileChanged { get; internal set; } = false;
        /// <summary>
        /// Has the application currently a spell storage.
        /// </summary>
        public bool HasSpellStorage { get; internal set; }
        /// <summary>
        /// Has the application currently a flame sword.
        /// </summary>
        public bool HasFlameSword { get; internal set; }
        /// <summary>
        /// Has the application currently a pet.
        /// </summary>
        public bool HasPet { get; internal set; }
        /// <summary>
        /// if spell names of "Heldentool" are used of not. (default false)
        /// </summary>
        public bool UseHeldentoolNames { get; set; } = false;
        /// <summary>
        /// Current instance of <see cref="Magehelper.Core.Staff"/>.
        /// </summary>
        public Staff Staff { get; internal set; }
        /// <summary>
        /// Current instance of <see cref="Magehelper.Core.CrystalBall"/>.
        /// </summary>
        public CrystalBall CrystalBall { get; internal set; }
        /// <summary>
        /// Current instance of <see cref="Magehelper.Core.Bowl"/>.
        /// </summary>
        public Bowl Bowl { get; internal set; }
        /// <summary>
        /// Current instance of <see cref="Magehelper.Core.BoneCub"/>.
        /// </summary>
        public BoneCub BoneCub { get; internal set; }
        /// <summary>
        /// Current instance of <see cref="Magehelper.Core.RingOfLife"/>.
        /// </summary>
        public RingOfLife RingOfLife { get; internal set; }
        /// <summary>
        /// Current instance of <see cref="Magehelper.Core.ObsidianDagger"/>.
        /// </summary>
        public ObsidianDagger ObsidianDagger { get; internal set; }
        /// <summary>
        /// GUI action to perform for artifacts when loading a save file.
        /// </summary>
        public Action<string> AddArtifactGUIAction { get; set; }
        /// <summary>
        /// GUI action to perform for spell storage when loading a save file.
        /// </summary>
        public Action EnableSpellStorageGUIAction { get; set; }
        /// <summary>
        /// GUI action to perform for the pet when loading a save file.
        /// </summary>
        public Action AddPetGUIAction { get; set; }
        /// <summary>
        /// GUI action to perform for timers when loading a save file.
        /// </summary>
        public Action<Timer> AddTimerGUIAction { get; set; }
        /// <summary>
        /// Names of the artifacts.
        /// </summary>
        public ReadOnlyCollection<string> ArtifactNames => new string[]
        {
            "Alchemistenschale",
            "Knochenkeule",
            "Kristallkugel",
            "Magierstab",
            "Ring des Lebens",
            "Vulkanglasdolch"
        }.ToList().AsReadOnly();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settingsPath">The settings path to use.</param>
        public Core(string settingsPath)
        {
            BasePath = AppContext.BaseDirectory;
#if DEBUG
            SettingsPath = Path.Combine(BasePath, "BaseSettings");
#else
            SettingsPath = settingsPath;
#endif
        }

        /// <summary>
        /// Resets the entire tool.
        /// </summary>
        public void ResetTool()
        {
            if (Staff != null)
            {
                Staff.ResetTool();
            }
            if (CrystalBall != null)
            {
                CrystalBall.ResetTool();
            }
            if (Bowl != null)
            {
                Bowl.ResetTool();
            }
            if (BoneCub != null)
            {
                BoneCub.ResetTool();
            }
            if (RingOfLife != null)
            {
                RingOfLife.ResetTool();
            }
            if (ObsidianDagger != null)
            {
                ObsidianDagger.ResetTool();
            }
            if (SpellStorage != null)
            {
                SpellStorage.ResetTool();
            }
            if (FlameSword != null)
            {
                FlameSword.ResetTool();
            }
            if (Character != null)
            {
                Character.ResetTool();
            }
            if (Pet != null)
            {
                Pet.ResetTool();
            }
            if (Timers != null)
            {
                Timers.RemoveAll();
            }
            FileName = string.Empty;
            HasSpellStorage = false;
            HasFlameSword = false;
            FileAup = false;
            FileLep = false;
            FileAsp = false;
            FileChanged = false;
        }

        /// <summary>
        /// Write a save file.
        /// </summary>
        public void WriteFile()
        {
            Artifact[] artifacts = new Artifact[] { Bowl, BoneCub, CrystalBall, Staff, RingOfLife, ObsidianDagger };
            using (XmlWriter xw = XmlWriter.Create(FileName))
            {
                xw.WriteStartDocument();
                xw.WriteStartElement("magehelper");
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
                xw.WriteStartElement("artifacts");
                foreach (Artifact artifact in artifacts)
                {
                    if (artifact != null)
                    {
                        xw.WriteStartElement("artifact");
                        xw.WriteStartElement("data");
                        xw.WriteAttributeString("name", artifact.Name);
                        xw.WriteAttributeString("boundSpells", artifact.BoundSpells.Count.ToString());
                        if (artifact.Name == ("Kristallkugel" ?? "Ring des Lebens" ?? "Vulkanglasdolch"))
                        {
                            xw.WriteAttributeString("maxSpells", (artifact as IMaxSpellArtifact).MaxSpells.ToString());
                        }
                        if (artifact is Staff staff)
                        {
                            xw.WriteAttributeString("material", staff.Material.ToString());
                            xw.WriteAttributeString("length", staff.Length.ToString());
                            xw.WriteAttributeString("pasp", staff.Pasp.ToString());
                            xw.WriteAttributeString("hammerRkp", staff.HammerRkp.ToString());
                            xw.WriteAttributeString("FlameSwordFour", staff.IsFlameSwordFour.ToString());
                            xw.WriteAttributeString("FlameSwordFive", staff.IsFlameSwordFive.ToString());
                        }
                        if (artifact is CrystalBall crystalBall)
                        {
                            xw.WriteAttributeString("material", ((int)crystalBall.Material).ToString());
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
                    xw.WriteElementString("rkp", Pet.RKW.ToString());
                    xw.WriteElementString("ae", Pet.AE.ToString());
                    xw.WriteStartElement("attributes");
                    foreach (string attribute in Pet.AttributeStrings)
                    {
                        if (attribute != "AE")
                        {
                            PropertyInfo[] p = Pet.GetAttribute(attribute);
                            xw.WriteStartElement("attribute");
                            xw.WriteAttributeString("name", attribute.ToLower());
                            xw.WriteAttributeString("current", p[0].GetValue(Pet).ToString());
                            xw.WriteAttributeString("start", p[1].GetValue(Pet).ToString());
                            xw.WriteEndElement();
                        }
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
        }

        /// <summary>
        /// Read a save file.
        /// </summary>
        /// <param name="path"></param>
        public void ReadFile(string path)
        {
            string xml = File.ReadAllText(path);
            Artifact[] artifacts = new Artifact[] { Bowl, BoneCub, CrystalBall, Staff, RingOfLife, ObsidianDagger }; XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            try
            {
                ResetTool();
                string aup = xmlDocument.SelectSingleNode("//aup").Value;
                if (aup != null)
                {
                    FileAup = true;
                    FileAupValue = int.Parse(aup);
                }
                string lep = xmlDocument.SelectSingleNode("//lep").Value;
                if (lep != null)
                {
                    FileLep = true;
                    FileLepValue = int.Parse(lep);
                }
                string asp = xmlDocument.SelectSingleNode("//asp").Value;
                if (asp != null)
                {
                    FileAsp = true;
                    FileAspValue = int.Parse(asp);
                }
                for (int i = 0; i < artifacts.Length; i++)
                {
                    Artifact artifact = artifacts[i];
                    XmlNode artifactNode = xmlDocument.SelectSingleNode("//artifact/data[@name='" + ArtifactNames[i] + "']/..");
                    if (artifactNode != null)
                    {
                        if (artifact is null)
                        {
                            switch (ArtifactNames[i])
                            {
                                case "Alchemistenschale":
                                    Bowl = new Bowl(this);
                                    artifact = Bowl;
                                    break;
                                case "Knochenkeule":
                                    BoneCub = new BoneCub(this);
                                    artifact = BoneCub;
                                    break;
                                case "Kristallkugel":
                                    CrystalBall = new CrystalBall(this);
                                    artifact = CrystalBall;
                                    break;
                                case "Magierstab":
                                    Staff = new Staff(this);
                                    artifact = Staff;
                                    break;
                                case "Ring des Lebens":
                                    RingOfLife = new RingOfLife(this);
                                    artifact = RingOfLife;
                                    break;
                                case "Vulkanglasdolch":
                                    ObsidianDagger = new ObsidianDagger(this);
                                    artifact = ObsidianDagger;
                                    break;
                            }
                        }
                        XmlAttributeCollection data = artifactNode.ChildNodes[0].Attributes;
                        if (artifact is Staff)
                        {
                            Staff.Material = int.Parse(data["material"].Value);
                            Staff.Length = int.Parse(data["length"].Value);
                            Staff.Pasp = int.Parse(data["pasp"].Value);
                            Staff.AfvTotal();
                            Staff.HammerRkp = int.Parse(data["hammerRkp"].Value);
                            Staff.IsFlameSwordFour = data["FlameSwordFour"].Value == "true";
                            Staff.IsFlameSwordFive = data["FlameSwordFive"].Value == "true";
                        }
                        if (artifact is CrystalBall)
                        {
                            CrystalBall.Material = (CrystalBallMaterial)int.Parse(data["material"].Value);
                        }
                        artifact.HasApport = data["apport"].Value == "true";
                        foreach (XmlNode boundSpell in artifactNode.ChildNodes[1].ChildNodes)
                        {
                            if (artifact is Staff)
                            {
                                string guid = boundSpell.Attributes["guid"].Value;
                                string name = boundSpell.Attributes["name"].Value;
                                string characteristic = boundSpell.Attributes["characteristic"].Value;
                                int points = int.Parse(boundSpell.Attributes["points"].Value);
                                Staff.AddSpell(name, characteristic, points, guid);
                            }
                            else
                            {
                                string guid = boundSpell.Attributes["guid"].Value;
                                string name = boundSpell.Attributes["name"].Value;
                                artifact.AddSpell(name, guid);
                            }
                        }
                        AddArtifactGUIAction?.Invoke(ArtifactNames[i]);
                    }
                }
                if (SpellStorage != null && xmlDocument.SelectSingleNode("//spellStorage").HasChildNodes)
                {
                    List<int> spellStorages = new List<int>();
                    foreach (XmlNode volume in xmlDocument.GetElementsByTagName("volume"))
                    {
                        spellStorages.Add(int.Parse(volume.InnerText));
                    }
                    SpellStorage.EnableStorage(spellStorages);
                    foreach (XmlNode spell in xmlDocument.SelectNodes("//spellStorage/spells/spell"))
                    {
                        string guid = spell.Attributes["guid"].Value;
                        string name = spell.Attributes["name"].Value;
                        string characteristics = spell.Attributes["characteristics"].Value;
                        string komplex = spell.Attributes["komplex"].Value;
                        int cost = int.Parse(spell.Attributes["cost"].Value);
                        int zfp = int.Parse(spell.Attributes["zfp"].Value);
                        int storage = int.Parse(spell.Attributes["storage"].Value);
                        SpellStorage.AddSpell(name, characteristics, komplex, cost, zfp, storage, guid);
                    }
                    EnableSpellStorageGUIAction?.Invoke();
                }
                if (Pet != null && xmlDocument.SelectSingleNode("//pet").HasChildNodes)
                {
                    Pet.Species = xmlDocument.SelectSingleNode("//species").InnerText;
                    Pet.IsFlying = xmlDocument.SelectSingleNode("//flying").InnerText == "true";
                    Pet.IsMightyCompanion = xmlDocument.SelectSingleNode("//mightyCompanion").InnerText == "true";
                    Pet.RKW = int.Parse(xmlDocument.SelectSingleNode("//rkp").InnerText);
                    Pet.AE = int.Parse(xmlDocument.SelectSingleNode("//ae").InnerText);
                    foreach (XmlNode attribute in xmlDocument.GetElementsByTagName("attribute"))
                    {
                        PropertyInfo[] p = Pet.GetAttribute(attribute.Attributes["name"].Value);
                        p[0].SetValue(Pet, int.Parse(attribute.Attributes["current"].Value));
                        p[1].SetValue(Pet, int.Parse(attribute.Attributes["start"].Value));
                    }
                    List<PetSpell> knownSpells = new List<PetSpell>();
                    foreach (XmlNode spell in xmlDocument.SelectNodes("//pet/spells/spell"))
                    {
                        knownSpells.Add(Pet.SpellsAvailable.Single(p => p.Name == spell.InnerText));
                    }
                    Pet.Knownspells = knownSpells;
                    AddPetGUIAction?.Invoke();
                }
                if (Timers != null && xmlDocument.SelectSingleNode("//timers").HasChildNodes)
                {
                    foreach (XmlNode timer in xmlDocument.GetElementsByTagName("timer"))
                    {
                        string guid = timer.Attributes["guid"].Value;
                        string text = timer.Attributes["text"].Value;
                        int duration = int.Parse(timer.Attributes["duration"].Value);
                        Timer t = Timers.Add(text, duration, guid);
                        AddTimerGUIAction?.Invoke(t);
                    }
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
    }
}