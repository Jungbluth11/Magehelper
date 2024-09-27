using Avalonia.Controls;

namespace Magehelper.Avalonia.ViewModels.Windows
{
    public partial class AddCrystalBallWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _selectedIndex = 0;
        public IEnumerable<string> Materials => CrystalBall.MaterialStrings;

        [RelayCommand]
        private void Submit(Window window)
        {
            TabContentArtifactViewModel.Instance.AddCrystalBall((CrystalBallMaterial)SelectedIndex);
            window.Close();
        }
    }
}
