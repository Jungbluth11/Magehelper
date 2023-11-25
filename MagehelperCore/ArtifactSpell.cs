namespace Magehelper.Core
{
    /// <summary>
    /// Represents a spell that can be put on an artifact.
    /// </summary>
    public struct ArtifactSpell
    {
        private string _displayText;
        /// <summary>
        /// The GUID of this spell.
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// THe points this spell used up. (Only used from <see cref="Staff"/>.)
        /// </summary>
        public int Points { get; set; }
        /// <summary>
        /// The name of this Spell
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The characteristic of this spell.
        /// </summary>
        public string Characteristic { get; set; }
        /// <summary>
        /// The type of the artifact where this spell can be put on.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The Cost of this spell to use.
        /// </summary>
        public int Cost { get; set; }
        /// <summary>
        /// Other spells that already musst if put on this artifact (if any).
        /// </summary>
        public string[] Requirements { get; set; }
        /// <summary>
        /// How many times can this spell on this artifact (0 means infinite).
        /// </summary>
        public int Max { get; set; }
        /// <summary>
        /// If this is a new Spell or not.
        /// </summary>
        /// <remarks>Spells generated from a save file are not new.</remarks>
        public bool IsNew { get; set; }
        /// <summary>
        /// The text that are shown on GUI.
        /// </summary>
        public string DisplayText
        {
            get => string.IsNullOrEmpty(_displayText) ? Name : _displayText;
            set => _displayText = value;
        }
    }
}