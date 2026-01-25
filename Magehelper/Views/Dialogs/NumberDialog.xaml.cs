namespace Magehelper.Views.Dialogs;

public sealed partial class NumberDialog : ContentDialog
{
    public Action<int>? SubmitAction { get; init; }
    public Func<(int, string)>? RollFunc { get; init; }

    public NumberDialog()
    {
        InitializeComponent();

        if (RollFunc != null)
        {
            BtnRoll.Visibility = Visibility.Visible;
        }

        if (Core.Core.GetInstance().Character != null)
        {
            BtnRoll.IsEnabled = true;
        }
    }

    private void BtnRoll_OnClick(object sender, RoutedEventArgs e)
    {
        (int item1, string item2) = RollFunc!.Invoke();
        InputNumber.Value = item1;
        RollResultText.Text = item2;
    }

    private void InputNumber_OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            SubmitAction?.Invoke((int) InputNumber.Value);
        }
    }

    private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        SubmitAction?.Invoke((int) InputNumber.Value);
    }
}