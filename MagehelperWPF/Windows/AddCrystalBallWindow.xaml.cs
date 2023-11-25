using System.Windows;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für AddCrystalBallWindow.xaml
    /// </summary>
    public partial class AddCrystalBallWindow : Window
    {
        private readonly TabContentArtifact tabContentArtifact;

        public AddCrystalBallWindow(TabContentArtifact tabContentArtifact)
        {
            InitializeComponent();
            this.tabContentArtifact = tabContentArtifact;
            DropdownCrystalBallKind.ItemsSource = CrystalBall.MaterialStrings;
            DropdownCrystalBallKind.SelectedIndex = 0;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            tabContentArtifact.AddCrystallBall((CrystalBallMaterial)DropdownCrystalBallKind.SelectedIndex);
            Close();
        }
    }
}