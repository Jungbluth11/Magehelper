using Timer = Magehelper.Core.Timer;

namespace Magehelper.ViewModels.Tabs;

public partial class TabTimerViewModel : ObservableObject,
    IRecipient<FileActionMessage>,
    IRecipient<AddTimerMessage>,
    IRecipient<RemoveTimerMessage>
{
    private readonly Timers _timers;
    public ObservableCollection<TimerControlViewModel> TimersLeft { get; set; } = [];
    public ObservableCollection<TimerControlViewModel> TimersRight { get; set; } = [];

    public TabTimerViewModel()
    {
        _timers = Core.Core.Instance.Timers ?? [];
        LoadTabContents();
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    private void LoadTabContents()
    {
        foreach (Timer timer in _timers)
        {
            AddTimer(timer);
        }
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
                LoadTabContents();

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
        if (timer.Duration < Timers.DurationMultiplier["Tage"])
        {
            TimersLeft.Add(new(timer));
        }
        else
        {
            TimersRight.Add(new(timer));
        }
    }

    public void ResetTab()
    {
        TimersLeft.Clear();
        TimersRight.Clear();
    }

    public void Receive(RemoveTimerMessage message)
    {
        _timers.Remove(message.Value.Guid);

        if (message.Value.DurationString.Contains("Tage"))
        {
            TimersRight.Remove(message.Value);
        }
        else
        {
            TimersLeft.Remove(message.Value);
        }
    }
}
