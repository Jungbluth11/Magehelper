using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Windows;

public partial class AddSpellWindow : Window
{
    public AddSpellWindow()
    {
        DataContext = new AddSpellWindowViewModel();
        InitializeComponent();
    }
}