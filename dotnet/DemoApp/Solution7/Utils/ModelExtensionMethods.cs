using Solution7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Solution7.Utils
{
    public static class ModelExtensionMethods
    {
        public static string FormatScheduleData(this Schedule schedule)
        {
            StringBuilder stringBuilder = new StringBuilder();

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
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("| Team Id | Name |");
            stringBuilder.AppendLine("| ----- | ----- |");

            foreach (Team team in teams.Teams)
            {
                stringBuilder.AppendLine($"| {team.Id} | {team.Name} |");

            }

            return stringBuilder.ToString();
        }
    }
}
