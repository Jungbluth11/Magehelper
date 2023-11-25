using System.Collections.Generic;
using System.Windows;
using Magehelper.Core;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für AddArtifactWindow.xaml
    /// </summary>
    public partial class AddArtifactWindow : Window
    {
        private readonly MainWindow mainWindow;
        public string SelectedArtifact { get; private set; }

        public AddArtifactWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            List<string> artifactsNames = new List<string>(mainWindow.Core.ArtifactNames);
            Artifact[] artifacts = new Artifact[]
            {
                mainWindow.TabContentArtifact.Staff,
                mainWindow.TabContentArtifact.CrystalBall,
                mainWindow.TabContentArtifact.Bowl,
                mainWindow.TabContentArtifact.BoneCub,
                mainWindow.TabContentArtifact.RingOfLife,
                mainWindow.TabContentArtifact.ObsidianDagger
            };
            foreach (Artifact artifact in artifacts)
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