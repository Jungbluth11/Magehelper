namespace Magehelper.Helper;

public static class SettingsHelper
{
    private static readonly Settings Settings = Settings.Instance;

    public static Dictionary<string, string> TabName => new()
    {
        {"Magehelper.Views.Tabs.TabTraditionArtifact","Traditionsartefakt"},
        {"Magehelper.Views.Tabs.TabSpellStorage","Zauberspeicher"},
        {"Magehelper.Views.Tabs.TabFlameSword","Flammenschwert"},
        {"Magehelper.Views.Tabs.TabArtifact","Artefakte"},
        {"Magehelper.Views.Tabs.TabArcaneGlyphs","Zauberzeichen"},
        {"Magehelper.Views.Tabs.TabCharacter","Charakter"},
        {"Magehelper.Views.Tabs.TabPet","Vertrautentier"},
        {"Magehelper.Views.Tabs.TabTimer","Timer"},
        {"Magehelper.Views.Tabs.TabMod","Modifikationsrechner"}
    };

    public static Dictionary<string, ArtifactSpell[]> ArtifactSpells => new()
    {
        {"staff.json", Settings.StaffSpells},
        {"crystalBall.json", Settings.CrystalBallSpells},
        {"bowl.json", Settings.BowlSpells},
        {"boneCub.json", Settings.BoneCubSpells},
        {"ringOfLife.json", Settings.RingOfLifeSpells},
        {"obsidianDagger.json", Settings.ObsidianDaggerSpells}
    };

    public static Dictionary<string, string> TraditionArtifactName => new()
    {
        {"staff.json", TraditionalArtifactHelper.ArtifactNames.Staff},
        {"crystalBall.json",TraditionalArtifactHelper.ArtifactNames.CrystalBall},
        {"bowl.json", TraditionalArtifactHelper.ArtifactNames.Bowl},
        {"boneCub.json", TraditionalArtifactHelper.ArtifactNames.BoneCub},
        {"ringOfLife.json", TraditionalArtifactHelper.ArtifactNames.RingOfLife},
        {"obsidianDagger.json", TraditionalArtifactHelper.ArtifactNames.ObsidianDagger}
    };
}
