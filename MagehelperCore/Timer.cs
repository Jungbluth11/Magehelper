namespace Magehelper.Core
{
    /// <summary>
    /// Represents a timer.
    /// </summary>
    public struct Timer
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
                    return (Duration / Timers.DurationDaysMultiplier).ToString() + " Tage";
                }
                else if (Duration >= Timers.DurationSRMultiplier)
                {
                    return (Duration / Timers.DurationSRMultiplier).ToString() + " SR";
                }
                else
                {
                    return (Duration / Timers.DurationKRMultplier).ToString() + " KR";
                }
            }
        }
    }
}