#pragma warning disable SKEXP0110
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0101
using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Net.Http.Json;
using Core.Utilities.Models;
using Core.Utilities.Extensions;
using Core.Utilities.Agents;
using Core.Utilities.Services;

namespace Solution7
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

        [KernelFunction, Description("Gets the play by play for a specific game.")]
        public async Task<string> GetGamePlayByPlay()
        {
            string tabularData = (await _mlbService.GamePlayByPlay(40)).FormatPlayByPlayData();
            return tabularData;
        }
    }
}
