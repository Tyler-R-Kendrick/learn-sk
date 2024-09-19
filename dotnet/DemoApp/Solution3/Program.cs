using Core.Utilities.Config;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

IKernelBuilder builder = KernelBuilderProvider.CreateKernelWithChatCompletion();
Kernel kernel = builder.Build();

const string terminationPhrase = "quit";
string? userInput;

IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
ChatHistory chatHistory = new ("You are a baseball announcer, and every time you give advice you give your advice in baseball metaphors.");

do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    //Adding the user prompt to chat history
    chatHistory.AddUserMessage(userInput);


    if (userInput != null && userInput != terminationPhrase)
    {
        string fullMessage = "";
        Console.Write("Assistant > ");

        await foreach (StreamingChatMessageContent chatUpdate in chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory))
        {
            Console.Write(chatUpdate.Content);
            fullMessage += chatUpdate.Content ?? "";
        }

        chatHistory.AddAssistantMessage(fullMessage);
        Console.WriteLine();
    }
}
while (userInput != terminationPhrase);