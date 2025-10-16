using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace Magehelper.Models;

public class Updater
{
    public bool CheckForUpdates()
    {
        try
        {
#pragma warning disable SYSLIB0014
            WebClient webClient = new();
#pragma warning restore SYSLIB0014
            Version lastVersion = JsonSerializer.Deserialize<Version>(webClient.DownloadString("https://api.jungbluthcloud.de/updates/magehelper/version"));
            System.Version? currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

            if (currentVersion!.Major > lastVersion.Major)
            {
                return false;
            }

            if (currentVersion.Minor > lastVersion.Minor)
            {
                return false;
            }

            return currentVersion.Build <= lastVersion.Build;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void Update()
    {
        Process.Start(Path.Combine(AppContext.BaseDirectory, "updater.exe"));
        Application.Current.Exit();
    }
}
