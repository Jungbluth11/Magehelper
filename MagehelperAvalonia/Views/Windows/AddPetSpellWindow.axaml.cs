using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Windows;

public partial class AddPetSpellWindow : Window
{
    public AddPetSpellWindow(Pet pet)
    {
        DataContext = new AddPetSpellWindowViewModel(pet);
        InitializeComponent();
    }
}