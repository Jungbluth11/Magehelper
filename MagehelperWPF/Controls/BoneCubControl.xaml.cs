using System;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für BoneCubControl.xaml
    /// </summary>
    public partial class BoneCubControl : UserControl, IArtifactData
    {
        private readonly BoneCub boneCub;
        public ArtifactSpellsControl ArtifactSpellsControl { get; }
        public BoneCubControl(BoneCub boneCub)
        {
            InitializeComponent();
            this.boneCub = boneCub;
            ArtifactSpellsControl = new ArtifactSpellsControl("Keulenzauber", boneCub, AddSpell, "Verbleibende Zauber:");
        }

        public ArtifactSpell? AddSpell()
        {
            AddArtifactSpellWindow addArtifactSpellWindow = new AddArtifactSpellWindow("Keulenzauber", boneCub);
            if (addArtifactSpellWindow.ShowDialog() == true)
            {
                try
                {
                    return boneCub.AddSpell(addArtifactSpellWindow.SpellName);
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
            boneCub.HasApport = (bool)CbApport.IsChecked;
        }
    }
}