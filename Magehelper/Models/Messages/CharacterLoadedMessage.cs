namespace Magehelper.Models.Messages;

public class CharacterLoadedMessage(string value) : ValueChangedMessage<string>(value);
