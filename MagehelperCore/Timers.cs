namespace Magehelper.Core;

public class Timers : IEnumerable<Timer>
{
    private readonly Core _core = Core.Instance;
    private readonly List<Timer> _timers = [];
    public Timer this[int i] => _timers[i];
    public Timer this[string guid] => _timers.SingleOrDefault(t => t.Guid == guid);
    public int Count => _timers.Count;

    public static Dictionary<string, int> DurationMultiplier => new()
    {
        {"KR",1},
        {"SR",100},
        {"Tage",28800}
    };

    public static string[] DurationUnits =>
    [
        "KR",
        "SR",
        "Tage"
    ];

    /// <summary>
    /// Constructor
    /// </summary>
    public Timers()
    {
        _core.Timers = this;
        ReadFile();
        
    }

    internal void ReadFile()
    {
        if (_core.XmlDoc == null || !_core.XmlDoc!.SelectSingleNode("//timers")!.HasChildNodes)
        {
            return;
        }

        foreach (XmlNode timer in _core.XmlDoc.GetElementsByTagName("timer"))
        {
            string guid = timer!.Attributes!["guid"]!.Value;
            string text = timer.Attributes!["text"]!.Value;
            int duration = int.Parse(timer.Attributes!["duration"]!.Value);
            Add(text, duration, guid);
        }
    }

    /// <summary>
    /// Adds a timer.
    /// </summary>
    /// <param name="text">Text for the timer.</param>
    /// <param name="duration">Duration thats be displayed in the GUI.</param>
    /// <param name="guid">GUID of the timer</param>
    /// <returns></returns>
    public Timer Add(string text, int duration, string? guid = null)
    {
        guid ??= Guid.NewGuid().ToString();
        Timer timer = new() { Guid = guid, Text = text, Duration = duration };
        _timers.Add(timer);
        _core.FileChanged = true;
        return timer;
    }

    /// <summary>
    /// Remove a timer with the given GUID.
    /// </summary>
    /// <param name="guid"></param>
    public void Remove(string guid)
    {
        _timers.Remove(_timers.Single(t => t.Guid == guid));
        _core.FileChanged = true;
    }

    /// <summary>
    /// Remove all Timers.
    /// </summary>
    public void RemoveAll()
    {
        _timers.Clear();
        _core.FileChanged = true;
    }

    public void CountDown(string guid, int amount)
    {
        for (int i = 0; i < _timers.Count; i++)
        {
            if (_timers[i].Guid != guid)
            {
                continue;
            }

            Timer timer = _timers[i];
            timer.Duration -= amount;
            _timers[i] = timer;

            if (timer.Duration <= 0)
            {
                _timers.RemoveAt(i);
            }

            _core.FileChanged = true;
            break;
        }
    }

    public void CountDownAll(int amount, int minDuration)
    {
        foreach (Timer timer in _timers.Where(timer => timer.Duration >= minDuration))
        {
            CountDown(timer.Guid, amount);
        }
    }

    public IEnumerator<Timer> GetEnumerator()
    {
        return _timers.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _timers.GetEnumerator();
    }
}
