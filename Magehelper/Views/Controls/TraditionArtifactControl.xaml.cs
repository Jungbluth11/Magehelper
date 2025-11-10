namespace Magehelper.Views.Controls;

public sealed partial class TraditionArtifactControl : UserControl
{
    private TraditionArtifactControlViewModel ViewModel => (DataContext as TraditionArtifactControlViewModel)!;

    public TraditionArtifactControl()
    {
        InitializeComponent();
    }

    private async void AddArtifact_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            AddArtifactSpellDialog dialog = new()
            {
                XamlRoot = XamlRoot,
                DataContext = ViewModel.AddArtifactSpellDialogViewModel
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private void TraditionArtifactControl_OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        UserControl control = ViewModel.Artifact switch
        {
            Bowl => new BowlControl(),
            BoneCub => new BoneCubControl(),
            CrystalBall => new CrystalBallControl(),
            Staff => new StaffControl(),
            RingOfLife => new RingOfLifeControl(),
            _ => new ObsidianDaggerControl()
        };

        Grid.Children.Add(control);
        Grid.SetColumn(control, 0);
        Grid.SetRow(control, 1);
        Grid.SetRowSpan(control, 2);
    }
}
