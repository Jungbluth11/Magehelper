using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Magehelper.UrlHandler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filename = Regex.Match(args[0], "name=([^&]+)").Groups[1].Value;
            string data = Encoding.UTF8.GetString(Convert.FromBase64String(Regex.Match(args[0], "data=(.+)$").Groups[1].Value));
            string tmpfilepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "magehelper", "downloads", filename);
            File.WriteAllText(tmpfilepath,data);
            Process.Start(Path.Combine(AppContext.BaseDirectory, "magehelper.exe"),tmpfilepath);
            File.Delete(tmpfilepath);
        }
    }
}
