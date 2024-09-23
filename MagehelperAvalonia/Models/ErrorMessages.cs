using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Magehelper.Avalonia.Models
{
    public static class ErrorMessages
    {
        public static void ReqError(string reqirements)
        {
            Error("Zauber kann nicht gewählt werden!\nVoraussetzungen: " + reqirements);
        }

        public static void Error(string msg)
        {
            Bitmap imageIcon = new(AssetLoader.Open(new Uri("avares://magehelper/Assets/exclamation.png")));

            MessageBoxGenerator.GetMessageBox(msg, MessageBoxGenerator.Buttons.OK, imageIcon).ShowAsync();
        }
    }
}