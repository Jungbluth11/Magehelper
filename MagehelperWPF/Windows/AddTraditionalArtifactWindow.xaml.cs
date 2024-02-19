using System.Collections.Generic;
using System.Windows;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für AddTraditionalArtifactWindow.xaml
    /// </summary>
    public partial class AddTraditionalArtifactWindow : Window
    {
        private readonly MainWindow mainWindow;
        public string SelectedArtifact { get; private set; }

        public AddTraditionalArtifactWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            List<string> artifactsNames = new List<string>(mainWindow.Core.ArtifactNames);
            TraditionalArtifact[] artifacts = new TraditionalArtifact[]
            {
                mainWindow.TabContentTraditionalArtifact.Staff,
                mainWindow.TabContentTraditionalArtifact.CrystalBall,
                mainWindow.TabContentTraditionalArtifact.Bowl,
                mainWindow.TabContentTraditionalArtifact.BoneCub,
                mainWindow.TabContentTraditionalArtifact.RingOfLife,
                mainWindow.TabContentTraditionalArtifact.ObsidianDagger
            };
            foreach (TraditionalArtifact artifact in artifacts)
            {
                if (artifact != null)
                {
                    artifactsNames.Remove(artifact.Name);
                }
            }
            DropdownArtifact.ItemsSource = artifactsNames;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (DropdownArtifact.SelectedIndex >= 0)
            {
                SelectedArtifact = DropdownArtifact.SelectedValue.ToString();
                DialogResult = true;
                Close();
            }
        }
    }
}