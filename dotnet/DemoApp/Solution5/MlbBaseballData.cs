using Core.Utilities.Services;
using Core.Utilities.Extensions;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace Solution5;

public class MlbBaseballDataPlugin(MlbService mlbService)
{
    [KernelFunction, Description("Gets a list of games for a specific baseball team.")]
    public async Task<string> GetTeamScheduleData(int teamId)
    {
        var startDate = DateTime.Now.AddDays(-1);
        var endDate = DateTime.Now.AddDays(14);
        var response = await mlbService.GetTeamSchedule(teamId, startDate, endDate);
        return response.FormatScheduleData();
    }

    [KernelFunction, Description("Gets a list of teams and their Ids.")]
    public async Task<string> GetTeamIdsData()
    {
        var response = await mlbService.GetTeams();
        return response.FormatTeamData();
    }


    [KernelFunction, Description("Gets the teams last played game id.")]
    public async Task<int> GetTeamsLastPlayedGameId(int teamId)
    {
        var startDate = DateTime.Now.AddDays(-4);
        var endDate = DateTime.Now.AddDays(-1);
        var response = await mlbService.GetTeamSchedule(teamId, startDate, endDate);
        var date = response.Dates.Last();
        var game = date.Games.Last();
        return game.GamePk;
    }

    [KernelFunction, Description("Gets the play by play for a specific game.")]
    public async Task<string> GetGamePlayByPlay(int gameId)
    {
        var response = await mlbService.GamePlayByPlay(gameId, 40);
        return response.FormatPlayByPlayData();
    }
}

