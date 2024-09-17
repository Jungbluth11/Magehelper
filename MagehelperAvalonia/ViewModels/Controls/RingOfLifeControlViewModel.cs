namespace Magehelper.Avalonia.ViewModels.Controls
{
    public partial class RingOfLifeControlViewModel(RingOfLife ringOfLife) : ObservableObject
    {
        private readonly RingOfLife ringOfLife = ringOfLife;

        public ArtifactSpell? AddSpell()
        {
            //AddArtifactSpellWindow addArtifactSpellWindow = new AddArtifactSpellWindow("Schlangenringzauber", ringOfLife);
            //if (addArtifactSpellWindow.ShowDialog() == true)
            //{
            //    try
            //    {
            //        return ringOfLife.AddSpell(addArtifactSpellWindow.SpellName);
            //    }
            //    catch (Exception e)
            //    {
            //        ErrorMessages.Error(e.Message);
            //    }
            //}
            return null;
        }
    }
}
