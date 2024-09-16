#pragma warning disable SKEXP0110
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0101
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using Core.Utilities.Agents;
using Core.Utilities.Models;
using Core.Utilities.Extensions;
using Core.Utilities.Services;

namespace Solution6
{
    public class TicketAgent(MlbService mlbService) : BaseAgent
    {
        private readonly MlbService _mlbService = mlbService;
        private bool pluginsRegistered = false;

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
        public async Task<int> GetTeamsLastPlayedGameId(int teamId)
        {
            var startDate = DateTime.Now.AddDays(-4);
            var endDate = DateTime.Now.AddDays(-1);
            var t = await _mlbService.GetTeamSchedule(teamId, startDate, endDate);
            int gamePk = (await _mlbService.GetTeamSchedule(teamId, startDate, endDate)).Dates.Last().Games.Last().GamePk;

            return gamePk;
        }

        [KernelFunction, Description("Gets the play by play for a specific game.")]
        public async Task<string> GetGamePlayByPlay(int gameId)
        {
            string tabularData = (await _mlbService.GamePlayByPlay(gameId, 40)).FormatPlayByPlayData();
            return tabularData;
        }
    }
}
