using Timer = Magehelper.Core.Timer;

namespace Magehelper.ViewModels.Controls;

public partial class TimerControlViewModel : ObservableObject
{
    private readonly string _durationMultiplier;
    private readonly Timers _timers = Core.Core.Instance.Timers!;
    private int _duration;
    [ObservableProperty] private string _durationString;
    [ObservableProperty] private string _text;
    public string ButtonText { get; }
    public string Guid { get; }

    public TimerControlViewModel(Timer timer)
    {
        Guid = timer.Guid;
        Text = timer.Text;
        DurationString = timer.DurationString;
        _duration = timer.Duration;

        if (timer.Duration >= 28800)
        {
            _durationMultiplier = "Tage";
            ButtonText = "einen Tag weiter";
        }
        else
        {
            _durationMultiplier = "KR";
            ButtonText = "eine KR weiter";
        }
    }

    [RelayCommand]
    private void Decrease()
    {
        _timers.CountDown(Guid, Timers.DurationMultiplier[_durationMultiplier]);

        if (_duration - Timers.DurationMultiplier[_durationMultiplier] > 0)
        {
            Timer t = _timers[Guid];
            _duration = t.Duration;
            DurationString = t.DurationString;
        }
        else
        {
            RemoveTimer();
        }
    }

    [RelayCommand]
    private void RemoveTimer()
    {
        WeakReferenceMessenger.Default.Send(new RemoveTimerMessage(this));
    }
}
