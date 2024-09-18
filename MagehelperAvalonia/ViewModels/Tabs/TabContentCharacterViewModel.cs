using Avalonia.Controls;
using Avalonia.Interactivity;
using DSAUtils.HeldentoolInterop;
using Magehelper.Avalonia.Views.Windows;

namespace Magehelper.Avalonia.ViewModels.Tabs
{
    public class TabContentCharacterViewModel
    {
        private static TabContentCharacterViewModel _instance = new();
        public static TabContentCharacterViewModel Instance => _instance;
        public Character Character { get; set; }

        public TabContentCharacterViewModel()
        {
            Character = new Character(MainWindowViewModel.Instance.Core);

        }

        public void LoadCharacterFromTool(Charakter character)
        {

        }

        public void LoadCharacterFromFile(string path)
        {
            
            
        }
    }
}
