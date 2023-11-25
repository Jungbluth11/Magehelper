namespace Magehelper.Core
{
    public class Timers
    {
        private readonly Core core;
        private readonly List<Timer> timers = new List<Timer>();
        public Timer this[int i] => timers[i];
        public Timer this[string guid] => timers.SingleOrDefault(t => t.Guid == guid);
        public int Count => timers.Count;
        public static int DurationKRMultplier => 1;
        public static int DurationSRMultiplier => 100;
        public static int DurationDaysMultiplier => 28800;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="core">An instance of <see cref="Core"/>.</param>
        public Timers(Core core)
        {
            this.core = core;
            core.Timers = this;
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
            if (guid is null)
            {
                guid = Guid.NewGuid().ToString();
            }
            Timer timer = new Timer { Guid = guid, Text = text, Duration = duration };
            timers.Add(timer);
            core.FileChanged = true;
            return timer;
        }

        /// <summary>
        /// Remove a timer with the given GUID.
        /// </summary>
        /// <param name="guid"></param>
        public void Remove(string guid)
        {
            timers.Remove(timers.Single(t => t.Guid == guid));
            core.FileChanged = true;
        }

        /// <summary>
        /// Remove all Timers.
        /// </summary>
        public void RemoveAll()
        {
            timers.Clear();
            core.FileChanged = true;
        }

        public void CountDown(string guid, int amount)
        {
            for (int i = 0; i < timers.Count; i++)
            {
                if (timers[i].Guid == guid)
                {
                    Timer timer = timers[i];
                    timer.Duration -= amount;
                    timers[i] = timer;
                    if (timer.Duration <= 0)
                    {
                        timers.RemoveAt(i);
                    }
                    core.FileChanged = true;
                    break;
                }
            }
        }

        public void CountDownAll(int amount, int minDuration)
        {
            foreach (Timer timer in timers)
            {
                if (timer.Duration >= minDuration)
                {
                    CountDown(timer.Guid, amount);
                }
            }
        }
    }
}