using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class ArtifactSpellsControlViewModel : ObservableObject
    {
        private readonly Artifact artifact;
        public Func<Window, Task<ArtifactSpell?>>? AddSpellFunc { get; set; }
        public string ArtifactSpellName { get; set; }
        public string ArtifactSpellCounterText { get; set; }
        public string ArtifactSpellCounterValue { get; set; } = string.Empty;
        public ObservableCollection<ArtifactSpell> Spells { get; set; } = [];

        public ArtifactSpellsControlViewModel(Settings settings, string artifactSpellName, Artifact artifact, string artifactSpellCounterText = "")
        {
            ArgumentNullException.ThrowIfNull(settings);
            ArgumentNullException.ThrowIfNull(artifact);

            if (string.IsNullOrEmpty(artifactSpellName))
            {
                throw new ArgumentException($"\"{nameof(artifactSpellName)}\" kann nicht NULL oder leer sein.", nameof(artifactSpellName));
            }

            this.artifact = artifact;
            AddSpellFunc = AddSpellFunc;
            ArtifactSpellName = artifactSpellName;
            ArtifactSpellCounterText = artifactSpellCounterText;
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
                ArtifactSpellCounterValue = value.ToString();
            }
        }

        private void Spells_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetSpellCounter();
        }

        [RelayCommand]
        private async void AddArtifactSpell()
        {
            if (AddSpellFunc != null)
            {
                ArtifactSpell? artifactSpell = await AddSpellFunc((Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow);
                if (artifactSpell != null)
                {
                    Spells.Add((ArtifactSpell)artifactSpell);
                }
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
