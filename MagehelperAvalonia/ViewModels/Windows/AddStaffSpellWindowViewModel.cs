using Avalonia.Controls;
using DSAUtils.Settings.Aventurien;

namespace Magehelper.Avalonia.ViewModels.Windows
{
    public partial class AddStaffSpellWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _selectedSpellName;
        [ObservableProperty]
        private string _characteristic;
        [ObservableProperty]
        private int _spellPoints = 1;
        [ObservableProperty]
        private int _minSpellPoints = 1;
        [ObservableProperty]
        private int _maxSpellPoints = 1;
        [ObservableProperty]
        private bool _isCharacteristicVisible = false;
        [ObservableProperty]
        private bool _isSpellPointsVisible = false;
        private readonly Staff staff;
        public IEnumerable<string> Spells { get; }
        public IEnumerable<string> CharacteristicStrings { get; }

        public AddStaffSpellWindowViewModel(Staff staff)
        {
            this.staff = staff;
            Spells = from ArtifactSpell in staff.SpellsAvailable select ArtifactSpell.Name;
            CharacteristicStrings = Aventurien.Zauber.merkmalsliste;
            SelectedSpellName = Spells.First();
            Characteristic = CharacteristicStrings.First();
        }

        partial void OnSelectedSpellNameChanged(string value)
        {
            switch (value)
            {
                case "Merkmalsfokus":
                    IsCharacteristicVisible = false;
                    IsSpellPointsVisible = false;
                    break;
                case "Doppeltes Maß":
                case "Zauberspeicher":
                    IsCharacteristicVisible = false;
                    IsSpellPointsVisible = true;
                    MaxSpellPoints = staff.SpellsAvailable.Where(a => a.Name == value).ToArray()[0].Points;
                    MinSpellPoints = 1;
                    SpellPoints = 1;
                    if (value == "Doppeltes Maß")
                    {
                        SpellPoints = MaxSpellPoints;
                        MinSpellPoints = MaxSpellPoints;
                        MaxSpellPoints *= 2;
                    }
                    break;
                default:
                    IsCharacteristicVisible = false;
                    IsSpellPointsVisible = false;
                    break;
            }
        }

        [RelayCommand]
        private void Submit(Window window)
        {
            window.Close((SelectedSpellName, Characteristic, SpellPoints));
        }

        [RelayCommand]
        private void Cancel(Window window)
        {
            window.Close();
        }
    }
}
