using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Controls;

public partial class SpellStorageControl : UserControl
{
    public SpellStorageControl(int spellStorageIndex, SpellStorage spellStorage)
    {
        ArgumentNullException.ThrowIfNull(spellStorage);

        DataContext = new SpellStorageControlViewModel(spellStorageIndex, spellStorage);
        InitializeComponent();
        Name = "SpellStorageControl" + spellStorageIndex.ToString();
    }
}