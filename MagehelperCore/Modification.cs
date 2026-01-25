namespace Magehelper.Core;

/// <summary>
/// Represents a modification.
/// </summary>
public record struct Modification
{
    /// <summary>
    /// Name of the modification.
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// the description of the modification.
    /// </summary>
    public string Description { get; }
    /// <summary>
    /// the amount to add or remove.
    /// </summary>
    public int Amount { get; }

    public Modification(string name, string description, int amount)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Amount = amount;
    }
}
