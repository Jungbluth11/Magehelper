namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class BowlControlViewModel : ObservableObject
    {
        private readonly Bowl bowl;

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

        public BowlControlViewModel(Bowl bowl)
        {
            this.bowl = bowl;
            FireAndIceVisibility = bowl.HasFireAndIce;
        }

        public ArtifactSpell? AddSpell()
        {
            //AddArtifactSpellWindow addArtifactSpellWindow = new AddArtifactSpellWindow("Schalenzauber", bowl);
            //if (addArtifactSpellWindow.ShowDialog() == true)
            //{
            //    try
            //    {
            //        if (addArtifactSpellWindow.SpellName == "Feuer und Eis")
            //        {
            //            FireAndIceVisibility = true;
            //        }
            //        return bowl.AddSpell(addArtifactSpellWindow.SpellName);
            //    }
            //    catch (Exception e)
            //    {
            //        ErrorMessages.Error(e.Message);
            //    }
            //}
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
