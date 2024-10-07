namespace Core.Utilities.Models
{
    public record MlbTeams(List<Team> Teams);

    public record Team(
        int Id,
        string Name,
        string TeamCode,
        string Abbreviation,
        string TeamName
    );
}
