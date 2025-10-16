namespace Magehelper.Models.Messages;

public class ConfigAddedMessage(string value) : ValueChangedMessage<string>(value);