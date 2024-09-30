using Ardalis.GuardClauses;
using Core.Utilities.Models;
using System.Net.Http.Json;

namespace Core.Utilities.Services
{
    public class MlbService
    {
        private readonly HttpClient _httpClient;

        public MlbService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("http://statsapi.mlb.com/api/v1/");
            _httpClient = httpClient;
        }
        public async Task<Schedule> GetTeamSchedule(int teamId, DateTime startDate, DateTime endDate)
        {
            string startDateFormatted = startDate.ToString("yyy-MM-dd");
            string endDateFormatted = endDate.ToString("yyy-MM-dd");
            var requestUri = $"schedule?sportId=1&sportId=51&sportId=21&startDate={startDateFormatted}&endDate={endDateFormatted}&teamId={teamId}&timeZone=America/New_York&gameType=E&&gameType=S&&gameType=R&&gameType=F&&gameType=D&&gameType=L&&gameType=W&&gameType=A&&gameType=C&language=en&leagueId=104&&leagueId=103&&leagueId=160&&leagueId=590&hydrate=team,linescore(matchup,runners),xrefId,story,flags,statusFlags,broadcasts(all),venue(location),decisions,person,probablePitcher,stats,game(content(media(epg),summary),tickets),seriesStatus(useOverride=true)&sortBy=gameDate,gameStatus,gameType";
            Schedule schedule = await GetHttpResponse<Schedule>(requestUri);

            return schedule;
        }

        public async Task<MlbTeams> GetTeams()
        {
            var requestUri = $"teams?sportId=1";
            MlbTeams mlbTeams = await GetHttpResponse<MlbTeams>(requestUri);

            return mlbTeams;
        }

        public async Task<List<Play>> GamePlayByPlay(int gameId, int maxPlayByPlay)
        {
            string requestUri = $"game/{gameId}/playByPlay";
            PlayByPlay playByPlay = await GetHttpResponse<PlayByPlay>(requestUri);

            return playByPlay.AllPlays.Take(maxPlayByPlay).ToList();
        }

        private async Task<T> GetHttpResponse<T>(string requestUri)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}, message: {errorMessage}");
            }

            T? data = await response.Content.ReadFromJsonAsync<T>();
            Guard.Against.Null(data);

            return data;
        }
    }
}
