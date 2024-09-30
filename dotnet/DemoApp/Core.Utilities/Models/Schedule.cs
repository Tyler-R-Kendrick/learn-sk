namespace Core.Utilities.Models
{
    public record Schedule(List<GameDate> Dates);

    public record GameDate(List<Game> Games);

    public record Game(
        int GamePk,
        Guid GameGuid,
        DateTime GameDate,
        Teams Teams
    );

    public record Teams(
        GameTeam Away,
        GameTeam Home
    );

    public record GameTeam(
        TeamInformation Team
    );

    public record TeamInformation(
        int Id,
        string Name,
        string TeamName
    );
}
