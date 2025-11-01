namespace Magehelper.Views.Tabs;

public sealed partial class TabCharacter : TabViewItem
{
    private TabCharacterViewModel ViewModel => (TabCharacterViewModel)DataContext;

    public TabCharacter()
    {
        InitializeComponent();
    }

    private void UIElement_OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            XamlRoot!.Content!.Focus(FocusState.Programmatic);
        }
    }

    private async void ButtonRollSpell_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            CharacterSpell spell = (CharacterSpell)(sender as FrameworkElement)!.Tag;
            string? selectedAttribute = null;
            int? attributeIndex = null;

            for (int i = 0; i < 3; i++)
            {
                if (spell.Attributes[i] != "*")
                {
                    continue;
                }

                SelectAttributeDialog selectAttributeDialog = new()
                {
                    XamlRoot = XamlRoot
                };

                await selectAttributeDialog.ShowAsync();
                selectedAttribute = selectAttributeDialog.SelectedAttribute;
            }

            (int pointsLeft, int[] rollData, string textResult) = ViewModel.RollSpell(spell, selectedAttribute, attributeIndex);

            DiceResultDialog diceResultDialog = new()
            {
                XamlRoot = XamlRoot,
                DiceResults = rollData,
                AdditionalText = $"{textResult} {pointsLeft} Punkte über"
            };

            await diceResultDialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private async void ButtonRollRitual_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            (int pointsLeft, int[] rollData, string textResult) = ViewModel.RollRitual((CharacterRitual)((sender as FrameworkElement)!.Tag));

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

    private async void ButtonRoll1W20_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            (int[] wuerfelergebnisse, string text) = DSA.Probe(3);

            DiceResultDialog dialog = new()
            {
                XamlRoot = XamlRoot,
                DiceResults = wuerfelergebnisse,
                AdditionalText = text
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private async void ButtonRoll3W20_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            (int[] wuerfelergebnisse, string text) = DSA.Probe(3);

            DiceResultDialog dialog = new()
            {
                XamlRoot = XamlRoot,
                DiceResults = wuerfelergebnisse,
                AdditionalText = text
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private async void ButtonRollW6_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (e.OriginalSource == AmountW6)
            {
                return;
            }

            DiceResultDialog dialog = new()
            {
                XamlRoot = XamlRoot,
                DiceResults = DSA.Roll((int)AmountW6.Value, 20)
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }
}
