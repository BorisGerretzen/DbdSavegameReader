namespace DbdSavegameReader.Lib.Models;

public class DbdDailyRituals
{
    public required DateTime LastRitualReceivedDate { get; init; }
    public required DateTime LastRitualPopupDate { get; init; }
    public required DateTime LastRitualDismissedDate { get; init; }
    public required DbdRitual[] Rituals { get; init; }
}