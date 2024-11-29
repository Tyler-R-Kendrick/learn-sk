using Baseball.Abstractions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Team[] teams = [
    new(1, "Name", "Team Code", "Abbr", "Team Name"),
    new(2, "Name", "Team Code", "Abbr", "Team Name")
];

app.MapGet("/game/{gameId}/playByPlay", (int gameId) => new MlbTeams(teams));
app.MapGet("/teams", (int sportId) => new MlbTeams(teams));
app.MapGet("/schedule", (ScheduleRequest request) => new Schedule([
    new([
        new(1,
            Guid.NewGuid(),
            DateTime.Now,
            new(
                new(
                    new(1,
                        "Name",
                        "Team Name"
                    )
                ),
                new(
                    new(2,
                        "Name",
                        "Team Name"
                    )
                )
            )
        )
    ])
]));

app.Run();
