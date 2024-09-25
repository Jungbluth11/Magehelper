using Avalonia.Controls;

namespace Magehelper.Avalonia.ViewModels.Windows
{
    public partial class AddStaffWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _pasp = 0;
        [ObservableProperty]
        private string _length;
        [ObservableProperty]
        private string _material;
        public IEnumerable<string> LengthStrings { get; }
        public IEnumerable<string> MaterialStrings { get; }

        public AddStaffWindowViewModel()
        {
            LengthStrings = Staff.LengthStrings;
            MaterialStrings = Staff.MaterialStrings;
            Length = LengthStrings.First();
            Material = MaterialStrings.First();
        }

        [RelayCommand]
        private void Submit(Window window)
        {
            TabContentArtifactViewModel.Instance.AddStaff(Length, Material, Pasp);
            window.Close();
        }
    }
}
