using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DSAUtils.HeldentoolInterop;
using DSAUtils.Settings.Aventurien;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für AddStaffSpellWindow.xaml
    /// </summary>
    public partial class AddStaffSpellWindow : Window
    {
        public string SpellName { get; private set; }
        public string SpellCharacteristic { get; private set; }
        public int SpellPoints { get; private set; }

        public AddStaffSpellWindow(Staff staff)
        {
            InitializeComponent();
            IEnumerable<string> spells = from ArtifactSpell in staff.SpellsAvailable select ArtifactSpell.Name;
            DropdownStaffSpells.ItemsSource = spells;
            DropdownStaffSpells.SelectedIndex = 0;
            ComboBoxCharacteristics.ItemsSource = Aventurien.Zauber.merkmalsliste;
        }

        private void DropdownStaffSpells_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (DropdownStaffSpells.SelectedValue)
            {
                case "Merkmalsfokus":
                    ComboBoxCharacteristics.Visibility = Visibility.Visible;
                    NumericUpDownPoints.Visibility = Visibility.Collapsed;
                    break;
                case "Doppeltes Maß":
                case "Zauberspeicher":
                    ComboBoxCharacteristics.Visibility = Visibility.Collapsed;
                    NumericUpDownPoints.Visibility = Visibility.Visible;
                    break;
                default:
                    ComboBoxCharacteristics.Visibility = Visibility.Collapsed;
                    NumericUpDownPoints.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            SpellName = DropdownStaffSpells.Text;
            switch (HeldentoolInterop.Rename(SpellName, DSAUtils.HeldentoolInterop.Name.Offi))
            {
                case "Merkmalsfokus":
                    SpellCharacteristic = ComboBoxCharacteristics.Text;
                    break;
                case "Doppeltes Maß":
                case "Zauberspeicher":
                    SpellPoints = NumericUpDownPoints.Value;
                    break;
                default:
                    break;
            }
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}