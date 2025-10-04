using System.Reflection;
using Microsoft.UI.Xaml.Media.Imaging;
using Version = System.Version;

namespace Magehelper.Views.Dialogs;

public sealed partial class AboutDialog : ContentDialog
{
    public AboutDialog()
    {
        Version version = Assembly.GetExecutingAssembly().GetName().Version!;
        InitializeComponent();
        ProgramNameAndVersion.Text = $"Metatalente {version.Major}.{version.Minor}.{version.Build}";
        BitmapImage bi = new();
        bi.SetSource(DSAUtils.UI.Logo.Getlogo);
        Logo.Source = bi;
        Disclaimer.Text = DSAUtils.UI.Disclaimer.GetDisclaimer;
    }
}
