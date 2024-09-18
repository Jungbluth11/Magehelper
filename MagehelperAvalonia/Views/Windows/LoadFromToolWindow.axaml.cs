using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Magehelper.Avalonia.Views.Windows;

public partial class LoadFromToolWindow : Window
{
    public LoadFromToolWindow()
    {
        DataContext = new LoadFromToolWindowViewModel();
        InitializeComponent();
    }
}