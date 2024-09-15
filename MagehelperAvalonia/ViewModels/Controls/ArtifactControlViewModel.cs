using Avalonia.Controls;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class ArtifactControlViewModel(string artifactName, UserControl userControl) : ObservableObject
    {
        public string ArtifactName { get; set; } = artifactName;
        public UserControl UserControl { get; set; } = userControl;
    }
}
