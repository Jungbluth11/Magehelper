using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Windows;

public partial class AddStaffSpellWindow : Window
{
    public AddStaffSpellWindow(Staff staff)
    {
        DataContext = new AddStaffSpellWindowViewModel(staff);
        InitializeComponent();
    }
}