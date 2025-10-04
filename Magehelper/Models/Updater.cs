using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text.Json;


namespace Magehelper.Models;

public class Updater
{
#pragma warning disable SYSLIB0014
    private readonly WebClient webClient = new();
#pragma warning restore SYSLIB0014

    public bool CheckForUpdates()
    {
        try
        {
            WebClient webClient = new();
            Version lastVersion = JsonSerializer.Deserialize<Version>(webClient.DownloadString("https://api.jungbluthcloud.de/updates/magehelper/version"));
            System.Version? currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            if (currentVersion.Major > lastVersion.Major)
            {
                return false;
            }
            else if (currentVersion.Minor > lastVersion.Minor)
            {
                return false;
            }
            else if (currentVersion.Build > lastVersion.Build)
            {
                return false;
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void Update()
    {
        try
        {
            Process.Start(Path.Combine(AppContext.BaseDirectory, "updater.exe"));
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp)
            {
                desktopApp.Shutdown();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
