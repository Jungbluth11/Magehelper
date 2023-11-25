using System;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für RingOfLiveControl.xaml
    /// </summary>
    public partial class RingOfLifeControl : UserControl, IArtifactData
    {
        private readonly RingOfLife ringOfLife;
        public ArtifactSpellsControl ArtifactSpellsControl { get; }

        public RingOfLifeControl(RingOfLife ringOfLife)
        {
            InitializeComponent();
            this.ringOfLife = ringOfLife;
            ArtifactSpellsControl = new ArtifactSpellsControl("Schlangenringzauber", ringOfLife, AddSpell, "Verbleibende Zauber:");
        }

        public ArtifactSpell? AddSpell()
        {
            AddArtifactSpellWindow addArtifactSpellWindow = new AddArtifactSpellWindow("Schlangenringzauber", ringOfLife);
            if (addArtifactSpellWindow.ShowDialog() == true)
            {
                try
                {
                    return ringOfLife.AddSpell(addArtifactSpellWindow.SpellName);
                }
                catch (Exception e)
                {
                    ErrorMessages.Error(e.Message);
                }
            }
            return null;
        }
    }
}