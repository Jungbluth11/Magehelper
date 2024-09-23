#pragma warning disable CS8601
using Avalonia.Controls;

namespace Magehelper.Avalonia.ViewModels.Windows
{
    public partial class AddArtifactWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _selectedArtifact;
        public List<string> ArtifactsNames { get; }

        public AddArtifactWindowViewModel()
        {
            ArtifactsNames = new List<string>(MainWindowViewModel.Instance.Core.ArtifactNames);
            Artifact[] artifacts =
            [
                MainWindowViewModel.Instance.Core.Staff,
                MainWindowViewModel.Instance.Core.CrystalBall,
                MainWindowViewModel.Instance.Core.Bowl,
                MainWindowViewModel.Instance.Core.BoneCub,
                MainWindowViewModel.Instance.Core.RingOfLife,
                MainWindowViewModel.Instance.Core.ObsidianDagger
            ];
            foreach (Artifact artifact in artifacts)
            {
                if (artifact != null)
                {
                    ArtifactsNames.Remove(artifact.Name);
                }
            }
            SelectedArtifact = ArtifactsNames[0];
        }

        [RelayCommand]
        private void AddArtifact(Window window)
        {
            window.Close(SelectedArtifact);
        }
    }
}
