using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für ArtifactSpellsControl.xaml
    /// </summary>
    public partial class ArtifactSpellsControl : UserControl
    {
        private readonly Artifact artifact;
        private readonly Func<ArtifactSpell?> addSpellFunction;
        private readonly ObservableCollection<ArtifactSpell> spells = new ObservableCollection<ArtifactSpell>();

        public ArtifactSpellsControl(string artifactSpellName, Artifact artifact, Func<ArtifactSpell?> addSpellFunction, string artifactSpellCounterText = null)
        {
            InitializeComponent();
            this.artifact = artifact;
            this.addSpellFunction = addSpellFunction;
            StringArtifactSpellCounterText.Content = artifactSpellCounterText;
            StringArtifactSpellName.Content = artifactSpellName;
            foreach (ArtifactSpell spell in artifact.BoundSpells)
            {
                ArtifactSpell artifactSpell = spell;
                if (Properties.Settings.Default.AllowRemoveSpells)
                {
                    artifactSpell.IsNew = true;
                }
                spells.Add(artifactSpell);
            }
            SetSpellCounter();
            ArtifactSpellPanel.ItemsSource = spells;
            spells.CollectionChanged += Spells_CollectionChanged;
        }

        public void SetSpellCounter()
        {
            if (artifact is Staff || artifact is IMaxSpellArtifact)
            {
                int value;
                if (artifact is Staff staff)
                {
                    value = staff.AfvRemain;
                }
                else
                {
                    value = (artifact as IMaxSpellArtifact).SpellsRemain;
                }
                StringArtifactSpellCounterValue.Content = value;
            }
        }

        private void Spells_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetSpellCounter();
        }

        private void BtnAddArtifactSpell_Click(object sender, RoutedEventArgs e)
        {
            ArtifactSpell? artifactSpell = addSpellFunction();
            if (artifactSpell != null)
            {
                spells.Add((ArtifactSpell)artifactSpell);
            }
        }

        private void BtnRemoveArtifactSpell_Click(object sender, RoutedEventArgs e)
        {
            ArtifactSpell artifactSpell = (ArtifactSpell)(sender as Button).Tag;
            if (artifact is Staff staff)
            {
                staff.RemoveSpell(artifactSpell.Guid);
            }
            else
            {
                artifact.RemoveSpell(artifactSpell.Guid);
            }
            spells.Remove(artifactSpell);
        }
    }
}