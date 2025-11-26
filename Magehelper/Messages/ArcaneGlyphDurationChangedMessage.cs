namespace Magehelper.Messages;

public class ArcaneGlyphDurationChangedMessage(string guid, int duration)
{
    public string Guid { get; } = guid;
    public int Duration { get; } = duration;
}
