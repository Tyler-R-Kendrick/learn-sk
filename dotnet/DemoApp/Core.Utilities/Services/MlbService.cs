using Core.Utilities.Models;
using System.Net.Http.Json;

namespace Core.Utilities.Services
{
    public class MlbService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<Schedule> GetTeamSchedule(int teamId, DateTime startDate, DateTime endDate)
        {
            string startDateFormatted = DateTime.Now.AddDays(-1).ToString("yyy-MM-dd");
            string endDateFormatted = DateTime.Now.AddDays(14).ToString("yyy-MM-dd");
            var requestUri = $"schedule?sportId=1&sportId=51&sportId=21&startDate={startDateFormatted}&endDate={endDateFormatted}&teamId={teamId}&timeZone=America/New_York&gameType=E&&gameType=S&&gameType=R&&gameType=F&&gameType=D&&gameType=L&&gameType=W&&gameType=A&&gameType=C&language=en&leagueId=104&&leagueId=103&&leagueId=160&&leagueId=590&hydrate=team,linescore(matchup,runners),xrefId,story,flags,statusFlags,broadcasts(all),venue(location),decisions,person,probablePitcher,stats,game(content(media(epg),summary),tickets),seriesStatus(useOverride=true)&sortBy=gameDate,gameStatus,gameType";
            var response = await _httpClient.GetAsync(requestUri);
            Schedule schedule = await response.Content.ReadFromJsonAsync<Schedule>();

            return schedule;
        }

        public async Task<MlbTeams> GetTeams()
        {
            var requestUri = $"teams?sportId=1";
            var response = await _httpClient.GetAsync(requestUri);
            MlbTeams mlbTeams = await response.Content.ReadFromJsonAsync<MlbTeams>();

            return mlbTeams;
        }
    }
}
