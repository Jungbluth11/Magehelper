using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    class ArtifactSpellsControlViewModel
    {
        private readonly Artifact artifact;
        private readonly Func<ArtifactSpell?> addSpellFunction;
        private readonly ObservableCollection<ArtifactSpell> spells = new ObservableCollection<ArtifactSpell>();

        public ArtifactSpellsControlViewModel(string artifactSpellName, Artifact artifact, Func<ArtifactSpell?> addSpellFunction, string artifactSpellCounterText = null)
        {
            this.artifact = artifact;
            this.addSpellFunction = addSpellFunction;
            //foreach (ArtifactSpell spell in artifact.BoundSpells)
            //{
            //    ArtifactSpell artifactSpell = spell;
            //    if (Properties.Settings.Default.AllowRemoveSpells)
            //    {
            //        artifactSpell.IsNew = true;
            //    }
            //    spells.Add(artifactSpell);
            //}
            //SetSpellCounter();
            //ArtifactSpellPanel.ItemsSource = spells;
            //spells.CollectionChanged += Spells_CollectionChanged;
        }
    }
}
