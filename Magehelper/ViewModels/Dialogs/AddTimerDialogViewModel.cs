namespace Magehelper.ViewModels.Dialogs;

public partial class AddTimerDialogViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private int _durationValue;

    [ObservableProperty] private string _durationUnit;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _name = string.Empty;

    public string[] DurationUnits => Timers.DurationUnits;

    public AddTimerDialogViewModel()
    {
        DurationUnit = DurationUnits[0];
    }

    private bool CanSubmit()
    {
        return !string.IsNullOrWhiteSpace(Name) && DurationValue > 0;
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private void Submit()
    {
        switch (DurationUnit)
        {
            case "KR":
                DurationValue *= Timers.DurationKrMultiplier;

                break;
            case "SR":
                DurationValue *= Timers.DurationSrMultiplier;

                break;
            case "Tage":
                DurationValue *= Timers.DurationDaysMultiplier;

                break;
        }

        WeakReferenceMessenger.Default.Send(new AddTimerMessage(Name, DurationValue));
    }
}