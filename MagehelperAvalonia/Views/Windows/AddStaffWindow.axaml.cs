using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Magehelper.Avalonia.Views.Windows;

public partial class AddStaffWindow : Window
{
    public AddStaffWindow()
    {
        DataContext = new AddStaffWindowViewModel();
        InitializeComponent();
    }
}