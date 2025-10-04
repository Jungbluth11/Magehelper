namespace Magehelper.ViewModels.Tabs;

public partial class TabContentTimerViewModel : ObservableObject
{
    private readonly Timers timers;
    private static TabContentTimerViewModel _instance = new();
    public static TabContentTimerViewModel Instance => _instance;
    public ObservableCollection<Core.Timer> TimersLeft { get; set; } = [];
    public ObservableCollection<Core.Timer> TimersRight { get; set; } = [];

    public TabContentTimerViewModel()
    {
        timers = new Timers(MainWindowViewModel.Instance.Core);
        MainWindowViewModel.Instance.Core.AddTimerGUIAction = AddTimer;
    }

    public void ResetTab()
    {
        TimersLeft.Clear();
        TimersRight.Clear();
    }

    public void AddTimer(string name, int duration)
    {
        Core.Timer timer = timers.Add(name, duration);
        AddTimer(timer);
    }

    public void AddTimer(Core.Timer timer)
    {
        if (timer.Duration < Timers.DurationDaysMultiplier)
        {
            TimersLeft.Add(timer);
        }
        else
        {
            TimersRight.Add(timer);
        }
    }

    [RelayCommand]
    private void RemoveTimer(Core.Timer timer)
    {
        if (timer.Duration < Timers.DurationDaysMultiplier)
        {
            TimersLeft.Remove(timer);
        }
        else
        {
            TimersRight.Remove(timer);
        }
        timers.Remove(timer.Guid);
    }

    [RelayCommand]
    private void DecreaseKR(Core.Timer timer)
    {
        timers.CountDown(timer.Guid, Timers.DurationKrMultiplier);
        if (timer.Duration - Timers.DurationKrMultiplier > 0)
        {
            TimersLeft[TimersLeft.IndexOf(timer)] = timers[timer.Guid];
        }
        else
        {
            TimersLeft.Remove(timer);
        }
    }

    [RelayCommand]
    private void DecreaseDay(Core.Timer timer)
    {
        timers.CountDown(timer.Guid, Timers.DurationDaysMultiplier);
        if (timer.Duration - Timers.DurationDaysMultiplier > 0)
        {
            TimersRight[TimersRight.IndexOf(timer)] = timers[timer.Guid];
        }
        else
        {
            TimersRight.Remove(timer);
        }
    }
}
