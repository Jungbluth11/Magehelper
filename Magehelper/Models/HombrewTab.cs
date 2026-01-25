namespace Magehelper.Models;

public struct HomeBrewTab
{
    public string Name { get; set; }

    public string SettingsFile { get; set; }

    public ArtifactSpell[] Spells { get; set; }
}