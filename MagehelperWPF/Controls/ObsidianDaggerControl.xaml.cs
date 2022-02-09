using System;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für ObsidianDaggerControl.xaml
    /// </summary>
    public partial class ObsidianDaggerControl : UserControl, IArtifactData
    {
        private readonly ObsidianDagger obsidianDagger;
        public ArtifactSpellsControl ArtifactSpellsControl { get; }

        public ObsidianDaggerControl(ObsidianDagger obsidianDagger)
        {
            InitializeComponent();
            this.obsidianDagger = obsidianDagger;
            ArtifactSpellsControl = new ArtifactSpellsControl("Dolchzauber", obsidianDagger, AddSpell, "Verbleibende Zauber:");
        }

        public ArtifactSpell? AddSpell()
        {
            AddArtifactSpellWindow addArtifactSpellWindow = new AddArtifactSpellWindow("Dolchzauber", obsidianDagger);
            if (addArtifactSpellWindow.ShowDialog() == true)
            {
                try
                {
                    return obsidianDagger.AddSpell(addArtifactSpellWindow.SpellName);
                }
                catch (Exception e)
                {
                    ErrorMessages.Error(e.Message);
                }
            }
            return null;
        }

        private void CbApport_CheckedChanged(object sender, RoutedEventArgs e)
        {
            obsidianDagger.HasApport = (bool)CbApport.IsChecked;
        }
    }
}