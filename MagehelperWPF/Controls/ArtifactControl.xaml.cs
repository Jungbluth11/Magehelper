using System.Windows;
using System.Windows.Controls;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für ArrtifactControl.xaml
    /// </summary>
    public partial class ArtifactControl : UserControl
    {
        public ArtifactControl(string artifactName, UserControl userControl)
        {
            InitializeComponent();
            StringArtifactName.Content = artifactName;
            userControl.VerticalAlignment = VerticalAlignment.Top;
            userControl.HorizontalAlignment = HorizontalAlignment.Left;
            ArtifactSpellsControl artifactSpellsControl = (userControl as IArtifactData).ArtifactSpellsControl;
            artifactSpellsControl.VerticalAlignment = VerticalAlignment.Top;
            artifactSpellsControl.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(userControl, 0);
            Grid.SetRow(userControl, 1);
            Grid.SetColumn(artifactSpellsControl, 1);
            Grid.SetRow(artifactSpellsControl, 1);
            GridContent.Children.Add(userControl);
            GridContent.Children.Add(artifactSpellsControl);
        }
    }
}