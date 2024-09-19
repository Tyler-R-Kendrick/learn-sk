using Core.Utilities.Extensions;
using Microsoft.SemanticKernel;
using System.ComponentModel;
using Core.Utilities.Services;
using Core.Utilities.Models;
using System.Text;
using System.Diagnostics.Metrics;

namespace Solution4
{
    public class MlbBaseballData(MlbService mlbService)
    {
        private readonly MlbService _mlbService = mlbService;

        [KernelFunction, Description("Gets a list of games for a specific baseball team.")]
        public async Task<string> GetTeamScheduleData(int teamId) 
        {
            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now.AddDays(14);         
            string tabularData = (await _mlbService.GetTeamSchedule(teamId, startDate, endDate)).FormatScheduleData();

            return tabularData;
        }

        [KernelFunction, Description("Gets a list of teams and their Ids.")]
        public async Task<string> GetTeamIdsData()
        {
            string tabularData = (await _mlbService.GetTeams()).FormatTeamData();
            return tabularData;
        }


        [KernelFunction, Description("Gets the teams last played game id.")]
        public async Task<string> GetTeamsLastPlayedGameId(int teamId)
        {
            var startDate = DateTime.Now.AddDays(-4);
            var endDate = DateTime.Now.AddDays(-1);
            var t = await _mlbService.GetTeamSchedule(teamId, startDate, endDate);
            Game game = (await _mlbService.GetTeamSchedule(teamId, startDate, endDate)).Dates.Last().Games.Last();

            return game.FormatGameData();
        }

        [KernelFunction, Description("Gets the play by play for a specific game.")]
        public async Task<string> GetGamePlayByPlay(int gameId)
        {
            string tabularData = (await _mlbService.GamePlayByPlay(gameId, 40)).FormatPlayByPlayData();
            return tabularData;
        }
    }
}

