using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;

namespace Magehelper.Avalonia.Views.Tabs;

public partial class TabContentSpellStorage : UserControl
{
    TabContentSpellStorageViewModel viewModel;
    public TabContentSpellStorage()
    {
        viewModel = TabContentSpellStorageViewModel.Instance;
        DataContext = viewModel;
        InitializeComponent();
    }

    public void ResetTab()
    {
        viewModel.ResetTab();
    }

    private async void Button_Click(object? sender, RoutedEventArgs e)
    {
        Window mainWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow;
        AddSpellWindow addSpellWindow = new();
        StoragedSpell spell = await addSpellWindow.ShowDialog<StoragedSpell>(mainWindow);
        if (string.IsNullOrEmpty(spell.Name))
        {
            viewModel.SpellStorageList[spell.Storage].Spells.Add(spell);
        }
    }
}