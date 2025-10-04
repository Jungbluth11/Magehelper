namespace Magehelper.Models.Messages;

public class EnableTabMessage(string tabName, int points = 0)
{
    public string TabName { get; } = tabName;
    public int Points { get; } = points;
}

