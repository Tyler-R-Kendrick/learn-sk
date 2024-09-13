using Microsoft.SemanticKernel;
using Solution4.Models;
using Solution4.Utils;
using System.ComponentModel;
using System.Net.Http.Json;

namespace Solution4
{
    public class MlbBaseballPlugin(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        [KernelFunction, Description("Gets the teams schedule.")]
        public async Task<string> GetTeamScheduleData() {
            string startDate = DateTime.Now.AddDays(-1).ToString("yyy-MM-dd");
            string endDate = DateTime.Now.AddDays(14).ToString("yyy-MM-dd");
            int teamId = 112;
            var requestUri = $"schedule?sportId=1&sportId=51&sportId=21&startDate={startDate}&endDate={endDate}&teamId={teamId}&timeZone=America/New_York&gameType=E&&gameType=S&&gameType=R&&gameType=F&&gameType=D&&gameType=L&&gameType=W&&gameType=A&&gameType=C&language=en&leagueId=104&&leagueId=103&&leagueId=160&&leagueId=590&hydrate=team,linescore(matchup,runners),xrefId,story,flags,statusFlags,broadcasts(all),venue(location),decisions,person,probablePitcher,stats,game(content(media(epg),summary),tickets),seriesStatus(useOverride=true)&sortBy=gameDate,gameStatus,gameType";
            var response = await _httpClient.GetAsync(requestUri);
            Schedule schedule = await response.Content.ReadFromJsonAsync<Schedule>();
            var tabularData = schedule.FormatScheduleData();

            return tabularData;
        }

        [KernelFunction, Description("Gets a list of teams and their Ids.")]
        public async Task<string> GetTeamIdsData()
        {
            var requestUri = $"teams?sportId=1";
            var response = await _httpClient.GetAsync(requestUri);
            MlbTeams mlbTeams = await response.Content.ReadFromJsonAsync<MlbTeams>();
            var tabularData = mlbTeams.FormatTeamData();

            return tabularData;
        }
    }
}

