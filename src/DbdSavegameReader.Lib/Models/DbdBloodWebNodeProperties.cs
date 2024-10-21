namespace DbdSavegameReader.Lib.Models;

public class DbdBloodWebNodeProperties
{
    public required string ContentType { get; init; }
    public required string Rarity { get; init; }

    /// <summary>
    ///     I don't know what this is, if you know please open an issue on the GitHub repository.
    /// </summary>
    public required object[] Tags { get; init; }
}