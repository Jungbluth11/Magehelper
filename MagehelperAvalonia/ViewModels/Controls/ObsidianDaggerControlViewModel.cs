namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class ObsidianDaggerControlViewModel(ObsidianDagger obsidianDagger) : ObservableObject
    {
        private readonly ObsidianDagger obsidianDagger = obsidianDagger;

        public ArtifactSpell? AddSpell()
        {
            //AddArtifactSpellWindow addArtifactSpellWindow = new AddArtifactSpellWindow("Dolchzauber", obsidianDagger);
            //if (addArtifactSpellWindow.ShowDialog() == true)
            //{
            //    try
            //    {
            //        return obsidianDagger.AddSpell(addArtifactSpellWindow.SpellName);
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
            obsidianDagger.HasApport = (bool)isChecked;
        }
    }
}
