using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für StaffSettings.xaml
    /// </summary>
    public partial class StaffSettingsControl : UserControl, IArtifactSettingsTab
    {
        public string SettingsHeader { get; }
        public ArtifactSpell[] ArtifactSpells { get; set; }

        public StaffSettingsControl(ArtifactSpell[] artifactSpells)
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

        private void NumericUpDownPoints_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            for (int i = 0; i < ArtifactSpells.Length; i++)
            {
                if (ArtifactSpells[i].Equals((sender as FrameworkElement).DataContext))
                {
                    ArtifactSpells[i].Points = e.NewValue;
                    break;
                }
            }
        }
    }
}