import asyncio 
import os
import sys

### Pull in core utility libraries
sys.path.append(os.path.join(os.path.dirname(__file__), '..'))  #allows us to pull in core packages by adding the parent directory to the path
from core.utilities.config.aiprovider import AIProvider

async def main():
    kernel = AIProvider.GetKernel()

    prompt = '''
            You are a baseball announcer, and every time you give advice you give 
            your advice in baseball metaphors. 
            
            request: {request}'''
    
    termination_phrases = ["quit", "exit"]
    user_input = None

    while user_input not in termination_phrases:
        user_input = input ("User > ")

        if (user_input != None) and (user_input.lower() not in termination_phrases):
            print("Assistant > ", end="")

            #print(prompt.format(request=user_input))

            #**** non-stream ****
            #result = await kernel.invoke_prompt(prompt=prompt.format(request=user_input))
            #print (result)

            #**** stream ****
            response = kernel.invoke_prompt_stream(prompt=prompt.format(request=user_input))
            async for chunk in response:
                print(chunk[0].content, end="")
            print()


# Run the main function
if __name__ == "__main__":
    asyncio.run(main())