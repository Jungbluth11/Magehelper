namespace Magehelper.Views.Dialogs;

public sealed partial class AddArtifactSpellDialog : ContentDialog
{
    public AddArtifactSpellDialog()
    {
        InitializeComponent();
    }

    private void AddArtifactSpellDialog_OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        AddArtifactSpellDialogViewModel viewModel = (DataContext as AddArtifactSpellDialogViewModel)!;

        UserControl? control = viewModel switch
        {
            AddBoneCubSpellDialogViewModel => new AddBoneCubSpellControl(),
            AddCrystalBallSpellDialogViewModel => new AddCrystalBallSpellControl(),
            AddStaffSpellDialogViewModel => new AddStaffSpellControl(),
            _ => null
        };

        if (control == null)
        {
            return;
        }

        control.DataContext = viewModel.AddArtifactSpellControlViewModel;
        Grid.Children.Add(control);
        Grid.SetRow(control, 1);

    }
}
