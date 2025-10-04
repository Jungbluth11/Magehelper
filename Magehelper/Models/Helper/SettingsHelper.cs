namespace Magehelper.Models.Helper;

public static class SettingsHelper
{
    private static readonly Settings Settings = Settings.GetInstance();

    public static Dictionary<string, string> TabName => new()
    {
        {"Magehelper.Views.Tabs.TabArtifact","Traditionsartefakt"},
        {"Magehelper.Views.Tabs.TabSpellStorage","Zauberspeicher"},
        {"Magehelper.Views.Tabs.FlameSword","Flammenschwert"},
        {"Magehelper.Views.Tabs.Character","Charakter"},
        {"Magehelper.Views.Tabs.Pet","Vertrautentier"},
        {"Magehelper.Views.Tabs.Timer","Timer"},
        {"Magehelper.Views.Tabs.Mod","Modifikationsrechner"}
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
}
