namespace Baseball.Abstractions;
    public record MlbTeams(Team[] Teams);

    public record Team(
        int Id,
        string Name,
        string TeamCode,
        string Abbreviation,
        string TeamName
    );
    public record PlayByPlay(Play[] AllPlays);

    public record Play(PlayResult Result);

    public record PlayResult(
        string Description
    );
public record ScheduleRequest(
    int[] SportId,
    DateTime StartDate,
    DateTime EndDate,
    int TeamId,
    string TimeZone,
    string[] GameType,
    string Language,
    int[] LeagueId,
    string[] Hydrate,
    string[] SortBy
);

public record Schedule(GameDate[] Dates);

public record GameDate(Game[] Games);

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
