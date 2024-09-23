using Avalonia.Controls;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class RingOfLifeControlViewModel : ObservableObject
    {
        private readonly RingOfLife ringOfLife;
        private readonly ArtifactSpellsControlViewModel artifactSpellsControlViewModel;

        public RingOfLifeControlViewModel(RingOfLife ringOfLife, ArtifactSpellsControlViewModel artifactSpellsControlViewModel)
        {
            this.ringOfLife = ringOfLife;
            this.artifactSpellsControlViewModel = artifactSpellsControlViewModel;
            this.artifactSpellsControlViewModel.AddSpellFunc = AddSpell;
        }

        public ArtifactSpell? AddSpell(Window window)
        {
            AddArtifactSpellWindow addArtifactSpellWindow = new("Schlangenringzauber", ringOfLife);
            string result = addArtifactSpellWindow.ShowDialog<string>(window).Result;
            if (result != null)
            {
                try
                {
                    return ringOfLife.AddSpell(result);
                }
                catch (Exception ex)
                {
                    ErrorMessages.Error(ex.Message);
                }
            }
            return null;
        }
    }
}
