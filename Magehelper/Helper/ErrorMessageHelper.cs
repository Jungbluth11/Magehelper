namespace Magehelper.Helper;

public static class ErrorMessageHelper
{
    public static async Task ShowErrorDialog(string msg, XamlRoot xamlRoot)
    {
        ContentDialog dialog = new()
        {
            Content = msg,
            XamlRoot = xamlRoot,
            CloseButtonText = "OK"
        };

        await dialog.ShowAsync();
    }
}
