using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Controls;

public partial class SpellStorageControl : UserControl
{
    public ObservableCollection<StoragedSpell> Spells { get; }

    public SpellStorageControl(int spellStorageIndex, SpellStorage spellStorage)
    {
        ArgumentNullException.ThrowIfNull(spellStorage);

        DataContext = new SpellStorageControlViewModel(spellStorageIndex, spellStorage);
#pragma warning disable CS8602
        Spells = (DataContext as SpellStorageControlViewModel).Spells;
#pragma warning restore CS8602
        InitializeComponent();
        Name = "SpellStorageControl" + spellStorageIndex.ToString();
    }
}