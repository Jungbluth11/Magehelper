namespace Magehelper.Core
{
    /// <summary>
    /// Represents a spell that can be used from the loaded character.
    /// </summary>
    public struct CharacterSpell
    {
        /// <summary>
        /// The name of this Spell
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The attributes thats be used to cast this ritual.
        /// </summary>
        public string[] Attributes { get; set; }
        /// <summary>
        /// An string of <see cref="Attributes"/> that is displayed in the GUI.
        /// </summary>
        public string AttributeString => Attributes[0] + "/" + Attributes[1] + "/" + Attributes[2];
        /// <summary>
        /// The characteristics of this spell.
        /// </summary>
        public string Characteristics { get; set; }
        public string Representation { get; set; }
        /// <summary>
        /// the complexity of this spell.
        /// </summary>
        public string Komplex { get; set; }
        /// <summary>
        /// The value of this spell.
        /// </summary>
        public int Value { get; set; }
    }
}