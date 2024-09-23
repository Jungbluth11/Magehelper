using Avalonia.Controls;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class StaffControlViewModel : ObservableObject
    {
        private readonly Staff staff;
        private readonly ArtifactSpellsControlViewModel artifactSpellsControlViewModel;
        public string Length { get; }
        public string Material { get; }
        public string Pasp { get; }

        public StaffControlViewModel(Staff staff, ArtifactSpellsControlViewModel artifactSpellsControlViewModel)
        {
            this.staff = staff ?? throw new ArgumentNullException(nameof(staff));
            this.artifactSpellsControlViewModel = artifactSpellsControlViewModel ?? throw new ArgumentNullException(nameof(artifactSpellsControlViewModel));
            this.artifactSpellsControlViewModel.AddSpellFunc = AddSpell;
            Length = Staff.LengthStrings[staff.Length];
            Material = Staff.MaterialStrings[staff.Material];
            Pasp = staff.Pasp.ToString();
        }

        public ArtifactSpell? AddSpell(Window window)
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
