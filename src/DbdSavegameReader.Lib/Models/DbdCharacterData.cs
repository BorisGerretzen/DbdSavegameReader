namespace DbdSavegameReader.Lib.Models;

public class DbdCharacterData
{
    public required int BloodWebLevel { get; init; }
    public required int PrestigeLevel { get; init; }
    public required int TimesConfronted { get; init; }
    public required DateTime[] PrestigeEarnedDates { get; init; }
    public required DbdBloodWeb BloodWebData { get; init; }
    public required DbdLoadout CharacterLoadoutData { get; init; }
    public required DbdInventory[] InventoryData { get; init; }
    public DbdStatProgression[]? StatProgression { get; init; }
    public string[]? CurrentCustomization { get; init; }
}