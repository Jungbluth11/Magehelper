using Avalonia.Controls;

namespace Magehelper.Avalonia.Views.Windows;

public partial class PetGeneratorWindow : Window
{
    public PetGeneratorWindow(Pet pet, Action action)
    {
        DataContext = new PetGeneratorWindowViewModel(pet, action);
        InitializeComponent();
    }
}