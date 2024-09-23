using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Windows;

public partial class EnableSpellStorageWindow : Window
{
    public EnableSpellStorageWindow(int points)
    {
        DataContext = new EnableSpellStorageWindowViewModel(points);
        InitializeComponent();
    }
}