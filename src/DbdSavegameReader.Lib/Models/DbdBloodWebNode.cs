namespace DbdSavegameReader.Lib.Models;

public class DbdBloodWebNode
{
    public required DbdBloodWebNodeProperties Properties { get; init; }

    /// <summary>
    ///     I don't know what this is, if you know please open an issue on the GitHub repository.
    /// </summary>
    public required object[] Gates { get; init; }

    public required string State { get; init; }
    public required string NodeId { get; init; }
    public required string ContentId { get; init; }
    public required DbdBloodWebChest BloodWebChest { get; init; }
}