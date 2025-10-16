using Timer = Magehelper.Core.Timer;

namespace Magehelper.ViewModels.Tabs;

public partial class TabTimerViewModel : ObservableObject, IRecipient<FileActionMessage>, IRecipient<AddTimerMessage>
{
    private readonly Timers _timers;
    public ObservableCollection<Timer> TimersLeft { get; set; } = [];
    public ObservableCollection<Timer> TimersRight { get; set; } = [];

    public TabTimerViewModel()
    {
        _timers = Core.Core.GetInstance().Timers ?? [];
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    public void Receive(AddTimerMessage message)
    {
        AddTimer(message.Name, message.Duration);
    }

    public void Receive(FileActionMessage message)
    {
        switch (message.Value)
        {
            case FileAction.New:
                ResetTab();

                break;
            case FileAction.Loaded:
                ResetTab();

                foreach (Timer timer in _timers)
                {
                    AddTimer(timer);
                }

                break;
        }
    }

    public void AddTimer(string name, int duration)
    {
        Timer timer = _timers.Add(name, duration);
        AddTimer(timer);
    }

    public void AddTimer(Timer timer)
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

    public void ResetTab()
    {
        TimersLeft.Clear();
        TimersRight.Clear();
    }

    [RelayCommand]
    private void DecreaseDay(Timer timer)
    {
        _timers.CountDown(timer.Guid, Timers.DurationDaysMultiplier);

        if (timer.Duration - Timers.DurationDaysMultiplier > 0)
        {
            TimersRight[TimersRight.IndexOf(timer)] = _timers[timer.Guid];
        }
        else
        {
            TimersRight.Remove(timer);
        }
    }

    [RelayCommand]
    private void DecreaseKr(Timer timer)
    {
        _timers.CountDown(timer.Guid, Timers.DurationKrMultiplier);

        if (timer.Duration - Timers.DurationKrMultiplier > 0)
        {
            TimersLeft[TimersLeft.IndexOf(timer)] = _timers[timer.Guid];
        }
        else
        {
            TimersLeft.Remove(timer);
        }
    }

    [RelayCommand]
    private void RemoveTimer(Timer timer)
    {
        if (timer.Duration < Timers.DurationDaysMultiplier)
        {
            TimersLeft.Remove(timer);
        }
        else
        {
            TimersRight.Remove(timer);
        }

        _timers.Remove(timer.Guid);
    }
}