namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class StaffControlViewModel(Staff staff, MainWindowViewModel mainWindowViewModel, ArtifactSpellsControlViewModel artifactSpellsControlViewModel) : ObservableObject
    {
        private readonly Staff staff = staff;
        private readonly MainWindowViewModel mainWindowViewModel = mainWindowViewModel;
        private readonly ArtifactSpellsControlViewModel artifactSpellsControlViewModel = artifactSpellsControlViewModel;

        public string Length { get; } = Staff.LengthStrings[staff.Length];
        public string Material { get; } = Staff.MaterialStrings[staff.Material];
        public string Pasp { get; } = staff.Pasp.ToString();

        public ArtifactSpell? AddSpell()
        {
            //AddStaffSpellWindow addStaffSpellWindow = new AddStaffSpellWindow(staff);
            //if (addStaffSpellWindow.ShowDialog() == true)
            //{
            //    try
            //    {
            //        ArtifactSpell artifactSpell = staff.AddSpell(addStaffSpellWindow.SpellName, addStaffSpellWindow.SpellCharacteristic, addStaffSpellWindow.SpellPoints);
            //        if (addStaffSpellWindow.SpellName == "Zauberspeicher")
            //        {
            //            new EnableSpellStorageWindow(addStaffSpellWindow.SpellPoints, mainWindowViewModel).ShowDialog();
            //            if (mainWindowViewModel.TabContentSpellStorage != null)
            //            {
            //                mainWindowViewModel.TabContentSpellStorage.EnableTab();
            //            }
            //        }
            //        else if (addStaffSpellWindow.SpellName == "Flammenschwert" && mainWindowViewModel.TabContentFlameSword != null)
            //        {
            //            mainWindowViewModel.TabContentFlameSword.EnableTab();
            //        }
            //        return artifactSpell;
            //    }
            //    catch (Exception e)
            //    {
            //        ErrorMessages.Error(e.Message);
            //    }
            //}
            return null;
        }

        [RelayCommand]
        private void Apport(bool? isChecked)
        {
            staff.HasApport = (bool)isChecked;
            staff.AfvTotal();
            artifactSpellsControlViewModel.SetSpellCounter();
        }
        [RelayCommand]
        private void FlameSwordFour(bool? isChecked)
        {
            staff.IsFlameSwordFour = (bool)isChecked;
            staff.AfvTotal();
            artifactSpellsControlViewModel.SetSpellCounter();
        }
        [RelayCommand]
        private void FlameSwordFive(bool? isChecked)
        {
            staff.IsFlameSwordFive = (bool)isChecked;
            staff.AfvTotal();
            artifactSpellsControlViewModel.SetSpellCounter();
        }
    }
}
