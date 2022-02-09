using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für EnableSpellStorageWindow.xaml
    /// </summary>
    public partial class EnableSpellStorageWindow : Window
    {
        private int pointsUsed;
        private readonly int pointsTotal;
        private readonly ObservableCollection<SpellStorageRepresenter> spellStorages = new ObservableCollection<SpellStorageRepresenter>();
        private readonly MainWindow mainWindow;
        private readonly SpellStorage spellStorage;

        public EnableSpellStorageWindow(int points, MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            if (mainWindow.TabContentSpellStorage is null)
            {
                spellStorage = new SpellStorage(mainWindow.Core);
            }
            else
            {
                spellStorage = mainWindow.TabContentSpellStorage.SpellStorage;
            }
            pointsTotal = spellStorage.GetMaxPoints(points);
            NumericUpDownPoints.MaxValue = pointsTotal;
            StringPointsRemain.Content = pointsTotal;
            SpellStoragePanel.ItemsSource = spellStorages;
            spellStorages.CollectionChanged += SpellStorages_CollectionChanged;
        }

        private string GetText(int index, int points)
        {
            return "Speicher " + (index + 1).ToString() + ": " + points.ToString() + " AsP";
        }

        private void SpellStorages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            int pointsRemain = pointsTotal - pointsUsed;
            StringPointsRemain.Content = pointsRemain.ToString();
            NumericUpDownPoints.MaxValue = pointsRemain;
            if (pointsRemain == 0)
            {
                BtnAddStorage.IsEnabled = false;
            }
            else
            {
                BtnAddStorage.IsEnabled = true;
            }
        }

        private void BtnAddStorage_Click(object sender, RoutedEventArgs e)
        {
            int points = NumericUpDownPoints.Value;
            string text = GetText(spellStorages.Count, points);
            pointsUsed += points;
            spellStorages.Add(new SpellStorageRepresenter { Text = text, Points = points });
        }

        private void BtnRemoveStorage_Click(object sender, RoutedEventArgs e)
        {
            SpellStorageRepresenter spellStorageRepresenter = (SpellStorageRepresenter)(sender as Button).Tag;
            pointsUsed -= spellStorageRepresenter.Points;
            spellStorages.Remove(spellStorageRepresenter);
            for (int i = 0; i < spellStorages.Count; i++)
            {
                int points = spellStorages[i].Points;
                spellStorages[i] = new SpellStorageRepresenter { Text = GetText(i, points), Points = points };
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (pointsUsed == pointsTotal)
            {
                List<int> points = new List<int>();
                foreach (SpellStorageRepresenter spellStorage in spellStorages)
                {
                    points.Add(spellStorage.Points);
                }
                spellStorage.EnableStorage(points);
                Close();
            }
            else
            {
                MessageBox.Show("Es müssen alle Punkte verwendet werden", "Magehelper", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}