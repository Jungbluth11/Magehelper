﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using DSAUtils.HeldentoolInterop;

namespace Magehelper.Core
{
    /// <summary>
    /// Artifact base class, all artifacts musst inherit it.
    /// </summary>
    public abstract class Artifact
    {
        protected ArtifactSpell[] spellsAvailable;
        protected readonly List<ArtifactSpell> boundSpells = new List<ArtifactSpell>();
        protected readonly Core core;
        /// <summary>
        /// Name of the Artifact
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// If artifact has the apport spell or not
        /// </summary>
        public bool HasApport { get; set; }
        /// <summary>
        /// Spells that can be put on this artifact.
        /// </summary>
        public ReadOnlyCollection<ArtifactSpell> SpellsAvailable => spellsAvailable.ToList().AsReadOnly();
        /// <summary>
        /// Spells that are put on the artifact.
        /// </summary>
        public ReadOnlyCollection<ArtifactSpell> BoundSpells => boundSpells.AsReadOnly();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="core">An instance of <see cref="Core"/>.</param>
        /// <param name="jsonSettingsFile">Name of the settings file that contains the spells for this artifact.</param>
        /// <param name="artifactName"> Name of the artifact. (See also <seealso cref="Name"/>)</param>
        public Artifact(Core core, string jsonSettingsFile, string artifactName)
        {
            this.core = core;
            Name = artifactName;
            spellsAvailable = JsonSerializer.Deserialize<ArtifactSpell[]>(File.ReadAllText(Path.Combine(core.SettingsPath, jsonSettingsFile)));
            if (core.UseHeldentoolNames)
            {
                for (int i = 0; i < spellsAvailable.Length; i++)
                {
                    spellsAvailable[i].Name = HeldentoolInterop.Rename(spellsAvailable[i].Name, DSAUtils.HeldentoolInterop.Name.Tool);
                }
            }
        }

        /// <summary>
        /// Adds an spells to this artifact.
        /// </summary>
        /// <param name="spellName">Name of the spell.</param>
        /// <param name="guid">GUID of the spell. (Only used by <see cref="Core.ReadFile"/>)</param>
        /// <returns>An instance of <see cref="ArtifactSpell"/> that contains data about the chosen spell.</returns>
        /// <exception cref="ArgumentException"/>
        /// /// <exception cref="InvalidOperationException"/>
        public ArtifactSpell AddSpell(string spellName, string guid = null)
        {
            try
            {
                ArtifactSpell spell = spellsAvailable.Single(a => a.Name == spellName);
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
        /// Adds an spells to this artifact. Also contains the condition check.
        /// </summary>
        /// <param name="spell">An instance of <see cref="ArtifactSpell"/> that was generated from the caller method.</param>
        /// <param name="guid">GUID of the spell. (Only used by <see cref="Core.ReadFile"/>)</param>
        /// <returns>An instance of <see cref="ArtifactSpell"/> that contains data about the chosen spell. An exception when not match the conditions.</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        protected ArtifactSpell AddSpell(ArtifactSpell spell, string guid = null)
        {
            try
            {
                if (spell.Max > 0 && boundSpells.Where(a => a.Name == spell.Name).Count() >= spell.Max)
                {
                    throw new InvalidOperationException("Maximum reached");
                }

                if (spell.Reqirements != null)
                {
                    foreach (string requirement in spell.Reqirements)
                    {
                        try
                        {
                            ArtifactSpell artifactSpell = BoundSpells.Single(a => a.Name == requirement);
                        }
                        catch
                        {
                            throw new InvalidOperationException("Requirements not full filled! Requirements: " + string.Join(", ", spell.Reqirements));
                        }
                    }
                }
                if (guid is null)
                {
                    spell.Guid = Guid.NewGuid().ToString();
                    spell.IsNew = true;
                }
                else
                {
                    spell.Guid = guid;
                    if (boundSpells.Contains(spell))
                    {
                        throw new ArgumentException("Spell already exists", nameof(guid));
                    }
                }
                boundSpells.Add(spell);
                core.FileChanged = true;
                return spell;
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
        public void RemoveSpell(string guid)
        {
            try
            {
                boundSpells.Remove(boundSpells.Single(a => a.Guid == guid));
                core.FileChanged = true;
            }
            catch
            {
                throw new ArgumentException("GUID doesn't exist", nameof(guid));
            }
        }

        /// <summary>
        /// Resets the instance of this class. (Only used from <see cref="Core.ResetTool"/>.)
        /// </summary>
        internal void ResetTool()
        {
            boundSpells.Clear();
        }
    }
}