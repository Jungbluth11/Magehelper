namespace Magehelper.Models.Helper;

public static class TraditionalArtifactHelper
{
    private static readonly Core.Core Core = Magehelper.Core.Core.GetInstance();

    public static Dictionary<string, bool> IsInitialized => new()
    {
        {ArtifactNames.Bowl, Core.Bowl != null},
        {ArtifactNames.BoneCub, Core.BoneCub != null},
        {ArtifactNames.CrystalBall, Core.CrystalBall != null},
        {ArtifactNames.Staff, Core.Staff != null},
        {ArtifactNames.RingOfLife, Core.RingOfLife != null},
        {ArtifactNames.ObsidianDagger, Core.ObsidianDagger != null}
    };

    public static Dictionary<string, Type> GetControlType => new()
    {
            {ArtifactNames.Bowl, typeof(Bowl)},
        //    {ArtifactNames.BoneCub, typeof(BoneCubControl)},
        //    {ArtifactNames.CrystalBall, typeof(CrystalBallControl)},
        //    {ArtifactNames.Staff, typeof(StaffControl)},
        //    {ArtifactNames.RingOfLife, typeof(RingOfLifeControl)},
        //    {ArtifactNames.ObsidianDagger, typeof(ObsidianDaggerControl)}
    };

    public static Dictionary<string, Artifact?> GetArtifact => new()
    {
        {ArtifactNames.Bowl, Core.Bowl},
        {ArtifactNames.BoneCub, Core.BoneCub},
        {ArtifactNames.CrystalBall, Core.CrystalBall},
        {ArtifactNames.Staff, Core.Staff},
        {ArtifactNames.RingOfLife, Core.RingOfLife},
        {ArtifactNames.ObsidianDagger, Core.ObsidianDagger}
    };

    public static Dictionary<string, string> SpellDescriptor => new()
    {
        {ArtifactNames.Bowl, "Schalenzauber"},
        {ArtifactNames.BoneCub,"Keulenzauber"},
        {ArtifactNames.CrystalBall, "Kugelzauber"},
        {ArtifactNames.Staff, "Stabzauber"},
        {ArtifactNames.RingOfLife,"Ringzauber"},
        {ArtifactNames.ObsidianDagger,"Dolchzauber"}
    };

    public static class ArtifactNames
    {
        public static string Bowl => "Alchemistenschale";
        public static string BoneCub => "Knochenkeule";
        public static string CrystalBall => "Kristallkugel";
        public static string Staff => "Magierstab";
        public static string RingOfLife => "Ring des Lebens";
        public static string ObsidianDagger => "Vulkanglasdolch";
    }
}
