using System;
using System.Windows;
using System.Windows.Input;
using DSAUtils.HeldentoolInterop;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für LoadFromToolWindow.xaml
    /// </summary>
    public partial class LoadFromToolWindow : Window
    {
        private readonly Charakter[] characters;
        private readonly Action<Charakter> characterAction;
        public LoadFromToolWindow(Charakter[] characters, Action<Charakter> action)
        {
            InitializeComponent();
            characterAction = action;
            this.characters = characters;
            foreach (Charakter c in characters)
            {
                Characters.Items.Add(c.ToString());
            }
        }

        private void Load()
        {
            if (Characters.SelectedIndex != -1)
            {
                characterAction(characters[Characters.SelectedIndex]);
                Close();
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Characters_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Load();
        }
    }
}