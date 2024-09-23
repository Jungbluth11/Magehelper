using Avalonia.Platform.Storage;

namespace Magehelper.Avalonia.Models
{
    public static class FileTypes
    {
        public static FilePickerFileType MagehelperFileType => new("Magehelper-Datei")
        {
            Patterns = ["*.magehelper"],
            AppleUniformTypeIdentifiers = ["public.xml."],
            MimeTypes = ["xml/*"]
        };
        public static FilePickerFileType XmlFileType => new("XML")
        {
            Patterns = ["*.xml"],
            AppleUniformTypeIdentifiers = ["public.xml."],
            MimeTypes = ["xml/*"]
        };
    }
}