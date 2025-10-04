namespace Magehelper.Core;

public class BoneCub : Artifact
{
    public static string[] TypeStrings =>
    [
        "Kleine Knochenkeule",
        "Mittelgroße Knochenkeule",
        "Große Knochenkeule",
        "Sägefischschwert",
        "echsisches Szepter"
    ];

    public string EnsoulEntityName { get; set; } = string.Empty;

    public int? EnsoulEntityLoyalty { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public BoneCub() : base("boneCub.json", "Knochenkeule")
    {
        _core.BoneCub = this;
    }
}
