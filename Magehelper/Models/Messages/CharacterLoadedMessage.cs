namespace Magehelper.Models.Messages;

public class CharacterLoadedMessage(Charakter value) : ValueChangedMessage<Charakter>(value);
