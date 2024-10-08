from typing import Annotated
from datetime import datetime, timedelta
from semantic_kernel import Kernel
from semantic_kernel.functions import kernel_function
#from semantic_kernel.functions.kernel_function_decorator import kernel_function
from core.utilities.services.mlb_service import MlbService
from core.utilities.formatters import ScheduleFormatter, TeamFormatter, GameFormatter, PlayByPlayFormatter

class MlbBaseballData:
    def __init__(self, mlb_service: MlbService):
        self._mlb_service = mlb_service

    @kernel_function(
        description="Gets a list of games for a specific baseball team."
    )
    async def get_team_schedule_data(self, team_id: int) -> str:
        start_date = datetime.now() - timedelta(days=1)
        end_date = datetime.now() + timedelta(days=14)
        schedule = await self._mlb_service.get_team_schedule(team_id, start_date, end_date)
        return ScheduleFormatter.format(schedule)

    @kernel_function(
        description="Gets a list of teams and their Ids."
    )
    async def get_team_ids_data(self) -> str:
        teams = await self._mlb_service.get_teams()
        return TeamFormatter.format(teams)
    
    @kernel_function(
        description="Gets the teams last played game id."
    )
    async def get_teams_last_played_game_id(self, team_id: int) -> str:
        start_date = datetime.now()- timedelta(days=40)
        end_date = datetime.now()- timedelta(days=1)
        response = await self._mlb_service.get_team_schedule(team_id, start_date, end_date)
        date = response.game_dates[-1]
        game = date.games[-1]
        return GameFormatter.format(game)   

    @kernel_function(
        description="Gets the play by play for a specific game."
    )
    async def get_game_play_by_play(self, game_id: int) -> str:
        play_by_play = await self._mlb_service.get_game_play_by_play(game_id, 40)
        return PlayByPlayFormatter.format(play_by_play)
