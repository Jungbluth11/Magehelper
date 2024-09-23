using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Magehelper.Avalonia.Views.Windows;

public partial class AddArtifactWindow : Window
{
    public AddArtifactWindow()
    {
        DataContext = new AddArtifactWindowViewModel();
        InitializeComponent();
    }
}