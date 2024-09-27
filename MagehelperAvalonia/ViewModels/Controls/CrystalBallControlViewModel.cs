using Avalonia.Controls;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class CrystalBallControlViewModel : ObservableObject
    {
        private readonly CrystalBall crystalBall;
        private readonly ArtifactSpellsControlViewModel artifactSpellsControlViewModel;
        public string Material { get; }

        public CrystalBallControlViewModel(CrystalBall crystalBall, ArtifactSpellsControlViewModel artifactSpellsControlViewModel)
        {
            this.crystalBall = crystalBall ?? throw new ArgumentNullException(nameof(crystalBall));
            this.artifactSpellsControlViewModel = artifactSpellsControlViewModel ?? throw new ArgumentNullException(nameof(artifactSpellsControlViewModel));
            this.artifactSpellsControlViewModel.AddSpellFunc = AddSpell;
            Material = CrystalBall.MaterialStrings[(int)crystalBall.Material];
        }

        public async Task<ArtifactSpell?> AddSpell(Window window)
        {
            AddCrystalBallSpellWindow addCrystalBallSpellWindow = new(crystalBall);
            (string spellName, string spellVariant) result = await addCrystalBallSpellWindow.ShowDialog<(string, string)>(window);
            if (result.spellName != null)
            {
                try
                {
                    return crystalBall.AddSpell(result.spellName, result.spellVariant);
                }
                catch (Exception e)
                {
                    ErrorMessages.Error(e.Message);
                }
            }
            return null;
        }

        [RelayCommand]
        private void Apport(bool? isChecked)
        {
            crystalBall.HasApport = (bool)isChecked;
        }
    }
}
