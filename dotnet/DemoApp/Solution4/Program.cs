using Solution4;

var httpClient = new HttpClient() { BaseAddress = new Uri("http://statsapi.mlb.com/api/v1/") };
var mlbBaseballPlugin = new MlbBaseballPlugin(httpClient);

var test = await mlbBaseballPlugin.GetTeamIdsData();
Console.WriteLine("Hello, World!");
