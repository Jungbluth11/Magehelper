using Avalonia.Controls;
using DSAUtils.HeldentoolInterop;

namespace Magehelper.Avalonia.ViewModels.Windows
{
    public partial class LoadFromToolWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _selectedCharacterIndex = 0;
        public List<CharakterListItem> Characters { get; set; } = [];
        public bool ShowNoCharactersText => Characters.Count == 0;

        public LoadFromToolWindowViewModel()
        {
            foreach (Charakter c in TabContentCharacterViewModel.Instance.Character.GetCharactersFromTool())
            {
                Characters.Add(new CharakterListItem { Name = c.ToString(), Charakter = c });
            }
        }

        private bool CanLoad()
        {
            return !ShowNoCharactersText;
        }

        [RelayCommand(CanExecute = nameof(CanLoad))]
        private void LoadCharacter(Window window)
        {
            TabContentCharacterViewModel.Instance.LoadCharacter(Characters[SelectedCharacterIndex].Charakter);
            window.Close();
        }
    }
}
