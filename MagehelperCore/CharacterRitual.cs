namespace Magehelper.Core
{
    /// <summary>
    /// Represents an ritual that can be used from the loaded character.
    /// </summary>
    public struct CharacterRitual
    {
        /// <summary>
        /// The name of the ritual.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The attributes thats be used to cast this ritual.
        /// </summary>
        public string Attributes { get; set; }
        /// <summary>
        /// The Skill that used with this ritual.
        /// </summary>
        public string Skill { get; set; }
        /// <summary>
        /// the modifications of this ritual (if any).
        /// </summary>
        public int[] Mod { get; set; }
        /// <summary>
        /// The type of this ritual.
        /// </summary>
        public RitualType Type { get; set; }
    }
}