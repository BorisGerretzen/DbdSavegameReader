namespace DbdSavegameReader.Lib.Models;

public class DbdSavegame
{
    public required int VersionNumber { get; init; }
    public required string UserId { get; init; }
    public required long SelectedCamper { get; init; }
    public required long SelectedSlasher { get; init; }
    public required int Experience { get; init; }
    public required int BonusExperience { get; init; }
    public required int FearTokens { get; init; }
    public required bool FirstTimePlaying { get; init; }
    public required string CurrentSeasonTicks { get; init; }
    public int? CumulativeMatches { get; init; }
    public int? CumulativeMatchesAsSurvivor { get; init; }
    public int? CumulativeMatchesAsKiller { get; init; }
    public DateTime? LastMatchTimestamp { get; init; }
    public DateTime? LastSessionTimestamp { get; init; }
    public int? CumulativeSessions { get; init; }
    public string? CumulativePlaytime { get; init; }
    public required long LastConnectedCharacterIndex { get; init; }
    public required string OngoingGameTime { get; init; }
    public required DbdLoadout LastConnectedLoadout { get; init; }
    public DbdPageVisited[]? PageVisited { get; init; }
    public required DbdCharacter[] CharacterData { get; init; }
    public DbdStatProgression[]? PlayerStatProgression { get; init; }
    public DbdFearMarket? FearMarket { get; init; }
}