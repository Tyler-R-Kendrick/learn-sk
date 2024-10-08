from typing import List


class Game:
    def __init__(self, game_pk: int, game_guid: str, game_date: str, teams: dict):
        self.game_pk = game_pk
        self.game_guid = game_guid
        self.game_date = game_date
        self.teams = teams

class GameDate:
    def __init__(self, games: List[Game]):
        self.games = games

class Schedule:
    def __init__(self, game_dates: List[GameDate]):
        self.game_dates = game_dates

class TeamInformation:
    def __init__(self, id: int, name: str, team_name: str):
        self.id = id
        self.name = name
        self.team_name = team_name

class GameTeam:
    def __init__(self, team: TeamInformation):
        self.team = team

class Teams:
    def __init__(self, away: GameTeam, home: GameTeam):
        self.away = away
        self.home = home

class Team:
    def __init__(self, id: int, name: str, team_code: str, abbreviation: str, team_name: str):
        self.id = id
        self.name = name
        self.team_code = team_code
        self.abbreviation = abbreviation
        self.team_name = team_name

class MlbTeams:
    def __init__(self, teams: List[Team]):
        self.teams = teams

class PlayResult:
    def __init__(self, description: str):
        self.description = description

class Play:
    def __init__(self, result: PlayResult):
        self.result = result


class PlayByPlay:
    def __init__(self, all_plays: List[Play]):
        self.all_plays = all_plays

