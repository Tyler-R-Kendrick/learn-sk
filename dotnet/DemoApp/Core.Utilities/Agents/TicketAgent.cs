using Microsoft.SemanticKernel;
using System.ComponentModel;
using Core.Utilities.Extensions;
using Core.Utilities.Services;

namespace Core.Utilities.Agents;

public class TicketAgent(MlbService mlbService) : BaseAgent
{
    private readonly MlbService _mlbService = mlbService;

    [KernelFunction, Description("Gets a list of games for a specific baseball team.")]
    public async Task<string> GetTeamScheduleData(int teamId)
    {
        var startDate = DateTime.Now.AddDays(-1);
        var endDate = DateTime.Now.AddDays(14);
        var teamSchedule = await _mlbService.GetTeamSchedule(teamId, startDate, endDate);
        return teamSchedule.FormatScheduleData();
    }

    [KernelFunction, Description("Gets a list of teams and their Ids.")]
    public async Task<string> GetTeamIdsData()
    {
        var teams = await _mlbService.GetTeams();
        return teams.FormatTeamData();
    }

    [KernelFunction, Description("Gets the teams last played game id.")]
    public async Task<int> GetTeamsLastPlayedGameId(int teamId)
    {
        var startDate = DateTime.Now.AddDays(-4);
        var endDate = DateTime.Now.AddDays(-1);
        var teamSchedule = await _mlbService.GetTeamSchedule(teamId, startDate, endDate);
        var lastDate = teamSchedule.Dates[^1];
        var lastGame = lastDate.Games[^1];
        return lastGame.GamePk;
    }

    [KernelFunction, Description("Gets the play by play for a specific game.")]
    public async Task<string> GetGamePlayByPlay(int gameId)
    {
        var playByPlay = await _mlbService.GamePlayByPlay(gameId, 40);
        return playByPlay.FormatPlayByPlayData();
    }
}
