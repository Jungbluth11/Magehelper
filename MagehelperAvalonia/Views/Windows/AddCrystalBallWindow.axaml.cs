using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Magehelper.Avalonia.Views.Windows;

public partial class AddCrystalBallWindow : Window
{
    public AddCrystalBallWindow()
    {
        DataContext = new AddCrystalBallWindowViewModel();
        InitializeComponent();
    }
}