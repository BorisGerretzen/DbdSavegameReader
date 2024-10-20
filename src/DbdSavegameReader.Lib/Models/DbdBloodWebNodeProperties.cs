namespace DbdSavegameReader.Lib.Models;

public class DbdBloodWebNodeProperties
{
    public required string ContentType { get; init; }
    public required string Rarity { get; init; }
    public required object[] Tags { get; init; } // TODO
}