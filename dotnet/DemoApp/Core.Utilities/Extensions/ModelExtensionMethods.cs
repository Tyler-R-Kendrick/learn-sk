using Core.Utilities.Models;
using System.Text;
using System.Text.Json;

namespace Core.Utilities.Extensions
{
    public static class ModelExtensionMethods
    {
        public static string FormatScheduleData(this Schedule schedule)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine("| Home Team | Away Team | Date |");
            stringBuilder.AppendLine("| ----- | ----- | ----- |");

            foreach (GameDate gameDate in schedule.Dates)
            {
                foreach (Game game in gameDate.Games)
                {
                    stringBuilder.AppendLine($"| {game.Teams.Home.Team.Name} | {game.Teams.Away.Team.Name} | {JsonSerializer.Serialize(game.GameDate)} |");
                }
            }

            return stringBuilder.ToString();
        }

        public static string FormatTeamData(this MlbTeams teams)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine("| Team Id | Name |");
            stringBuilder.AppendLine("| ----- | ----- |");

            foreach (Team team in teams.Teams)
            {
                stringBuilder.AppendLine($"| {team.Id} | {team.Name} |");

            }

            return stringBuilder.ToString();
        }

        public static string FormatPlayByPlayData(this List<Play> plays)
        {
            StringBuilder stringBuilder = new();
            int counter = 0;

            stringBuilder.AppendLine("| Play Number | Play Result |");
            stringBuilder.AppendLine("| ----- | ----- |");

            foreach (Play play in plays)
            {
                counter++;
                stringBuilder.AppendLine($"| {counter} | {play.Result.Description} |");

            }

            return stringBuilder.ToString();
        }
    }
}
