﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.Input;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class ArtifactSpellsControlViewModel : ObservableObject
    {
        private readonly Artifact artifact;
        private readonly Func<ArtifactSpell?> addSpellFunction;
        public string ArtifactSpellName { get; set; }
        public string ArtifactSpellCounterText { get; set; }
        public int ArtifactSpellCounterValue { get; set; }
        public ObservableCollection<ArtifactSpell> Spells => [];

        public ArtifactSpellsControlViewModel(Settings settings, string artifactSpellName, Artifact artifact, Func<ArtifactSpell?> addSpellFunction, string artifactSpellCounterText = "")
        {
            ArgumentNullException.ThrowIfNull(settings);
            ArgumentNullException.ThrowIfNull(artifact);
            ArgumentNullException.ThrowIfNull(addSpellFunction);

            if (string.IsNullOrEmpty(artifactSpellName))
            {
                throw new ArgumentException($"\"{nameof(artifactSpellName)}\" kann nicht NULL oder leer sein.", nameof(artifactSpellName));
            }

            this.artifact = artifact;
            this.addSpellFunction = addSpellFunction;
            ArtifactSpellName = artifactSpellName;
            ArtifactSpellCounterText = artifactSpellName;
            foreach (ArtifactSpell spell in artifact.BoundSpells)
            {
                ArtifactSpell artifactSpell = spell;
                if (settings.AllowRemoveSpells)
                {
                    artifactSpell.IsNew = true;
                }
                Spells.Add(artifactSpell);
            }
            SetSpellCounter();
            Spells.CollectionChanged += Spells_CollectionChanged;
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
                ArtifactSpellCounterValue = value;
            }
        }

        private void Spells_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetSpellCounter();
        }

        [RelayCommand]
        private void AddArtifactSpell()
        {
            ArtifactSpell? artifactSpell = addSpellFunction();
            if (artifactSpell != null)
            {
                Spells.Add((ArtifactSpell)artifactSpell);
            }
        }

        [RelayCommand]
        private void RemoveArtifactSpell(ArtifactSpell artifactSpell)
        {
            if (artifact is Staff staff)
            {
                staff.RemoveSpell(artifactSpell.Guid);
            }
            else
            {
                artifact.RemoveSpell(artifactSpell.Guid);
            }
            Spells.Remove(artifactSpell);
        }
    }
}
