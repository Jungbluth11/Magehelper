namespace Magehelper.Views.Pages;

public sealed partial class SettingsPage : Page
{
    private SettingsPageViewModel ViewModel => (DataContext as SettingsPageViewModel)!;

    public SettingsPage()
    {
        InitializeComponent();
    }

    private async void BtnAddConfig_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            AddConfigDialog dialog = new()
            {
                XamlRoot = XamlRoot
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
    {
        Frame.GoBack();
    }

    private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (Settings.GetInstance().SettingsChanged)
            {
                ContentDialog saveDialog = new()
                {
                    XamlRoot = XamlRoot,
                    Content = "Sollen die Änderungen gespeichert werden?",
                    PrimaryButtonText = "Ja",
                    CloseButtonText = "Nein"
                };

                ContentDialogResult saveDialogResult = await saveDialog.ShowAsync();

                if (saveDialogResult != ContentDialogResult.Primary)
                {
                    return;
                }

                (DataContext as SettingsPageViewModel)!.Save();

                ContentDialog restartDialog = new()
                {
                    XamlRoot = XamlRoot,
                    Content = "Die Änderungen werden erst nach einem Neustart wirksam.\nSoll jetzt neu gestartet werden?",
                    PrimaryButtonText = "Ja",
                    CloseButtonText = "Nein"
                };

                ContentDialogResult restartDialogResult = await restartDialog.ShowAsync();

                if (restartDialogResult == ContentDialogResult.Primary)
                {
                    Environment.Exit(0);
                }
            }
            Frame.GoBack();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private void CheckBoxTabSettings_OnClick(object sender, RoutedEventArgs e)
    {
        CheckBox cb = (sender as CheckBox)!;
        ViewModel.SetTabSetting(cb.Content.ToString()!, (bool) cb.IsChecked!);
    }

    private void ToggleSwitchTheme_OnToggled(object sender, RoutedEventArgs e)
    {
        if (IsLoaded)
        {
            (XamlRoot!.Content as FrameworkElement)!.RequestedTheme = (sender as ToggleSwitch)!.IsOn
                ? ElementTheme.Dark
                : ElementTheme.Light;
        }
    }

    private void UIElement_OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            XamlRoot!.Content!.Focus(FocusState.Programmatic);
        }
    }

    private void NumberBoxHomeBrew_OnValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
    {
        if (!double.IsNaN(args.NewValue) && !double.IsNaN(args.OldValue))
        {
            ViewModel.SetSpellCost(sender.Tag.ToString()!, (int) args.NewValue);
        }
    }
}