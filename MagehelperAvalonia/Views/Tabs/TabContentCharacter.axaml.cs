#nullable disable
using Avalonia.Controls;
using Avalonia.Interactivity;
using DSAUtils;
using DSAUtils.UI;

namespace Magehelper.Avalonia.Views.Tabs;

public partial class TabContentCharacter : UserControl
{
    private readonly Window mainWindow;

    public TabContentCharacter(Window mainWindow)
    {
        DataContext = TabContentCharacterViewModel.Instance;
        this.mainWindow = mainWindow;
        InitializeComponent();
    }

    public void ResetTab()
    {
        TabContentCharacterViewModel.Instance.ResetTab();
    }

    private void BtnRoll1W20_Click(object sender, RoutedEventArgs e)
    {
        new DiceWindow(DSA.Probe(1)).ShowDialog(mainWindow);
    }

    private void BtnRoll3W20_Click(object sender, RoutedEventArgs e)
    {
        new DiceWindow(DSA.Probe(3)).ShowDialog(mainWindow);
    }

    private void BtnRollW6_Click(object sender, RoutedEventArgs e)
    {
        if (e.Source == AmountW6)
        {
            return;
        }
        new DiceWindow(DSA.Roll(Convert.ToInt32(AmountW6.Value), 6)).ShowDialog(mainWindow);
    }

    private void BtnSpell_Click(object sender, RoutedEventArgs e)
    {
        (int, int[], string) result = TabContentCharacterViewModel.Instance.Character.RollSpell((CharacterSpell)(sender as Button).Tag);
        new DiceWindow(result).ShowDialog(mainWindow);
    }

    private void BtnRitual_Click(object sender, RoutedEventArgs e)
    {
        (int, int[], string) result = TabContentCharacterViewModel.Instance.Character.RollRitual((CharacterRitual)(sender as Button).Tag);
        new DiceWindow(result).ShowDialog(mainWindow);
    }
}