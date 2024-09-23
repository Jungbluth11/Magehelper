using Avalonia.Controls;

namespace Magehelper.Avalonia.ViewModels.Windows
{
    public partial class AddArtifactSpellWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _selectedSpellName;
        public string WindowTitle { get; }
        IEnumerable<string> Spells { get; }

        public AddArtifactSpellWindowViewModel(string artifactSpellName, Artifact artifact)
        {
            WindowTitle = artifactSpellName + " hinzufügen";
            Spells = from ArtifactSpell in artifact.SpellsAvailable select ArtifactSpell.Name;
            SelectedSpellName = Spells.First();
        }

        [RelayCommand]
        private void AddSpell(Window window)
        {
            window.Close(SelectedSpellName);
        }

        [RelayCommand]
        private void Cancel(Window window)
        {
            window.Close();
        }
    }
}
