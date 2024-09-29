using Avalonia.Controls;
using Avalonia.Interactivity;
using DSAUtils.UI;

namespace Magehelper.Avalonia.Views.Tabs;

public partial class TabContentFlameSword : UserControl
{
    private readonly Window mainWindow;
    private TabContentFlameSwordViewModel viewModel;

    public TabContentFlameSword(Window mainWindow)
    {
        DataContext = TabContentFlameSwordViewModel.Instance;
        viewModel = TabContentFlameSwordViewModel.Instance;
        this.mainWindow = mainWindow;
        InitializeComponent();
    }

    public void ResetTab()
    {
        TabContentFlameSwordViewModel.Instance.ResetTab();
        viewModel.FlameSwordExist = false;
    }

    private async void Button_Click(object? sender, RoutedEventArgs e)
    {
        if (viewModel.ButtonText == "Aktivieren")
        {
            bool btnRollEnabled = false;
            if (TabContentCharacterViewModel.Instance.Character.IsLoaded)
            {
                btnRollEnabled = true;
            }
            DialogNumberWindow dialogNumberWindow = new("RkP* des Flammenschwerts", "RkP* des Flammenschwerts", viewModel.Enable, btnRollEnabled, "RkP* auswürfeln", RollRKP);
            bool result = await dialogNumberWindow.ShowDialog<bool>(mainWindow);
            if (result)
            {
                viewModel.ButtonText = "Deaktivieren";
            }
        }
        else
        {
            viewModel.ResetTab();
        }
    }

    private int RollRKP()
    {
        (int, int[], string) result = viewModel.FlameSword.RollActivation();
        new DiceWindow(result).ShowDialog(mainWindow);
        return result.Item1;
    }
}