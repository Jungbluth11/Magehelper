namespace Magehelper.Models.Helper;

public static class TraditionalArtifactHelper
{
    private static readonly Core.Core Core = Magehelper.Core.Core.GetInstance();

    public static Dictionary<string, bool> IsInitialized => new()
    {
        {"Alchemistenschale", Core.Bowl != null},
        {"Knochenkeule", Core.BoneCub != null},
        {"Kristallkugel", Core.CrystalBall != null},
        {"Magierstab", Core.Staff != null},
        {"Ring des Lebens", Core.RingOfLife != null},
        {"Vulkanglasdolch", Core.ObsidianDagger != null}
    };

    public static Dictionary<string, Type> GetControlType => new()
    {
        {"Alchemistenschale", typeof(BowlControl)},
        {"Knochenkeule",typeof(BoneCub)},
        {"Kristallkugel", typeof(CrystalBall)},
        {"Magierstab",typeof(Staff)},
        {"Ring des Lebens", typeof(RingOfLifeControl)},
        {"Vulkanglasdolch", typeof(ObsidianDaggerControl)}
    };

    public static Dictionary<string, Artifact?> GetArtifact => new()
    {
        {"Alchemistenschale", Core.Bowl},
        {"Knochenkeule", Core.BoneCub},
        {"Kristallkugel", Core.CrystalBall},
        {"Magierstab", Core.Staff},
        {"Ring des Lebens", Core.RingOfLife},
        {"Vulkanglasdolch", Core.ObsidianDagger}
    };

    public static Dictionary<string, string> SpellDescriptor => new()
    {
        {"Alchemistenschale", "Schalenzauber"},
        {"Knochenkeule","Keulenzauber"},
        {"Kristallkugel", "Kugelzauber"},
        {"Magierstab", "Stabzauber"},
        {"Ring des Lebens","Ringzauber"},
        {"Vulkanglasdolch","Dolchzauber"}
    };
}
