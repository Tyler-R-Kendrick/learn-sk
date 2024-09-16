using Core.Utilities.Config;
using Core.Utilities.Models;
using Microsoft.SemanticKernel;

AISettings applicationSettings = AISettingsProvider.GetSettings();

var builder = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(
        deploymentName: applicationSettings.OpenAI.ModelName, 
        endpoint: applicationSettings.OpenAI.Endpoint, 
        apiKey: applicationSettings.OpenAI.Key);

var kernel = builder.Build();

string? userInput;
do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    if (userInput != null && userInput != "quit")
    {
        Console.Write("Assistant> ");
        await foreach (var response in kernel.InvokePromptStreamingAsync(userInput))
        {
            Console.Write(response);
        }
        Console.WriteLine();
    }
}
while (userInput != "quit");
