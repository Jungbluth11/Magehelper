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
            if (Duration >= Timers.DurationDaysMultiplier)
            {
                return (Duration / Timers.DurationDaysMultiplier) + " Tage";
            }

            if (Duration >= Timers.DurationSrMultiplier)
            {
                return (Duration / Timers.DurationSrMultiplier) + " SR";
            }

            return (Duration / Timers.DurationKrMultiplier) + " KR";
        }
    }
}
