namespace Magehelper.Messages;

public class AddArtifactSpellDialogMessage(string artifactName, string spellName, Dictionary<string, string> additionalValues)
{
    public string ArtifactName { get; } = artifactName;
    public string SpellName { get; } = spellName;
    public Dictionary<string, string> AdditionalValues { get; } = additionalValues;
}
