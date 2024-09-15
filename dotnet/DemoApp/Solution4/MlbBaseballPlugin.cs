using Core.Utilities.Extensions;
using Microsoft.SemanticKernel;
using System.ComponentModel;
using Core.Utilities.Services;

namespace Solution4
{
    public class MlbBaseballPlugin(MlbService mlbService)
    {
        private readonly MlbService _mlbService = mlbService;

        [KernelFunction, Description("Gets a list of games for a specific baseball team.")]
        public async Task<string> GetTeamScheduleData(int teamId) {
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

