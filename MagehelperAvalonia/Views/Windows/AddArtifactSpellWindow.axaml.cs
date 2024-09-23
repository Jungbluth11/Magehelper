using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Windows;

public partial class AddArtifactSpellWindow : Window
{
    public AddArtifactSpellWindow(string artifactSpellName, Artifact artifact)
    {
        DataContext = new AddArtifactSpellWindowViewModel(artifactSpellName, artifact);
        InitializeComponent();
    }
}