using Avalonia.Controls;
using Avalonia.Interactivity;
using DSAUtils.UI;

namespace Magehelper.Avalonia.Views.Tabs;

public partial class TabContentPet : UserControl
{
    private readonly TabContentPetViewModel viewModel;
    private readonly Window mainWindow;
    public TabContentPet(Window mainWindow)
    {
        this.mainWindow = mainWindow;
        viewModel = TabContentPetViewModel.Instance;
        DataContext = viewModel;
        InitializeComponent();

    }

    public void ResetTab()
    {
        viewModel.ResetTab();
    }

    private async void ShowMaximumReachedMessageBox()
    {
        await MessageBoxGenerator.GetMessageBox("Maximum erreicht", MessageBoxGenerator.Buttons.OK).ShowAsync();
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        if(viewModel.ButtonText == "Vertrauten binden")
        {
            PetGeneratorWindow petGeneratorWindow = new(viewModel.Pet, viewModel.AddPet);
            petGeneratorWindow.ShowDialog(mainWindow);
        }
        else
        {
            AddPetSpellWindow addPetSpellWindow = new(viewModel.Pet);
            addPetSpellWindow.ShowDialog(mainWindow);
        }
    }

    private void BtnIncreaseMU_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("MU"))
        {
            viewModel.Mu++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseKL_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("KL"))
        {
            viewModel.Kl++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseIN_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("IN"))
        {
            viewModel.In++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseCH_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("CH"))
        {
            viewModel.Ch++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseFF_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("FF"))
        {
            viewModel.Ff++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseGE_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("GE"))
        {
            viewModel.Ge++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseKO_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("KO"))
        {
            viewModel.Ko++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseKK_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("KK"))
        {
            viewModel.Kk++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseMR_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("MR"))
        {
            viewModel.Mr++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseRkw_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("RKW"))
        {
            viewModel.Rkw++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseAttack_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("Attack"))
        {
            viewModel.IncreaseAttack();
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseParry_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("Parry"))
        {
            viewModel.IncreaseParry();
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseGs_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("GS"))
        {
            viewModel.IncreaseGs();
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseAup_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("AU"))
        {
            viewModel.Aup++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseLep_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("LE"))
        {
            viewModel.Lep++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseAsp_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.IncreaseAttribute("AE"))
        {
            viewModel.Asp++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnSpell_Click(object sender, RoutedEventArgs e)
    {
#pragma warning disable CS8602
        (int, int[], string) result = viewModel.Pet.RollSpell(viewModel.Pet.KnownSpells.Single(c => c.Name == (sender as Button).Tag.ToString()));
        new DiceWindow(result).ShowDialog(mainWindow);
#pragma warning restore CS8602
    }
}