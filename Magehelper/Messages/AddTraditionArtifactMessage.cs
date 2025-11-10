namespace Magehelper.Messages;

public class AddTraditionArtifactMessage(string artifactName, Dictionary<string, string> additionalValues)
{
    public string ArtifactName { get; } = artifactName;
    public Dictionary<string, string> AdditionalValues { get; } = additionalValues;
}
