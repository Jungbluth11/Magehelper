using Avalonia.Controls;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class BoneCubControlViewModel : ObservableObject
    {
        private readonly BoneCub boneCub;
        private readonly ArtifactSpellsControlViewModel artifactSpellsControlViewModel;

        public BoneCubControlViewModel(BoneCub boneCub, ArtifactSpellsControlViewModel artifactSpellsControlViewModel)
        {
            this.boneCub = boneCub;
            this.artifactSpellsControlViewModel = artifactSpellsControlViewModel;
            this.artifactSpellsControlViewModel.AddSpellFunc = AddSpell;
        }

        public ArtifactSpell? AddSpell(Window window)
        {
            AddArtifactSpellWindow addArtifactSpellWindow = new("Keulenzauber", boneCub);
            string result = addArtifactSpellWindow.ShowDialog<string>(window).Result;
            if (result != null)
            {
                try
                {
                    return boneCub.AddSpell(result);
                }
                catch (Exception ex)
                {
                    ErrorMessages.Error(ex.Message);
                }
            }
            return null;
        }

        [RelayCommand]
        private void Apport(bool? isChecked)
        {
            boneCub.HasApport = (bool)isChecked;
        }
    }
}
