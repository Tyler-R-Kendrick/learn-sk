import asyncio 
import os
import sys
import logging
from datetime import datetime

### Pull in core utility libraries
sys.path.append(os.path.join(os.path.dirname(__file__), '..'))  #allows us to pull in core packages by adding the parent directory to the path
from core.utilities.config.aiprovider import AIProvider
from core.utilities.config.loggingprovider import LoggingProvider
from core.utilities.filters.functioninvocationloggingfilter import FunctionInvocationLoggingFilter
from core.utilities.filters.functioninvocationretryfilter import FunctionInvocationRetryFilter
from core.utilities.services.mlb_service import MlbService
from core.utilities.plugins.mlb_basebase_data import MlbBaseballData

from semantic_kernel.contents.chat_history import ChatHistory
from semantic_kernel.functions import KernelArguments
from semantic_kernel.connectors.ai.function_choice_behavior import FunctionChoiceBehavior
from semantic_kernel.connectors.ai.open_ai.prompt_execution_settings.azure_chat_prompt_execution_settings import AzureChatPromptExecutionSettings
from semantic_kernel.filters.filter_types import FilterTypes



async def main():
    kernel = AIProvider.GetKernel()

    kernel.add_plugin(plugin=MlbBaseballData(MlbService()), 
                      plugin_name="MlbBaseballData")
    
    logger = LoggingProvider.get_logger('learn-sk-python', logging.DEBUG)
    kernel.add_filter(FilterTypes.FUNCTION_INVOCATION, 
                      FunctionInvocationLoggingFilter(logger).OnFunctionInovationAsync)
    kernel.add_filter(FilterTypes.FUNCTION_INVOCATION, 
                      FunctionInvocationRetryFilter(max_retries=3).OnFunctionInovationAsync)

    execution_settings = AzureChatPromptExecutionSettings(tool_choice="auto")
    execution_settings.function_choice_behavior = FunctionChoiceBehavior.Auto(auto_invoke=True)

    arguments = KernelArguments(settings=execution_settings)

    #**** Testing the MlbService****
    #mlb_service = MlbService()
    #teams = await mlb_service.get_teams()
    #schedule = await mlb_service.get_team_schedule(112, datetime(2024, 9, 1), datetime(2024, 9, 30))
    #playbyplay = await mlb_service.get_game_play_by_play(744806, 40)
    
    system_message ='''You are a sports announcer.
        Summarize the game play-by-play as if you were the famous Cubs announcer Harry Caray.
        When describing win or loss conditions, mention the lore/superstition that may be associated 
        with the result."
        '''

    termination_phrases = ["quit", "exit"]
    user_input = None

    chat_history = ChatHistory()
    chat_history.add_system_message(system_message)

    while user_input not in termination_phrases:
        user_input = input ("User > ")

        if (user_input != None) and (user_input.lower() not in termination_phrases):
            print("Assistant > ")

            chat_history.add_user_message(user_input)

            #**** non-stream ****
            #assistant_response = await kernel.invoke_prompt(prompt=chat_history.to_prompt())
            #print (assistant_response)
           
            #**** stream ****
            assistant_response = ""
            response = kernel.invoke_prompt_stream(prompt=chat_history.to_prompt(), arguments=arguments)
            async for chunk in response:
                print(chunk[0].content, end="")
                assistant_response += chunk[0].content

            print()

            chat_history.add_assistant_message(assistant_response)


# Run the main function
if __name__ == "__main__":
    asyncio.run(main())