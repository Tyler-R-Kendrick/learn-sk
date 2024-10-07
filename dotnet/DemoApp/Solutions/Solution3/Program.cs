using Core.Utilities.Config;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

var builder = KernelBuilderProvider.CreateKernelWithChatCompletion();
var kernel = builder.Build();

const string terminationPhrase = "quit";
string? userInput;

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
ChatHistory chatHistory = new("You are a baseball announcer, and every time you give advice you give your advice in baseball metaphors.");

do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    if (userInput != null && userInput != terminationPhrase)
    {
        string fullMessage = "";
        Console.Write("Assistant > ");

        //Adding the user prompt to chat history
        chatHistory.AddUserMessage(userInput);

        await foreach (var chatUpdate in chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory))
        {
            Console.Write(chatUpdate.Content);
            fullMessage += chatUpdate.Content ?? "";
        }

        chatHistory.AddAssistantMessage(fullMessage);
        Console.WriteLine();
    }
}
while (userInput != terminationPhrase);
