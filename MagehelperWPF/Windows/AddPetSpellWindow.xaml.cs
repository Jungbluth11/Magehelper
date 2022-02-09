using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für AddPetSpellWindow.xaml
    /// </summary>
    public partial class AddPetSpellWindow : Window
    {
        private readonly Pet pet;
        private readonly ObservableCollection<PetSpell> spellsAvailable = new ObservableCollection<PetSpell>();
        private readonly ObservableCollection<PetSpell> spellsToLearn = new ObservableCollection<PetSpell>();

        public AddPetSpellWindow(Pet pet)
        {
            this.pet = pet;
            InitializeComponent();
            foreach (PetSpell spell in pet.SpellsAvailable)
            {
                if (!pet.KnownSpells.Contains(spell))
                {
                    spellsAvailable.Add(spell);
                }
            }
            spellsAvailable.Sort(p => p.Name);
            spellsAvailable.CollectionChanged += SpellsAvailable_CollectionChanged;
            DataGridSpellsAvailable.ItemsSource = spellsAvailable;
            DataGridSpellsToLearn.ItemsSource = spellsToLearn;
        }

        private void SpellsAvailable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                spellsAvailable.Sort(p => p.Name);
            }
        }

        private void BtnAddSpell_Click(object sender, RoutedEventArgs e)
        {
            PetSpell spell = (PetSpell)(sender as Button).Tag;
            spellsAvailable.Remove(spell);
            spellsToLearn.Add(spell);
        }

        private void BtnRemoveSpell_Click(object sender, RoutedEventArgs e)
        {
            PetSpell spell = (PetSpell)(sender as Button).Tag;
            spellsAvailable.Add(spell);
            spellsToLearn.Remove(spell);
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (spellsToLearn.Count > 0)
            {
                foreach (PetSpell spell in spellsToLearn)
                {
                    pet.LearnSpell(spell.Name);
                }
            }
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}