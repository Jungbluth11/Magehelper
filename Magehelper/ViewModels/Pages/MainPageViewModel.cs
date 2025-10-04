namespace Magehelper.ViewModels.Pages;

public partial class MainPageViewModel : ObservableObject
{
    public bool CanLoadCharacterFromTool => HeldentoolInterop.IsInstalled();
    [ObservableProperty] private string _loadedCharacter = string.Empty;
    public Settings Settings { get; }
    private readonly Core.Core _core = Core.Core.GetInstance();

    public MainPageViewModel()
    {
        Settings = new();
        _core.SettingsPath = Settings.CurrentSettingsPath;
        _core.SpellStoragePoints = Settings.SpellStoragePoints;

    }

    public void NewFile()
    {
        _core.ResetTool();
        WeakReferenceMessenger.Default.Send(new FileActionMessage(FileAction.New));
    }

    public void LoadFile(string path)
    {
        _core.ReadFileVersionSelector(path);
        WeakReferenceMessenger.Default.Send(new FileActionMessage(FileAction.Loaded));
    }

    public void SaveFile(string path)
    {
        if (string.IsNullOrEmpty(_core.FileName))
        {
            _core.FileName = path;
        }
        _core.WriteFile();
    }

    public string WarnOtherVersionFilesMessage(bool isLegacy)
    {
        // ReSharper disable once ConvertIfStatementToReturnStatement --- improves readability
        if (isLegacy)
        {
            return "Achtung! Diese Datei wurde mit einer Version vor 3.0.0 erstellt. Wenn sie gespeichert wird kann nicht wieder in Magehelper vor Version 3.0.0 geöffnet werden!";
        }
        return "Achtung! Die Datei wurde mit einer anderen Version von Magehelper erstellt.\nFalls diese Datei mit einer neueren Version erstellt wurde, werden eventuell nicht alle Funktionen unterstützt und es können Daten verloren gehen wenn sie gespeichert wird.";
    }
}
