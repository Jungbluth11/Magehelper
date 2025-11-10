namespace Magehelper.ViewModels.Controls;

public partial class AddCrystalBallSpellControlViewModel : ObservableObject
{
    [ObservableProperty] private bool _isSpellVariantVisible;
    [ObservableProperty] private string _selectedSpellVariant = string.Empty;
    [ObservableProperty] private string _variantDescription = string.Empty;
    [ObservableProperty] private bool _isVariantDescriptionVisible;
    public string[] SpellVariants => ["Variante 1", "Variante 2", "Beschreibung eingeben"];

    partial void OnSelectedSpellVariantChanged(string value)
    {
        if (value == "Beschreibung eingeben")
        {
            IsVariantDescriptionVisible = true;
        }
        else
        {
            IsVariantDescriptionVisible = false;
            VariantDescription = string.Empty;
        }
    }
}
