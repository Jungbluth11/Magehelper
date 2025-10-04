namespace Magehelper.Models.Messages;

public class FileActionMessage(FileAction value) : ValueChangedMessage<FileAction>(value);
