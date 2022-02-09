using System;
using System.Windows;
using System.Windows.Controls;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für BowlControl.xaml
    /// </summary>
    public partial class BowlControl : UserControl, IArtifactData
    {
        private readonly Bowl bowl;
        public ArtifactSpellsControl ArtifactSpellsControl { get; }

        public BowlControl(Bowl bowl)
        {
            InitializeComponent();
            this.bowl = bowl;
            ArtifactSpellsControl = new ArtifactSpellsControl("Schalenzauber", bowl, AddSpell);
            DropdownTemperatureCategoryStart.ItemsSource = Bowl.TemperatureCategoryStrings;
            DropdownTemperatureCategoryTarget.ItemsSource = Bowl.TemperatureCategoryStrings;
            DropdownTemperatureCategoryStart.SelectedIndex = 5;
            DropdownTemperatureCategoryTarget.SelectedIndex = 5;
            if (bowl.HasFireAndIce)
            {
                GroupBoxFireAndIce.Visibility = Visibility.Visible;
            }
        }

        public ArtifactSpell? AddSpell()
        {
            AddArtifactSpellWindow addArtifactSpellWindow = new AddArtifactSpellWindow("Schalenzauber", bowl);
            if (addArtifactSpellWindow.ShowDialog() == true)
            {
                try
                {
                    if (addArtifactSpellWindow.SpellName == "Feuer und Eis")
                    {
                        GroupBoxFireAndIce.Visibility = Visibility.Visible;
                    }
                    return bowl.AddSpell(addArtifactSpellWindow.SpellName);
                }
                catch (Exception e)
                {
                    ErrorMessages.Error(e.Message);
                }
            }
            return null;
        }

        private void FireAndIce()
        {
            if (GroupBoxFireAndIce.Visibility == Visibility.Visible)
            {
                StringCost.Content = bowl.FireAndIce(DropdownTemperatureCategoryStart.SelectedValue.ToString(), DropdownTemperatureCategoryTarget.SelectedValue.ToString(), NumericUpDownDuration.Value);
            }
        }

        private void CbApport_CheckedChanged(object sender, RoutedEventArgs e)
        {
            bowl.HasApport = (bool)CbApport.IsChecked;
        }

        private void DropdownTemperatureCategoryStart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FireAndIce();
        }

        private void DropdownTemperatureCategoryTarget_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FireAndIce();
        }

        private void NumericUpDownDuration_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            FireAndIce();
        }
    }
}