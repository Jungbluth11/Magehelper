using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace Magehelper.Models;

public static class Updater
{
    public static bool CheckForUpdates()
    {
        //legacy support
        try
        {
#pragma warning disable SYSLIB0014 //Legacy WebClient is used here for simplicity
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

#pragma warning disable CS1998
    public static async Task Update()
    {
#if __UNO_SKIA_MACOS__
        Process.Start("open","https://api.jungbluthcloud.de/updates/magehelper/macos");
#else

        UpdateManager mgr = new("https://the.place/you-host/updates"); // TODO change to github repo
        UpdateInfo? newVersion = await mgr.CheckForUpdatesAsync();

        if (newVersion == null)
        {
            return;
        }

        await mgr.DownloadUpdatesAsync(newVersion);

        mgr.ApplyUpdatesAndRestart(newVersion);
#endif
    }
#pragma warning restore CS1998 
}
