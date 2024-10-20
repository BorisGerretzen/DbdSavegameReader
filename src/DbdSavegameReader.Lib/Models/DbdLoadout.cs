namespace DbdSavegameReader.Lib.Models;

public class DbdLoadout
{
    public required string Item { get; init; }
    public required string[] ItemAddOns { get; init; }
    public required string[] CamperPerks { get; init; }
    public required long[] CamperPerkLevels { get; init; }
    public required string CamperFavor { get; init; }
    public required string Power { get; init; }
    public required string[] PowerAddOns { get; init; }
    public required string[] SlasherPerks { get; init; }
    public required long[] SlasherPerkLevels { get; init; }
    public required string SlasherFavor { get; init; }
}