namespace Magehelper.Views.Controls;

public sealed partial class TraditionArtifactControl : UserControl
{
    public string Artifact
    {
        get => (string)GetValue(ArtifactProperty);
        set => SetValue(ArtifactProperty, value);
    }

    public static readonly DependencyProperty ArtifactProperty = DependencyProperty.Register(
        nameof(Artifact),
        typeof(string),
        typeof(TraditionArtifactControl),
        new(default(string),
            OnArtifactChanged));

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
                XamlRoot = XamlRoot
            };

            (dialog.DataContext as AddArtifactSpellDialogViewModel)!.Artifact =
                (DataContext as TraditionArtifactControlViewModel)!.Artifact;

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
    }

    private static void OnArtifactChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TraditionArtifactControl control = (d as TraditionArtifactControl)!;

        (control.DataContext as TraditionArtifactControlViewModel)!.Artifact =
            TraditionalArtifactHelper.GetArtifact[e.NewValue.ToString()!];

        switch (e.NewValue.ToString())
        {
            case "Alchemistenschale":
                control.Grid.Children.Add(new BowlControl());
                break;
            case "Knochenkeule":
                control.Grid.Children.Add(new BoneCubControl());
                break;
            case "Kristallkugel":
                control.Grid.Children.Add(new CrystalBallControl());
                break;
            case "Magierstab":
                control.Grid.Children.Add(new StaffControl());
                break;
            case "Ring des Lebens":
                control.Grid.Children.Add(new RingOfLifeControl());
                break;
            case "Vulkanglasdolch":
                control.Grid.Children.Add(new ObsidianDaggerControl());
                break;
        }
    }
}
