namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class ArtifactControlViewModel(string artifactName) : ObservableObject
    {
        public string ArtifactName { get; set; } = artifactName;
    }
}
