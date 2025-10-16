namespace Magehelper.ViewModels.Dialogs;

public partial class AddConfigDialogViewModel : ObservableObject
{
    private readonly Settings _settings = Settings.GetInstance();
    [ObservableProperty] private bool _isConfigNameExistErrorVisible;

    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    [ObservableProperty]
    private string _configName = string.Empty;

    private bool CanAdd()
    {
        return !string.IsNullOrWhiteSpace(ConfigName) && !IsConfigNameExistErrorVisible;
    }

    partial void OnConfigNameChanged(string value)
    {
        IsConfigNameExistErrorVisible = _settings.ConfigNames.Contains(value, StringComparer.OrdinalIgnoreCase);
    }

    [RelayCommand(CanExecute = nameof(CanAdd))]
    private void Add()
    {
        _settings.AddConfig(ConfigName);
        WeakReferenceMessenger.Default.Send(new ConfigAddedMessage(ConfigName));
    }
}