using DSAUtils.Settings.Aventurien;

namespace Magehelper.ViewModels.Dialogs
{
    public partial class AddSpellWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _spellstorage = 0;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string _name = string.Empty;
        [ObservableProperty]
        private string _characteristic;
        [ObservableProperty]
        private string _komplex;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string _cost = string.Empty;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string _zfp = string.Empty;
        public IEnumerable<string> CharacteristicStrings { get; }
        public IEnumerable<string> KomplexStrings { get; }
        public List<string> SpellStorages { get; set; } = [];

        public AddSpellWindowViewModel()
        {
            CharacteristicStrings = Aventurien.Zauber.merkmalsliste;
            KomplexStrings = DSA.komplexitaet;
            Characteristic = CharacteristicStrings.First();
            Komplex = KomplexStrings.First();
            for (int i = 0; i < TabSpellStorageViewModel.Instance.SpellStorage.StorageCount; i++)
            {
                SpellStorages.Add("Speicher " + (i + 1).ToString());
            }
        }

        private bool CanSubmit()
        {
            try
            {
                int.Parse(Cost);
                if (!string.IsNullOrWhiteSpace(Zfp))
                {
                    int.Parse(Zfp);
                }
            }
            catch
            {
                return false;
            }
            return !string.IsNullOrWhiteSpace(Name);
        }

        [RelayCommand(CanExecute = nameof(CanSubmit))]
        private void Submit(Window window)
        {
            int? zfp = string.IsNullOrWhiteSpace(Zfp) ? null : int.Parse(Zfp);
            StoragedSpell  storagedSpell = TabSpellStorageViewModel.Instance.SpellStorage.AddSpell(Name, Characteristic, Komplex, int.Parse(Cost), zfp, Spellstorage);
            window.Close(storagedSpell);
        }

        [RelayCommand]
        private void Cancel(Window window)
        {
            window.Close();
        }
    }
}
