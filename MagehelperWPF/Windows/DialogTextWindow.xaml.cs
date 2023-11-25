using System;
using System.Windows;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für DialogTextWindow.xaml
    /// </summary>
    public partial class DialogTextWindow : Window
    {
        public string value { get; private set; }

        public DialogTextWindow(string caption, string dialogText)
        {
            if (string.IsNullOrEmpty(caption))
            {
                throw new ArgumentException($"\"{nameof(caption)}\" kann nicht NULL oder leer sein.", nameof(caption));
            }
            if (string.IsNullOrEmpty(dialogText))
            {
                throw new ArgumentException($"\"{nameof(dialogText)}\" kann nicht NULL oder leer sein.", nameof(dialogText));
            }
            InitializeComponent();
            Title = caption + " - Magehelper";
            StringDialogText.Content = dialogText;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            value = TBoxText.Text;
            DialogResult = true;
            Close();
        }
    }
}