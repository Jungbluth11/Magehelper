using System;
using System.Windows;

namespace Magehelper.WPF
{
    /// <summary>
    /// Interaktionslogik für DialogNumberWindow.xaml
    /// </summary>
    public partial class DialogNumberWindow : Window
    {
        public Func<int> btnRollAction;
        public int Value { get; private set; }

        public DialogNumberWindow(string caption, string dialogText)
        {
            Init(caption, dialogText);
        }

        public DialogNumberWindow(string caption, string dialogText, Func<int> btnRollAction, string btnRollName, bool btnRollEnabled)
        {
            if (string.IsNullOrEmpty(btnRollName))
            {
                throw new ArgumentException($"\"{nameof(btnRollName)}\" kann nicht NULL oder leer sein.", nameof(btnRollName));
            }
            Init(caption, dialogText);
            this.btnRollAction = btnRollAction ?? throw new ArgumentNullException(nameof(btnRollAction));
            StringBtnRollText.Content = btnRollName;
            BtnRoll.Visibility = Visibility.Visible;
            BtnRoll.IsEnabled = btnRollEnabled;
        }

        private void Init(string caption, string dialogText)
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

        private void BtnRoll_Click(object sender, RoutedEventArgs e)
        {
            TBoxNumber.Text = btnRollAction().ToString();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Value = int.Parse(TBoxNumber.Text);
            DialogResult = true;
            Close();
        }
    }
}