using Core.Utilities.Config;
using Core.Utilities.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

// Construct program dependencies
AISettings applicationSettings = AISettingsProvider.GetSettings();

var builder = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(
        deploymentName: applicationSettings.OpenAI.ModelName, 
        endpoint: applicationSettings.OpenAI.Endpoint, 
        apiKey: applicationSettings.OpenAI.Key);

var kernel = builder.Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Execute program.
const string terminationPhrase = "quit";
string? userInput;
do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    if (userInput != null && userInput != terminationPhrase)
    {
        Console.Write("Assistant > ");
        await foreach (var response in chatCompletionService.GetStreamingChatMessageContentsAsync(userInput))
        {
            Console.Write(response);
        }
        Console.WriteLine();
    }
}
while (userInput != terminationPhrase);
