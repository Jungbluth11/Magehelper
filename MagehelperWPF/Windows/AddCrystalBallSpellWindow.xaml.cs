using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DSAUtils.HeldentoolInterop;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für AddCrystallBallSpellWindow.xaml
    /// </summary>
    public partial class AddCrystalBallSpellWindow : Window
    {
        public string SpellName { get; private set; }
        public string SpellVariant { get; private set; }

        public AddCrystalBallSpellWindow(CrystalBall crystalBall)
        {
            InitializeComponent();
            IEnumerable<string> spells = from ArtifactSpell in crystalBall.SpellsAvailable select ArtifactSpell.Name;
            DropdownCrystalBallSpells.ItemsSource = spells;
            DropdownCrystalBallSpells.SelectedIndex = 0;
        }

        private void DropdownCrystalBallSpells_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DropdownCrystalBallSpells.SelectedValue.ToString() == "Bildergalerie")
            {
                DropdownVariant.Visibility = Visibility.Visible;
            }
            else
            {
                DropdownVariant.Visibility = Visibility.Collapsed;
            }
        }

        private void DropdownVariant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DropdownVariant.SelectedIndex == 2)
            {
                TBoxVariant.Visibility = Visibility.Visible;
            }
            else
            {
                TBoxVariant.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            SpellName = DropdownCrystalBallSpells.Text;

            if (HeldentoolInterop.Rename(SpellName, DSAUtils.HeldentoolInterop.Name.Offi) == "Bildergalerie")
            {
                if (DropdownVariant.SelectedIndex == 2)
                {
                    SpellVariant = TBoxVariant.Text;
                }
                else
                {
                    SpellVariant = DropdownVariant.SelectedValue.ToString();
                }
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