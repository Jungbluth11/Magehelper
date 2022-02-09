using System.Windows;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für AddStaffWindow.xaml
    /// </summary>
    public partial class AddStaffWindow : Window
    {
        private readonly TabContentArtifact tabContentArtifact;

        public AddStaffWindow(TabContentArtifact tabContentArtifact)
        {
            InitializeComponent();
            this.tabContentArtifact = tabContentArtifact;
            DropdownStaffLength.ItemsSource = Staff.LengthStrings;
            DropdownStaffMaterial.ItemsSource = Staff.MaterialStrings;
            DropdownStaffLength.SelectedIndex = 0;
            DropdownStaffMaterial.SelectedIndex = 0;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            int length = DropdownStaffLength.SelectedIndex;
            int material = DropdownStaffMaterial.SelectedIndex;
            int pasp = NumericUpDownPasp.Value;
            tabContentArtifact.AddStaff(length, material, pasp);
            Close();
        }
    }
}