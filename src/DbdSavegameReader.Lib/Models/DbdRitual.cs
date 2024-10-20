namespace DbdSavegameReader.Lib.Models;

public class DbdRitual
{
    public required string RitualKey { get; init; }
    public required int DifficultyTier { get; init; }
    public required string[] TrackedEvents { get; init; }
    public required long[] CharacterIds { get; init; }
    public required string[] Roles { get; init; }
    public required float Progress { get; init; }
    public required int Threshold { get; init; }
    public required int Tolerance { get; init; }
    public required int DisplayThreshold { get; init; }
    public required int ExpReward { get; init; }
    public required bool Active { get; init; }
    public required bool Rewarded { get; init; }
    public required bool IsNew { get; init; }
}