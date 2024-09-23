using System.Collections.ObjectModel;
using Avalonia.Controls;
using Magehelper.Avalonia.Models;

namespace Magehelper.Avalonia.ViewModels.Windows
{
    public partial class EnableSpellStorageWindowViewModel : ObservableObject
    {
        private int pointsUsed;
        private readonly int pointsTotal;
        private readonly SpellStorage spellStorage;
        public int PointsRemain => pointsTotal - pointsUsed;
        public ObservableCollection<SpellStorageRepresenter> SpellStorages => [];


        public EnableSpellStorageWindowViewModel(int points)
        {
            spellStorage = TabContentSpellStorageViewModel.Instance.SpellStorage;
            pointsTotal = spellStorage.GetMaxPoints(points);
        }

        private bool CanAddSpellStorage()
        {
            if (pointsUsed > 0)
            {
                return true;
            }
            return false;
        }

        private bool CanSubmit()
        {
            if (PointsRemain == 0)
            {
                return true;
            }
            return false;
        }

        private string GetText(int index, int points)
        {
            return "Speicher " + (index + 1).ToString() + ": " + points.ToString() + " AsP";
        }

        [RelayCommand(CanExecute = nameof(CanAddSpellStorage))]
        private void AddSpellStorage(int points)
        {
            string text = GetText(SpellStorages.Count, points);
            pointsUsed += points;
            SpellStorages.Add(new SpellStorageRepresenter { Text = text, Points = points });
        }

        [RelayCommand]
        private void RemoveSpellStorage(SpellStorageRepresenter spellStorage)
        {
            pointsUsed -= spellStorage.Points;
            SpellStorages.Remove(spellStorage);
            for (int i = 0; i < SpellStorages.Count; i++)
            {
                int points = SpellStorages[i].Points;
                SpellStorages[i] = new SpellStorageRepresenter { Text = GetText(i, points), Points = points };
            }
        }

        [RelayCommand(CanExecute = nameof(CanSubmit))]
        private void Submit(Window window)
        {
            List<int> points = new();
            foreach (SpellStorageRepresenter spellStorage in SpellStorages)
            {
                points.Add(spellStorage.Points);
            }
            spellStorage.EnableStorage(points);
            window.Close();
        }
    }
}
