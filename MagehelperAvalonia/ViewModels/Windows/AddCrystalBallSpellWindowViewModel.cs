using Avalonia.Controls;

namespace Magehelper.Avalonia.ViewModels.Windows
{
    public partial class AddCrystalBallSpellWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _selectedSpellName;
        [ObservableProperty]
        private string _selectedSpellVariant;
        [ObservableProperty]
        private string _variantDescription = string.Empty;
        [ObservableProperty]
        private bool _spellVariantVisible = false;
        [ObservableProperty]
        private bool _variantDescriptionVisible = false;
        public IEnumerable<string> Spells { get; }
        public IEnumerable<string> SpellVariants { get; } = ["Variante 1", "Variante 2", "Beschreibung eingeben"];

        public AddCrystalBallSpellWindowViewModel(CrystalBall crystalBall)
        {
            Spells = from ArtifactSpell in crystalBall.SpellsAvailable select ArtifactSpell.Name;
            SelectedSpellName = Spells.First();
            SelectedSpellVariant = SpellVariants.First();
        }

        partial void OnSelectedSpellNameChanged(string value)
        {
            if (value == "Bildergalerie")
            {
                SpellVariantVisible = true;
            }
            else
            {
                SpellVariantVisible = false;
                SelectedSpellVariant = SpellVariants.First();
            }
        }

        partial void OnSelectedSpellVariantChanged(string value)
        {
            if (value == "Beschreibung eingeben")
            {
                VariantDescriptionVisible = true;
            }
            else
            {
                VariantDescriptionVisible = false;
                VariantDescription = string.Empty;
            }
        }

        [RelayCommand]
        private void AddSpell(Window window)
        {
            window.Close((SelectedSpellName, VariantDescription != string.Empty ? VariantDescription : SelectedSpellVariant));
        }

        [RelayCommand]
        private void Cancel(Window window)
        {
            window.Close();
        }
    }
}
