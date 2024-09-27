#nullable disable
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;

namespace Magehelper.Avalonia.Views.Tabs;

public partial class TabContentArtifact : UserControl
{
    public TabContentArtifact()
    {
        DataContext = TabContentArtifactViewModel.Instance;
        InitializeComponent();
    }

    public void ResetTab()
    {
        StackPanelArtifacts.Children.Clear();
        TabContentArtifactViewModel.Instance.ResetTab();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        Window mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow;
        AddArtifactWindow addArtifactWindow = new();
        string artifact = await addArtifactWindow.ShowDialog<string>(mainWindow);
        switch (artifact)
        {
            case "Magierstab":
                await new AddStaffWindow().ShowDialog(mainWindow);
                break;
            case "Kristallkugel":
                await new AddCrystalBallWindow().ShowDialog(mainWindow);
                break;
            default:
                TabContentArtifactViewModel.Instance.AddArtifact(artifact);
                break;
        }
        switch (artifact)
        {
            case "Alchemistenschale":
                StackPanelArtifacts.Children.Add(new ArtifactControl(artifact, TabContentArtifactViewModel.Instance.BowlControl));
                break;
            case "Knochenkeule":
                StackPanelArtifacts.Children.Add(new ArtifactControl(artifact, TabContentArtifactViewModel.Instance.BoneCubControl));
                break;
            case "Kristallkugel":
                StackPanelArtifacts.Children.Add(new ArtifactControl(artifact, TabContentArtifactViewModel.Instance.CrystalBallControl));
                break;
            case "Magierstab":
                StackPanelArtifacts.Children.Add(new ArtifactControl(artifact, TabContentArtifactViewModel.Instance.StaffControl));
                break;
            case "Ring des Lebens":
                StackPanelArtifacts.Children.Add(new ArtifactControl(artifact, TabContentArtifactViewModel.Instance.RingOfLifeControl));
                break;
            case "Vulkanglasdolch":
                StackPanelArtifacts.Children.Add(new ArtifactControl(artifact, TabContentArtifactViewModel.Instance.ObsidianDaggerControl));
                break;
        }
    }
}