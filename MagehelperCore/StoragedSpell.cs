namespace Magehelper.Core
{
    /// <summary>
    /// Represents a spell that is stored in the spell storage. (See also <seealso cref="SpellStorage"/>)
    /// </summary>
    public struct StoragedSpell
    {
        /// <summary>
        /// GUID of the spell.
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// Name of the Spell
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Complexity of the spell.
        /// </summary>
        public string Komplex { get; set; }
        /// <summary>
        /// characteristic of the spell.
        /// </summary>
        public string Characteristics { get; set; }
        /// <summary>
        /// Text that is displayed in the GUI.
        /// </summary>
        public string DisplayText => Zfp == null ? Name : Name + " (" + Zfp.ToString() + " ZfP)";
        /// <summary>
        /// Coast of the Spell
        /// </summary>
        public int Cost { get; set; }
        /// <summary>
        /// ZfP of the spell.
        /// </summary>
        public int? Zfp { get; set; }
        /// <summary>
        /// Index of Storage where the spell is stored.
        /// </summary>
        public int Storage { get; set; }
    }
}