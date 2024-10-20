namespace DbdSavegameReader.Lib.Models;

public class DbdFearMarketObject
{
    public required string ItemId { get; init; }
    public required int Cost { get; init; }
    public required int ExperienceConversion { get; init; }
    public required bool Purchased { get; init; }
}