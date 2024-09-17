using System.Windows;

namespace Magehelper.Avalonia
{
    public static class ErrorMessages
    {
        public static void ReqError(string reqirements)
        {
            Error("Zauber kann nicht gewählt werden!\nVoraussetzungen: " + reqirements);
        }

        public static void Error(string msg)
        {
            //MessageBox.Show(msg, "Magehelper", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}