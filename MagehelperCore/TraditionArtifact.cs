using System.Text.Json;

namespace Magehelper.Core;

/// <summary>
/// Tradition artifact base class, all tradition artifacts musst inherit it.
/// </summary>
public abstract class TraditionArtifact
{
    protected ArtifactSpell[]? spellsAvailable;
    protected readonly List<ArtifactSpell> boundSpells = [];
    protected readonly Core _core = Core.Instance;
    /// <summary>
    /// Name of the Artifact
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// If artifact has the "Apport" spell or not
    /// </summary>
    public bool HasApport { get; set; }
    /// <summary>
    /// Spells that can be put on this artifact.
    /// </summary>
    public ReadOnlyCollection<ArtifactSpell> SpellsAvailable => spellsAvailable!.ToList().AsReadOnly();
    /// <summary>
    /// Spells that are put on the artifact.
    /// </summary>
    public ReadOnlyCollection<ArtifactSpell> BoundSpells => boundSpells.AsReadOnly();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="jsonSettingsFile">Name of the settings file that contains the spells for this artifact.</param>
    /// <param name="artifactName"> Name of the artifact. (See also <seealso cref="Name"/>)</param>

#pragma warning disable CS8618 // program files are corrupted if SpellsAvailable is null
    protected TraditionArtifact(string jsonSettingsFile, string artifactName)
#pragma warning restore CS8618
    {
        Name = artifactName;
        _core.FileChanged = true;
        spellsAvailable = JsonSerializer.Deserialize<ArtifactSpell[]>(File.ReadAllText(Path.Combine(_core.SettingsPath, jsonSettingsFile)));

        if (!_core.UseHeldentoolNames)
        {
            return;
        }

        for (int i = 0; i < spellsAvailable!.Length; i++)
        {
            spellsAvailable[i].Name = HeldentoolInterop.Rename(spellsAvailable[i].Name, DSAUtils.HeldentoolInterop.Name.Tool);
        }
    }

    protected XmlNode GetTraditionArtifactNode()
    {
        string traditionArtifactNodeName = _core.GetFileVersion() == "3.0.0" ? "artifact" : "traditionArtifact";
        return _core.XmlDoc!.SelectSingleNode($"//{traditionArtifactNodeName}/data[@name='{Name}']/..")!;
    }

    internal void Readfile()
    {
        if (_core.XmlDoc == null)
        {
            return;
        }

        XmlNode node = GetTraditionArtifactNode();
        HasApport = node.ChildNodes[0]!.Attributes!["apport"]!.Value == "True";

        foreach (XmlNode boundSpell in node.ChildNodes[1]!.ChildNodes)
        {
            string guid = boundSpell.Attributes!["guid"]!.Value;
            string name = boundSpell.Attributes["name"]!.Value;
            AddSpell(name, guid);
        }
    }

    /// <summary>
    /// Adds an spells to this artifact.
    /// </summary>
    /// <param name="spellName">Name of the spell.</param>
    /// <param name="guid">GUID of the spell. (Only used by <see cref="Core.ReadFile"/>)</param>
    /// <returns>An instance of <see cref="ArtifactSpell"/> that contains data about the chosen spell.</returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="InvalidOperationException"/>
    /// <remarks>Some artifacts may provide their own AddSpell class</remarks>
    public ArtifactSpell AddSpell(string spellName, string? guid = null)
    {
        try
        {
            ArtifactSpell spell = spellsAvailable!.Single(a => a.Name == spellName);
            return AddSpell(spell, guid);
        }
        catch (InvalidOperationException e)
        {
            if (e.Message == "Sequence contains no matching element")
            {
                throw new ArgumentException("Spell doesn't exist", nameof(spellName));
            }

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
    protected ArtifactSpell AddSpell(ArtifactSpell spell, string? guid = null)
    {
        if (spell.Max > 0 && boundSpells.Count(a => a.Name == spell.Name) >= spell.Max)
        {
            throw new InvalidOperationException("Maximum reached");
        }

        if (spell.Requirements != null)
        {
            foreach (string requirement in spell.Requirements)
            {
                try
                {
                    // ReSharper disable once UnusedVariable --- just checking if requirement is fulfilled
                    ArtifactSpell artifactSpell = BoundSpells.Single(a => a.Name == requirement);
                }
                catch
                {
                    throw new InvalidOperationException("Requirements not full filled! Requirements: " + string.Join(", ", spell.Requirements));
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
        _core.FileChanged = true;
        return spell;
    }

    /// <summary>
    /// Remove a spell with the given GUID from this artifact.
    /// </summary>
    /// <param name="guid">The GUID of the that should be removed.</param>
    /// <exception cref="ArgumentException"/>
    /// <remarks>Some artifacts may provide their own RemoveSpell class</remarks>
    public void RemoveSpell(string guid)
    {
        try
        {
            boundSpells.Remove(boundSpells.Single(a => a.Guid == guid));
            _core.FileChanged = true;
        }
        catch
        {
            throw new ArgumentException("GUID doesn't exist", nameof(guid));
        }
    }

    /// <summary>
    /// Resets the instance of this class. (only used by <see cref="Core.ResetTool"/>.)
    /// </summary>
    internal void ResetTool()
    {
        boundSpells.Clear();
    }
}
