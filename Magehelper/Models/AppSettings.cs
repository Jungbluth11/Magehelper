namespace Magehelper.Models;

public struct AppSettings
{
    public int SpellStoragePoints { get; set; }
    public bool UseHeldentoolNames { get; set; }
    public bool AllowRemoveSpells { get; set; }
    public bool CheckForUpdates { get; set; }
    public bool AutoInstallUpdates { get; set; }
    public bool WarnOtherVersionFiles { get; set; }
    public bool ChangeTraditionArtifactTabName { get; set; }
}
