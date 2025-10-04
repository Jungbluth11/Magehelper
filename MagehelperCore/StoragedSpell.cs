namespace Magehelper.Core;

/// <summary>
/// Represents a spell that is stored in the spell storage. (See also <seealso cref="SpellStorage"/>)
/// </summary>
public record StoragedSpell(
    string Guid,
    string Name,
    string Characteristics,
    string Komplex,
    int Cost,
    int? Zfp,
    int Storage)
{
    public string DisplayText => Zfp == null ? Name : Name + " (" + Zfp + " ZfP)";
}

