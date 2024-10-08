#from http import HttpClient
import asyncio 
import requests
import json
from typing import List
from datetime import datetime
from core.utilities.models.core_models import Schedule, MlbTeams, Team, Game, GameDate, PlayByPlay, Play, PlayResult

class MlbService:
    def __init__(self):
        self.base_url = "http://statsapi.mlb.com/api/v1/"
        
    async def get_team_schedule(self, team_id: int, start_date: datetime, end_date: datetime) -> Schedule:
        start_date_formatted = start_date.strftime("%Y-%m-%d")
        end_date_formatted = end_date.strftime("%Y-%m-%d")
        request_uri = f"schedule?sportId=1&sportId=51&sportId=21&startDate={start_date_formatted}&endDate={end_date_formatted}&teamId={team_id}&timeZone=America/New_York&gameType=E&&gameType=S&&gameType=R&&gameType=F&&gameType=D&&gameType=L&&gameType=W&&gameType=A&&gameType=C&language=en&leagueId=104&&leagueId=103&&leagueId=160&&leagueId=590&hydrate=team,linescore(matchup,runners),xrefId,story,flags,statusFlags,broadcasts(all),venue(location),decisions,person,probablePitcher,stats,game(content(media(epg),summary),tickets),seriesStatus(useOverride=true)&sortBy=gameDate,gameStatus,gameType"

        full_url = self.base_url + request_uri
        response = requests.get(full_url)
        schedule_json = json.loads(response.content)
        
        #with open('schedule.json', 'w') as f:
        #    json.dump(schedule_json, f, indent=4)
        game_dates = []
        for date in schedule_json['dates']:
            games = []
            for game in date['games']:
                new_game = Game(game_pk=game['gamePk'], game_guid=game['gameGuid'], game_date=game['gameDate'], teams=game['teams'])
                games.append(new_game)
            new_game_date = GameDate(games=games)
            game_dates.append(new_game_date)

        return Schedule(game_dates)

    async def get_teams(self) -> MlbTeams:
        request_uri = "teams?sportId=1"
        full_url = self.base_url + request_uri
        response = requests.get(full_url)
        mlb_teams_json = json.loads(response.content)
        teams = []
        for team in mlb_teams_json['teams']:
            new_team = Team(id=team['id'], name=team['name'], team_code=team['teamCode'], abbreviation=team['abbreviation'], team_name=team['teamName'])
            #print (f"{team['id']} - {team['name']} - {team['teamCode']} - {team['abbreviation']} - {team['teamName']}")
            teams.append(new_team)

        #with open('mlb_teams.json', 'w') as f:
        #   json.dump(mlb_teams_json, f, indent=4)

        return MlbTeams(teams)
    
    async def get_game_play_by_play(self, game_id: int, max_play_by_play: int) -> List[Play]:
        request_uri = f"game/{game_id}/playByPlay"
        full_url = self.base_url + request_uri
        response = requests.get(full_url)
        play_by_play_json = json.loads(response.content)

        #with open('play_by_play.json', 'w') as f:
        #    json.dump(play_by_play_json, f, indent=4)

        plays = []
        for play in play_by_play_json['allPlays']:
            new_play = Play(result=PlayResult(description=play['result']['description']))
            print (f"{play['result']['description']}")
            plays.append(new_play)

        return plays

        
