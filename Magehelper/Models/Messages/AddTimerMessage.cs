namespace Magehelper.Models.Messages;

public class AddTimerMessage(string name, int duration)
{
    public string Name { get; } = name;
    public int Duration { get; } = duration;
}