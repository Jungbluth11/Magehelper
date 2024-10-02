using System.Collections.ObjectModel;

namespace Magehelper.Avalonia.ViewModels.Tabs
{
    public partial class TabContentSpellStorageViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isSpellStorageEnabled = false;
        [ObservableProperty]
        private bool _showNoSpellStorageText = true;
        private static TabContentSpellStorageViewModel _instance = new();
        public static TabContentSpellStorageViewModel Instance => _instance;
        public ObservableCollection<SpellStorageControl> SpellStorageList { get; set; } = [];
        public SpellStorage SpellStorage { get; }

        public TabContentSpellStorageViewModel()
        {
            SpellStorage = new SpellStorage(MainWindowViewModel.Instance.Core);
            MainWindowViewModel.Instance.Core.EnableSpellStorageGUIAction = EnableTab;
        }

        public void ResetTab()
        {
            IsSpellStorageEnabled = false;
            ShowNoSpellStorageText = true;
            SpellStorageList.Clear();
        }

        public void EnableTab()
        {
            for (int i = 0; i < SpellStorage.StorageCount; i++)
            {
                SpellStorageList.Add(new SpellStorageControl(i, SpellStorage));
            }
            IsSpellStorageEnabled = true;
            ShowNoSpellStorageText = false;
        }
    }
}
