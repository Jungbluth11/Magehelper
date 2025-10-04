// Ignore Spelling: Storages

namespace Magehelper.Core;

public class SpellStorage
{
    private int[] _pointsUsed = [];
    private readonly List<StoragedSpell> _spells = [];
    private readonly Core _core = Core.GetInstance();
    /// <summary>
    /// Number of spell storages.
    /// </summary>
    public int StorageCount => _core.HasSpellStorage ? PointsTotal.Count : 0;
    /// <summary>
    /// Total amount of points in each storage.
    /// </summary>
    public ReadOnlyCollection<int> PointsTotal { get; private set; } = Array.Empty<int>().ToList().AsReadOnly();
    /// <summary>
    /// Amount of points used in each storage.
    /// </summary>
    public ReadOnlyCollection<int> PointsUsed => _pointsUsed.ToList().AsReadOnly();
    /// <summary>
    /// Amount of points left in each storage.
    /// </summary>
    public ReadOnlyCollection<int> PointsRemain
    {
        get
        {
            List<int> output = [];
            for (int i = 0; i < StorageCount; i++)
            {
                output.Add(PointsTotal[i] - _pointsUsed[i]);
            }
            return output.AsReadOnly();
        }
    }
    /// <summary>
    /// spells that's are stored in the spell storage.
    /// </summary>
    public ReadOnlyCollection<StoragedSpell> Spells => _spells.AsReadOnly();

    /// <summary>
    /// Constructor
    /// </summary>
    public SpellStorage()
    {
        _core.SpellStorage = this;
    }

    /// <summary>
    /// calculates the maximal amount of AsP that can be stored
    /// </summary>
    /// <param name="volumePoints">points that's used to create the spell storage.</param>
    /// <returns></returns>
    public int GetMaxPoints(int volumePoints)
    {
        return volumePoints * _core.SpellStoragePoints;
    }

    /// <summary>
    /// Enables the storage.
    /// </summary>
    /// <param name="spellStorages">configuration of the storage's.</param>
    public void EnableStorage(List<int> spellStorages)
    {
        PointsTotal = spellStorages.AsReadOnly();
        _pointsUsed = new int[spellStorages.Count];
        _core.HasSpellStorage = true;
        _core.FileChanged = true;
    }

    /// <summary>
    /// Adds a spell to storage.
    /// </summary>
    /// <param name="name">Name of the spell.</param>
    /// <param name="characteristics">Characteristics of the spell.</param>
    /// <param name="komplex">Complexity of the spell.</param>
    /// <param name="cost">Cost of the Spell</param>
    /// <param name="zfp">ZfP of the spell.</param>
    /// <param name="storage">index of the storage where the spell will be stored.</param>
    /// <param name="guid">GUID of the spell. (Only used by <see cref="Core.ReadFile"/>)</param>
    /// <returns></returns>
    public StoragedSpell AddSpell(string name, string characteristics, string komplex, int cost, int? zfp, int storage, string? guid = null)
    {
        if (StorageCount == 0)
        {
            throw new("Storage not enabled");
        }

        guid ??= Guid.NewGuid().ToString();
        StoragedSpell storagedSpell = new(guid, name, characteristics, komplex,
            cost, zfp, storage);
        _spells.Add(storagedSpell);
        _pointsUsed[storage] += cost;
        _core.FileChanged = true;
        return storagedSpell;
    }

    /// <summary>
    /// Remove a spell with the given GUID from spell storage.
    /// </summary>
    /// <param name="guid">The GUID of the that should be removed.</param>
    public void RemoveSpell(string guid)
    {
        if (StorageCount == 0)
        {
            throw new("Storage not enabled");
        }
        for (int i = 0; i < _spells.Count; i++)
        {
            if (_spells[i].Guid != guid)
            {
                continue;
            }

            _pointsUsed[_spells[i].Storage] -= _spells[i].Cost;
            _spells.RemoveAt(i);
            _core.FileChanged = true;
        }
    }

    /// <summary>
    /// Resets the instance of this class. (only used by <see cref="Core.ResetTool"/>.)
    /// </summary>
    internal void ResetTool()
    {
        _spells.Clear();
        _pointsUsed = [];
        PointsTotal = Array.Empty<int>().ToList().AsReadOnly();
    }
}
