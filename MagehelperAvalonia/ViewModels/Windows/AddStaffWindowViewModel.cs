using Avalonia.Controls;

namespace Magehelper.Avalonia.ViewModels.Windows
{
    public partial class AddStaffWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _crystalBallMaterial = 0;
        [ObservableProperty]
        private int _pasp = 0;
        [ObservableProperty]
        private string _length;
        [ObservableProperty]
        private string _staffMaterial;
        [ObservableProperty]
        private bool _isCrystalBallVisible = false;
        public IEnumerable<string> LengthStrings { get; }
        public IEnumerable<string> StaffMaterialStrings { get; }
        public IEnumerable<string> CrystalBallMaterialStrings { get; }

        public AddStaffWindowViewModel()
        {
            LengthStrings = Staff.LengthStrings;
            StaffMaterialStrings = Staff.MaterialStrings;
            CrystalBallMaterialStrings = CrystalBall.MaterialStrings;
            Length = LengthStrings.First();
            StaffMaterial = StaffMaterialStrings.First();
        }

        partial void OnLengthChanged(string value)
        {
            if (Length == "Magierstab m. Kristallkugel")
            {
                IsCrystalBallVisible = true;
            }
            else
            {
                IsCrystalBallVisible = false;
            }
        }

        [RelayCommand]
        private void Submit(Window window)
        {
            TabContentArtifactViewModel.Instance.AddStaff(Length, StaffMaterial, Pasp);
            if (Length == "Magierstab m. Kristallkugel")
            {
                TabContentArtifactViewModel.Instance.AddCrystalBall((CrystalBallMaterial)CrystalBallMaterial);
            }
            window.Close();
        }
    }
}
