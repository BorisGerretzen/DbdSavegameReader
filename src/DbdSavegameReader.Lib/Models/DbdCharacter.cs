namespace DbdSavegameReader.Lib.Models;

public class DbdCharacter
{
    /// <summary>
    ///     The ID of the character.
    ///     Some older savegames use <see cref="CharacterDataId" />.
    /// </summary>
    public long? CharacterId { get; init; }

    /// <summary>
    ///     The ID of the character used in the old savegame format.
    /// </summary>
    public long? CharacterDataId { get; init; }

    public required DbdCharacterData CharacterDataValue { get; init; }
}