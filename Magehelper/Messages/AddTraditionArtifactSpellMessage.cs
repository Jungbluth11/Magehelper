namespace Magehelper.Messages;

public class AddTraditionArtifactSpellMessage(ArtifactSpell artifactSpell, Type type)
{
    public ArtifactSpell ArtifactSpell { get; } = artifactSpell;
    public Type Type { get; } = type;
}
