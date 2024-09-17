namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class BoneCubControlViewModel(BoneCub boneCub) : ObservableObject
    {
        private readonly BoneCub boneCub = boneCub;

        public ArtifactSpell? AddSpell()
        {
            //AddArtifactSpellWindow addArtifactSpellWindow = new AddArtifactSpellWindow("Keulenzauber", boneCub);
            //if (addArtifactSpellWindow.ShowDialog() == true)
            //{
            //    try
            //    {
            //        return boneCub.AddSpell(addArtifactSpellWindow.SpellName);
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
            boneCub.HasApport = (bool)isChecked;
        }
    }
}
