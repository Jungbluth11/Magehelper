using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Windows;

public partial class AddCrystalBallSpellWindow : Window
{
    public AddCrystalBallSpellWindow(CrystalBall crystalBall)
    {
        DataContext = new AddCrystalBallSpellWindowViewModel(crystalBall);
        InitializeComponent();
    }
}