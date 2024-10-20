namespace DbdSavegameReader.Lib.Models;

public class DbdBloodWebNode
{
    public required DbdBloodWebNodeProperties Properties { get; init; }
    public required object[] Gates { get; init; } // TODO
    public required string State { get; init; }
    public required string NodeId { get; init; }
    public required string ContentId { get; init; }
    public required DbdBloodWebChest BloodWebChest { get; init; }
}