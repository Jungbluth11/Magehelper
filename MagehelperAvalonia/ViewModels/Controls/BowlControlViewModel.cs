using Avalonia.Controls;

namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class BowlControlViewModel : ObservableObject
    {
        private readonly Bowl bowl;
        private readonly ArtifactSpellsControlViewModel artifactSpellsControlViewModel;
        [ObservableProperty]
        private string temperatureCategoryStart = "Normal";
        [ObservableProperty]
        private string temperatureCategoryTarget = "Normal";
        [ObservableProperty]
        private int fireAndIceDuration = 1;
        [ObservableProperty]
        private int fireAndIceCost = 0;
        [ObservableProperty]
        private bool fireAndIceVisibility = false;

        public BowlControlViewModel(Bowl bowl, ArtifactSpellsControlViewModel artifactSpellsControlViewModel)
        {
            this.bowl = bowl;
            this.artifactSpellsControlViewModel = artifactSpellsControlViewModel;
            this.artifactSpellsControlViewModel.AddSpellFunc = AddSpell;
            FireAndIceVisibility = bowl.HasFireAndIce;
        }

        public ArtifactSpell? AddSpell(Window window)
        {
            AddArtifactSpellWindow addArtifactSpellWindow = new("Schalenzauber", bowl);
            string result = addArtifactSpellWindow.ShowDialog<string>(window).Result;
            if (result != null)
            {
                try
                {
                    if (result == "Feuer und Eis")
                    {
                        FireAndIceVisibility = true;
                    }
                    return bowl.AddSpell(result);
                }
                catch (Exception ex)
                {
                    ErrorMessages.Error(ex.Message);
                }
            }
            return null;
            return null;
        }

        [RelayCommand]
        private void Apport(bool? isChecked)
        {
            bowl.HasApport = (bool)isChecked;
        }

        private void FireAndIce()
        {
            FireAndIceCost = bowl.FireAndIce(TemperatureCategoryStart, TemperatureCategoryTarget, FireAndIceDuration);
        }

        partial void OnTemperatureCategoryStartChanged(string value)
        {
            FireAndIce();
        }

        partial void OnTemperatureCategoryTargetChanged(string value)
        {
            FireAndIce();
        }

        partial void OnFireAndIceDurationChanged(int value)
        {
            FireAndIce();
        }
    }
}
