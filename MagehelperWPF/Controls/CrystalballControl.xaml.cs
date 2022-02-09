using System;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für CrystalballControl.xaml
    /// </summary>
    public partial class CrystalBallControl : UserControl, IArtifactData
    {
        private readonly CrystalBall crystalBall;
        public ArtifactSpellsControl ArtifactSpellsControl { get; }

        public CrystalBallControl(CrystalBall crystalBall)
        {
            InitializeComponent();
            this.crystalBall = crystalBall;
            ArtifactSpellsControl = new ArtifactSpellsControl("Kugelzauber", crystalBall, AddSpell, "Verbleibende Kugelzauber");
            StringMaterial.Content = CrystalBall.MaterialStrings[(int)crystalBall.Material];
        }

        public ArtifactSpell? AddSpell()
        {
            AddCrystalBallSpellWindow addCrystalBallSpellWindow = new AddCrystalBallSpellWindow(crystalBall);
            if (addCrystalBallSpellWindow.ShowDialog() == true)
            {
                try
                {
                    return crystalBall.AddSpell(addCrystalBallSpellWindow.SpellName);
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
            crystalBall.HasApport = (bool)CbApport.IsChecked;
            ArtifactSpellsControl.SetSpellCounter();
        }
    }
}