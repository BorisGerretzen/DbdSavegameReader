namespace DbdSavegameReader.Lib.Models;

public class DbdBloodWeb
{
    public required int VersionNumber { get; init; }
    public required int Level { get; init; }
    public required DbdRing[] RingData { get; init; }
    public required string[] Paths { get; init; }
    public required string EntityCurrentNode { get; init; }
}