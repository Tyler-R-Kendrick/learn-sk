import asyncio 
import os
import sys

### Pull in core utility libraries
sys.path.append(os.path.join(os.path.dirname(__file__), '..'))  #allows us to pull in core packages by adding the parent directory to the path
from core.utilities.config.aiprovider import AIProvider
from semantic_kernel.contents.chat_history import ChatHistory


async def main():
    kernel = AIProvider.GetKernel()

    system_message = '''You are a baseball announcer, and every time you give advice 
                        you give your advice in baseball metaphors.'''


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
            response = kernel.invoke_prompt_stream(prompt=chat_history.to_prompt())
            async for chunk in response:
                print(chunk[0].content, end="")
                assistant_response += chunk[0].content

            print()

            chat_history.add_assistant_message(assistant_response)


# Run the main function
if __name__ == "__main__":
    asyncio.run(main())