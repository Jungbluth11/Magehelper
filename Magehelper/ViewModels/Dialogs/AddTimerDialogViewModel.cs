

namespace Magehelper.ViewModels.Dialogs;

public partial class AddTimerWindowViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _name = string.Empty;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _durationValue = string.Empty;
    [ObservableProperty]
    private string _durationUnit = string.Empty;

    private bool CanSubmit()
    {
        try
        {
            int.Parse(DurationValue);
        }
        catch
        {
            return false;
        }
        return !string.IsNullOrWhiteSpace(Name);
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private void Submit(Window window)
    {
        int duration = int.Parse(DurationValue);

        switch (DurationUnit)
        {
            case "KR":
                duration *= Timers.DurationKrMultiplier;
                break;
            case "SR":
                duration *= Timers.DurationSrMultiplier;
                break;
            case "Tage":
                duration *= Timers.DurationDaysMultiplier;
                break;
        }

        TabContentTimerViewModel.Instance.AddTimer(Name, duration);
        window.Close();
    }

    [RelayCommand]
    private void Cancel(Window window)
    {
        window.Close();
    }
}
