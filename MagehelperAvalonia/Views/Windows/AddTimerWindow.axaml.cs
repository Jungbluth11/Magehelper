using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Magehelper.Avalonia.Views.Windows;

public partial class AddTimerWindow : Window
{
    public AddTimerWindow()
    {
        DataContext = new AddTimerWindowViewModel();
        InitializeComponent();
    }
}