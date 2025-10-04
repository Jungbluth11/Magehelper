

namespace Magehelper.ViewModels.Controls
{
    public partial class ObsidianDaggerControlViewModel : ObservableObject
    {
        private readonly ObsidianDagger obsidianDagger;
        readonly TraditionArtifactControlViewModel artifactSpellsControlViewModel;

        public ObsidianDaggerControlViewModel(ObsidianDagger obsidianDagger, TraditionArtifactControlViewModel artifactSpellsControlViewModel)
        {
            this.obsidianDagger = obsidianDagger;
            this.artifactSpellsControlViewModel = artifactSpellsControlViewModel;
            this.artifactSpellsControlViewModel.AddSpellFunc = AddSpell;
        }


        public async  Task<ArtifactSpell?> AddSpell(Window window)
        {
            AddArtifactSpellWindow addArtifactSpellWindow = new("Dolchzauber", obsidianDagger);
            string result = await addArtifactSpellWindow.ShowDialog<string>(window);
            if (result != null)
            {
                try
                {
                    return obsidianDagger.AddSpell(result);
                }
                catch (Exception ex)
                {
                    ErrorMessages.Error(ex.Message, TODO);
                }
            }
            return null;
        }

        [RelayCommand]
        private void Apport(bool? isChecked)
        {
            obsidianDagger.HasApport = (bool)isChecked;
        }
    }
}
