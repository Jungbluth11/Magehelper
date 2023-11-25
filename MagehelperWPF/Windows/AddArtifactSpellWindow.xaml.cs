using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für AddArtifactSpellWindow.xaml
    /// </summary>
    public partial class AddArtifactSpellWindow : Window
    {
        public string SpellName { get; private set; }

        public AddArtifactSpellWindow(string artifactSpellName, Artifact artifact)
        {
            InitializeComponent();
            Title = artifactSpellName + " hinzufügen";
            IEnumerable<string> spells = from ArtifactSpell in artifact.SpellsAvailable select ArtifactSpell.Name;
            DropdownArtifactSpells.ItemsSource = spells;
            DropdownArtifactSpells.SelectedIndex = 0;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            SpellName = DropdownArtifactSpells.Text;
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}