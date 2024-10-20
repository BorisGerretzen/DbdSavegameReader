namespace DbdSavegameReader.Lib.Models;

public class DbdStatProgression
{
    public required int Version { get; init; }
    public required string Name { get; init; }
    public required int Value { get; init; }
}