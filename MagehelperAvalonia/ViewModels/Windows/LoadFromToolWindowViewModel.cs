using DSAUtils.HeldentoolInterop;

namespace Magehelper.Avalonia.ViewModels.Windows
{
    public partial class LoadFromToolWindowViewModel : ObservableObject
    {
        public List<CharakterListItem> Characters { get; set; } = [];
        public bool HasCharacters => Characters.Count > 0;

        public LoadFromToolWindowViewModel()
        {
            foreach (Charakter c in HeldentoolInterop.GetByAE())
            {
                Characters.Add(new CharakterListItem { Name = c.ToString(), Charakter = c});
            }
        }

        [RelayCommand]
        private void LoadCharacter(int index)
        {
            TabContentCharacterViewModel.Instance.LoadCharacterFromTool(Characters[index].Charakter);
        }
    }
}
