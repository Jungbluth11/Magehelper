using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Windows;

public partial class DialogNumberWindow : Window
{
    public DialogNumberWindow(string caption, string dialogText, Action<int> action, bool btnRollVisible = false, string btnRollName = "", Func<int>? btnRollFunc = null)
    {
        DataContext = new DialogNumberWindowViewModel(caption, dialogText, action, btnRollVisible, btnRollName);
        InitializeComponent();
    }
}