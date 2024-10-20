namespace DbdSavegameReader.Lib.Models;

public class DbdFearMarket
{
    public required DbdFearMarketObject[] ObjectsForSale { get; init; }
    public required string StartTime { get; init; }
    public required string EndTime { get; init; }
    public required string FearMarketEndDate { get; init; }
    public required string FearMarketStartDate { get; init; }
}