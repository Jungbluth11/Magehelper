using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Magehelper.Avalonia.ViewModels.Tabs
{
    public partial class TabContentModViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _infoText;
        [ObservableProperty]
        private string _representation;
        [ObservableProperty]
        private string _modValueTotal = string.Empty;
        [ObservableProperty]
        private bool _useMr = false;
        private readonly Mod mod = new();
        public ObservableCollection<Modification> ModList { get; set; } = [];
        public ObservableCollection<Modification> ModDataList { get; set; } = [];
        public IEnumerable<string> RepresentationList { get; set; }

        public TabContentModViewModel()
        {
            RepresentationList = mod.Representations;
            if (mod.Data != null)
            {
                ModDataList.AddRange(mod.Data);
            }
            Representation = mod.CurrentRepresentation ?? RepresentationList.First();
            InfoText = mod.InfoText ?? string.Empty;
            ModList.CollectionChanged += ModList_CollectionChanged;
        }

        private void ModList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Reset && ModList.Count > 0)
            {
                ModValueTotal = mod.Calculate().ToString();
            }
            else
            {
                ModValueTotal = string.Empty;
            }
        }

        public void AddMr(int mr)
        {
            mod.MR = mr;
            mod.UseMr = true;
            ModList.Add(new Modification("Magieresistenz: " + mr.ToString(), "MR", mr));
        }

        partial void OnRepresentationChanged(string value)
        {
            mod.SetRepresentation(value);
            InfoText = mod.InfoText ?? string.Empty;
            ModDataList.Clear();
#pragma warning disable CS8604
            ModDataList.AddRange(mod.Data);
#pragma warning restore CS8604
        }

        [RelayCommand]
        private void AddMod(Modification modification)
        {
            mod.Add(modification.Amount);
            ModList.Add(modification);
        }

        [RelayCommand]
        private void RemoveMod(Modification modification)
        {
            if (modification.Description != "MR")
            {
                mod.Remove(modification.Amount);
            }
            else
            {
                mod.MR = 0;
                mod.UseMr = false;
                UseMr = false;
            }
            ModList.Remove(modification);
        }

        [RelayCommand]
        private void Reset()
        {
            ModList.Clear();
            mod.Reset();
        }
    }
}
