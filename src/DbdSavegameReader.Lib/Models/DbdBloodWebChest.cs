using System.Text.Json.Serialization;

namespace DbdSavegameReader.Lib.Models;

public class DbdBloodWebChest
{
    [JsonPropertyName("iD")] public required string Id { get; init; }
    public required string Rarity { get; init; }
    public required int[] GivenItemRarity { get; init; }
}