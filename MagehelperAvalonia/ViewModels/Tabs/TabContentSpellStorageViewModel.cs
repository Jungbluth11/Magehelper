namespace Magehelper.Avalonia.ViewModels.Tabs
{
    public partial class TabContentSpellStorageViewModel : ObservableObject
    {
        private static TabContentSpellStorageViewModel _instance = new();
        public static TabContentSpellStorageViewModel Instance => _instance;

        public SpellStorage SpellStorage { get; }

        public TabContentSpellStorageViewModel()
        {
            SpellStorage = new SpellStorage(MainWindowViewModel.Instance.Core);
        }
    }
}
