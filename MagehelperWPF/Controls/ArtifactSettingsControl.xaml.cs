using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für ArtifactSettingsControl.xaml
    /// </summary>
    public partial class ArtifactSettingsControl : UserControl, IArtifactSettingsTab
    {
        public ArtifactSpell[] ArtifactSpells { get; set; }
        public string SettingsHeader { get; }

        public ArtifactSettingsControl(ArtifactSpell[] artifactSpells)
        {
            InitializeComponent();
            ArtifactSpells = artifactSpells;
            Content.ItemsSource = artifactSpells;
            SettingsHeader = artifactSpells[0].Type;
        }

        private void NumericUpDownCost_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            for (int i = 0; i < ArtifactSpells.Length; i++)
            {
                if (ArtifactSpells[i].Equals((sender as FrameworkElement).DataContext))
                {
                    ArtifactSpells[i].Cost = e.NewValue;
                    break;
                }
            }
        }
    }
}