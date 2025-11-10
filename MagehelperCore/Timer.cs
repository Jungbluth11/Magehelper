namespace Magehelper.Core;

/// <summary>
/// Represents a timer.
/// </summary>
public record struct Timer
{
    /// <summary>
    /// GUID of the timer
    /// </summary>
    public string Guid { get; set; }
    /// <summary>
    /// Text for the timer.
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// Duration of the timer.
    /// </summary>
    public int Duration { get; set; }
    /// <summary>
    /// Duration thats be displayed in the GUI.
    /// </summary>
    public string DurationString
    {
        get
        {
            if (Duration >= Timers.DurationMultiplier["Tage"])
            {
                return Duration / Timers.DurationMultiplier["Tage"] + " Tage";
            }

            if (Duration >= Timers.DurationMultiplier["SR"])
            {
                return Duration / Timers.DurationMultiplier["SR"] + " SR";
            }

            return Duration / Timers.DurationMultiplier["KR"] + " KR";
        }
    }
}
