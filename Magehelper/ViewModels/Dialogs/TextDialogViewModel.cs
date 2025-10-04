

namespace Magehelper.ViewModels.Dialogs
{
    public partial class DialogTextWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _textValue = string.Empty;
        private readonly Action<string> action;
        public string Caption { get; }
        public string DialogText { get; }

        public DialogTextWindowViewModel(string caption, string dialogText, Action<string> action)
        {
            if (string.IsNullOrEmpty(caption))
            {
                throw new ArgumentException($"\"{nameof(caption)}\" kann nicht NULL oder leer sein.", nameof(caption));
            }

            if (string.IsNullOrEmpty(dialogText))
            {
                throw new ArgumentException($"\"{nameof(dialogText)}\" kann nicht NULL oder leer sein.", nameof(dialogText));
            }

            Caption = caption + " - Magehelper";
            DialogText = dialogText;
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        private bool CanSubmit()
        {
            if (TextValue != string.Empty)
            {
                return true;
            }
            return false;
        }

        [RelayCommand(CanExecute = nameof(CanSubmit))]
        private void Submit(Window window)
        {
            action(TextValue);
            window.Close();
        }
    }
}
