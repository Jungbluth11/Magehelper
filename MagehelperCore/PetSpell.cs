namespace Magehelper.Core;

/// <summary>
/// Represents a spell that can be used from a pet.
/// </summary>
public record struct PetSpell
{
    /// <summary>
    /// The name of this Spell
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The attributes that's be used to cast this ritual.
    /// </summary>
    public string[] Attributes { get; set; }
    /// <summary>
    /// An string of <see cref="Attributes"/> that is displayed in the GUI.
    /// </summary>
    public readonly string AttributeString => $"{Attributes[0]}/{Attributes[1]}/{Attributes[2]}";
    /// <summary>
    /// The characteristics of this spell.
    /// </summary>
    public string Characteristics { get; set; }
}