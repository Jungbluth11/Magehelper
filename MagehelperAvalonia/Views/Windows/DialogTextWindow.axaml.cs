using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Windows;

public partial class DialogTextWindow : Window
{
    public DialogTextWindow(string caption, string dialogText, Action<string> action)
    {
        DataContext = new DialogTextWindowViewModel(caption, dialogText, action);
        InitializeComponent();
    }
}