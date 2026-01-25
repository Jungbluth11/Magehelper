namespace Magehelper.Views.Tabs;

public sealed partial class TabPet : TabViewItem
{
    private TabPetViewModel ViewModel => (TabPetViewModel) DataContext;

    public TabPet()
    {
        InitializeComponent();
    }

    private void ShowMaximumReachedMessageBox()
    {
        ContentDialog dialog = new()
        {
            Content = "Maximum erreicht",
            XamlRoot = XamlRoot,
            CloseButtonText = "OK"
        };

#pragma warning disable CS4014 // no need for waiting
        dialog.ShowAsync();
#pragma warning restore CS4014
    }

    private void UIElement_OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            XamlRoot!.Content!.Focus(FocusState.Programmatic);
        }
    }

    private async void Button_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (ViewModel.ButtonText == "Vertrauten binden")
            {
                (XamlRoot!.Content as Frame)!.Navigate(typeof(PetGeneratorPage));

            }
            else
            {
                AddPetSpellDialog dialog = new()
                {
                    XamlRoot = XamlRoot
                };

                await dialog.ShowAsync();
            }
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private void BtnIncreaseMU_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("MU"))
        {
            ViewModel.Mu++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseKL_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("KL"))
        {
            ViewModel.Kl++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseIN_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("IN"))
        {
            ViewModel.In++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseCH_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("CH"))
        {
            ViewModel.Ch++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseFF_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("FF"))
        {
            ViewModel.Ff++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseGE_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("GE"))
        {
            ViewModel.Ge++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseKO_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("KO"))
        {
            ViewModel.Ko++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseKK_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("KK"))
        {
            ViewModel.Kk++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseMR_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("MR"))
        {
            ViewModel.Mr++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseRkw_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("RKW"))
        {
            ViewModel.Rkw++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseAttack_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("Attack"))
        {
            ViewModel.IncreaseAttack();
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseParry_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("Parry"))
        {
            ViewModel.IncreaseParry();
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseGs_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("GS"))
        {
            ViewModel.IncreaseGs();
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseAup_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("AU"))
        {
            ViewModel.Aup++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseLep_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("LE"))
        {
            ViewModel.Lep++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private void BtnIncreaseAsp_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IncreaseAttribute("AE"))
        {
            ViewModel.Asp++;
        }
        else
        {
            ShowMaximumReachedMessageBox();
        }
    }

    private async void BtnRollSpell_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            (int pointsLeft, int[] rollData, string textResult) = ViewModel.RollSpell((PetSpell) (sender as FrameworkElement)!.Tag!);

            DiceResultDialog dialog = new()
            {
                XamlRoot = XamlRoot,
                DiceResults = rollData,
                AdditionalText = $"{textResult} {pointsLeft} Punkte über"
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }
}
