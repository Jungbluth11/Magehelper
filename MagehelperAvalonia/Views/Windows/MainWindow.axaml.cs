using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Magehelper.Avalonia.Views.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ArtifactSettingsControl ArtifactSettingsControl = new([new ArtifactSpell { Name = "test", Cost = 1, Type = "test" }, new ArtifactSpell { Name = "test2", Cost = 1, Type = "test" }]);
        this.Content = ArtifactSettingsControl;
    }
}