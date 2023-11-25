using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für TabContentMod.xaml
    /// </summary>
    public partial class TabContentMod : UserControl
    {
        private readonly Mod mod = new Mod();
        private readonly ObservableCollection<Modification> modList = new ObservableCollection<Modification>();
        public TabContentMod()
        {
            InitializeComponent();
            DropdownRepresentation.ItemsSource = mod.Representations;
            DropdownRepresentation.SelectedIndex = 7;
            ModListPanel.ItemsSource = modList;
            modList.CollectionChanged += ModList_CollectionChanged;
        }

        private void AddMod(Modification modification)
        {
            mod.Add(modification.Amount);
            modList.Add(modification);
        }

        private void ModList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Reset && modList.Count > 0)
            {
                StringModValueTotal.Content = mod.Calculate();
            }
            else
            {
                StringModValueTotal.Content = string.Empty;
            }
        }

        private void DropdownRepresentation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mod.SetRepresentation(e.AddedItems[0].ToString());
            StringInfoText.Content = mod.InfoText;
            for (int i = 0; i < 14; i++)
            {
                (GridModifications.FindName("StringModName" + i) as Label).Content = mod.Data[i].Name;
                (GridModifications.FindName("StringModDescription" + i) as Label).Content = mod.Data[i].Description;
                (GridModifications.FindName("BtnAddMod" + i) as Button).Tag = mod.Data[i];
            }
        }

        private void BtnAddMod0_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod1_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod2_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod3_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod4_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod5_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod6_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod7_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod8_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod9_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod10_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod11_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod12_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnAddMod13_Click(object sender, RoutedEventArgs e)
        {
            AddMod((Modification)(sender as Button).Tag);
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            modList.Clear();
            mod.Reset();
        }

        private void BtnRemoveMod_Click(object sender, RoutedEventArgs e)
        {
            Modification modification = (Modification)(sender as Button).Tag;
            if (modification.Description != "MR")
            {
                mod.Remove(modification.Amount);
            }
            else
            {
                mod.MR = 0;
                mod.UseMr = false;
                CbMR.IsEnabled = true;
            }
            modList.Remove(modification);
        }

        private void CbMR_Checked(object sender, RoutedEventArgs e)
        {
            DialogNumberWindow dialogNumberWindow = new DialogNumberWindow("Magieresistenz", "höchste beteiligte Magieresistenz");
            dialogNumberWindow.ShowDialog();
            int mr = dialogNumberWindow.Value;
            mod.MR = mr;
            mod.UseMr = true;
            modList.Add(new Modification("Magieresistenz: " + mr.ToString(), "MR", mr));
            CbMR.IsEnabled = false;
        }
    }
}