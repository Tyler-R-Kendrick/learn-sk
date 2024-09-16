using Core.Utilities.Config;
using Core.Utilities.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

AISettings applicationSettings = AISettingsProvider.GetSettings();

var builder = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(
        deploymentName: applicationSettings.OpenAI.ModelName, 
        endpoint: applicationSettings.OpenAI.Endpoint, 
        apiKey: applicationSettings.OpenAI.Key);

var kernel = builder.Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

string? userInput;
do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    if (userInput != null && userInput != "quit")
    {
        Console.Write("Assistant> ");
        await foreach (var response in chatCompletionService.GetStreamingChatMessageContentsAsync(userInput))
        {
            Console.Write(response);
        }
        Console.WriteLine();
    }
}
while (userInput != "quit");
