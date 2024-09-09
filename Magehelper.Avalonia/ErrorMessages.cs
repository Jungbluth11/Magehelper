using System.Windows;

namespace Magehelper.Avalonia
{
    internal static class ErrorMessages
    {
        internal static void ReqError(string reqirements)
        {
            Error("Zauber kann nicht gewählt werden!\nVoraussetzungen: " + reqirements);
        }

        internal static void Error(string msg)
        {
            //MessageBox.Show(msg, "Magehelper", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}