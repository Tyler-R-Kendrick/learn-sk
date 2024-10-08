from core.utilities.models.core_models import Schedule, MlbTeams, Game, Play
from typing import List

class ScheduleFormatter:
    @staticmethod
    def format(schedule: Schedule) -> str:
        output = "| Home Team | Away Team | Date |\n"
        output += "| ----- | ----- | ----- |\n"

        for game_date in schedule.game_dates:
            for game in game_date.games:
                output += f"| {game.teams.home.team_name} | {game.teams.away.team_name} | {game.game_date} |\n"

        return output
    
class TeamFormatter:
    @staticmethod
    def format(teams: MlbTeams) -> str:
        output = "| Team Id | Name |\n"
        output += "| ----- | ----- |\n"

        for team in teams.teams:
            output += f"| {team.id} | {team.name} |\n"

        return output
    
class GameFormatter:
    @staticmethod
    def format(game: Game) -> str:
        output = "| GamePk | GameDate |\n"
        output += "| ----- | ----- |\n"
        output += f"| {game.game_pk} | {game.game_date} |\n"

        return output
    
class PlayByPlayFormatter:
    @staticmethod
    def format(plays: List[Play]) -> str:
        output = "| Play Number | Play Result |\n"
        output += "| ----- | ----- |\n"
        counter = 0

        for play in plays:
            counter += 1
            output += f"| {counter} | {play.result.description} |\n"

        return output

