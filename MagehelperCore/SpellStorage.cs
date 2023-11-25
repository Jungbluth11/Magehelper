using System.Collections.ObjectModel;

namespace Magehelper.Core
{
    public class SpellStorage
    {
        private int[] _pointsUsed = new int[0];
        private readonly List<StoragedSpell> spells = new List<StoragedSpell>();
        private readonly Core core;
        /// <summary>
        /// Number of spell storages.
        /// </summary>
        public int StorageCount => core.HasSpellStorage ? PointsTotal.Count : 0;
        /// <summary>
        /// Total amount of points in each storage.
        /// </summary>
        public ReadOnlyCollection<int> PointsTotal { get; private set; } = new int[0].ToList().AsReadOnly();
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
                List<int> output = new List<int>();
                for (int i = 0; i < StorageCount; i++)
                {
                    output.Add(PointsTotal[i] - _pointsUsed[i]);
                }
                return output.AsReadOnly();
            }
        }
        /// <summary>
        /// spells thats are stored in the spell storage.
        /// </summary>
        public ReadOnlyCollection<StoragedSpell> Spells => spells.AsReadOnly();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="core">An instance of <see cref="Core"/>.</param>
        public SpellStorage(Core core)
        {
            core.SpellStorage = this;
            this.core = core;
        }

        /// <summary>
        /// calculates the maximal amount of AsP that can be stored
        /// </summary>
        /// <param name="volumePoints">points thats used to create the spell storage.</param>
        /// <returns></returns>
        public int GetMaxPoints(int volumePoints)
        {
            return volumePoints * core.SpellStoragePoints;
        }

        /// <summary>
        /// Enables the storage.
        /// </summary>
        /// <param name="spellStorages">configuration of the storages.</param>
        public void EnableStorage(List<int> spellStorages)
        {
            PointsTotal = spellStorages.AsReadOnly();
            _pointsUsed = new int[spellStorages.Count];
            core.HasSpellStorage = true;
            core.FileChanged = true;
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
                throw new Exception("Storage not enabled");
            }
            if (guid is null)
            {
                guid = Guid.NewGuid().ToString();
            }
            StoragedSpell storagedSpell = new StoragedSpell { Guid = guid, Name = name, Characteristics = characteristics, Komplex = komplex, Cost = cost, Zfp = zfp, Storage = storage };
            spells.Add(storagedSpell);
            _pointsUsed[storage] += cost;
            core.FileChanged = true;
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
                throw new Exception("Storage not enabled");
            }
            for (int i = 0; i < spells.Count; i++)
            {
                if (spells[i].Guid == guid)
                {
                    _pointsUsed[spells[i].Storage] -= spells[i].Cost;
                    spells.RemoveAt(i);
                    core.FileChanged = true;
                }
            }
        }

        /// <summary>
        /// Resets the instance of this class. (Only used from <see cref="Core.ResetTool"/>.)
        /// </summary>
        internal void ResetTool()
        {
            spells.Clear();
            _pointsUsed = new int[0];
            PointsTotal = new int[0].ToList().AsReadOnly();
        }
    }
}