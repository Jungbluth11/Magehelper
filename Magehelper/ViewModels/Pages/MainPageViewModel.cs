namespace Magehelper.ViewModels.Pages;

public partial class MainPageViewModel : ObservableObject
{
    public bool CanLoadCharacterFromTool => HeldentoolInterop.IsInstalled();
    [ObservableProperty] private string _loadedCharacter = string.Empty;
    public Settings Settings => Settings.GetInstance();
    private readonly Core.Core _core = Core.Core.GetInstance();

    public MainPageViewModel()
    {
        Settings.LoadConfig(Settings.CurrentConfigName);
        _core.SettingsPath = Settings.CurrentSettingsPath;
        _core.SpellStoragePoints = Settings.SpellStoragePoints;
        _core.OnFileChanged += Core_OnFileChanged;
    }

    private void Core_OnFileChanged(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new FileActionMessage(FileAction.Changed));
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

    public string WarnOtherVersionFilesMessage(string path)
    {
        // ReSharper disable once ConvertIfStatementToReturnStatement --- improves readability
        if (_core.GetFileVersion(path) == "0")
        {
            return "Achtung! Diese Datei wurde mit einer Version vor 3.0.0 erstellt. Wenn sie gespeichert wird kann nicht wieder in Magehelper vor Version 3.0.0 geöffnet werden!";
        }
        return "Achtung! Die Datei wurde mit einer anderen Version von Magehelper erstellt.\nFalls diese Datei mit einer neueren Version erstellt wurde, werden eventuell nicht alle Funktionen unterstützt und es können Daten verloren gehen wenn sie gespeichert wird.";
    }
}
