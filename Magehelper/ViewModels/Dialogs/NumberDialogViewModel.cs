

namespace Magehelper.ViewModels.Dialogs
{
    public partial class DialogNumberWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _numberValue = 0;
        private readonly Action<int> action;
        private readonly Func<int>? btnRollFunc;
        public string Caption { get; }
        public string DialogText { get; }
        public string BtnRollName { get; }
        public bool BtnRollVisible { get; }

        public DialogNumberWindowViewModel(string caption, string dialogText, Action<int> action,  bool btnRollVisible = false, string btnRollName = "", Func<int>? btnRollFunc = null)
        {
            if (string.IsNullOrEmpty(caption))
            {
                throw new ArgumentException($"\"{nameof(caption)}\" kann nicht NULL oder leer sein.", nameof(caption));
            }

            if (string.IsNullOrEmpty(dialogText))
            {
                throw new ArgumentException($"\"{nameof(dialogText)}\" kann nicht NULL oder leer sein.", nameof(dialogText));
            }

            if (btnRollVisible && string.IsNullOrEmpty(btnRollName))
            {
                throw new ArgumentException($"\"{nameof(btnRollName)}\" kann nicht NULL oder leer sein.", nameof(btnRollName));
            }

            if(btnRollVisible && btnRollFunc is null)
            {
                throw new ArgumentNullException(nameof(btnRollFunc));
            }

            Caption = caption + " - Magehelper";
            DialogText = dialogText;
            this.action = action;
            this.btnRollFunc = btnRollFunc;
            BtnRollName = btnRollName;
            BtnRollVisible = btnRollVisible;
        }

        [RelayCommand]
        private void Roll()
        {
#pragma warning disable CS8602 // not null if btnRollVisible is true
            NumberValue = btnRollFunc();
#pragma warning restore CS8602
        }

        [RelayCommand]
        private void Submit(Window window)
        {
            action(NumberValue);
            window.Close(true);
        }

    }
}
