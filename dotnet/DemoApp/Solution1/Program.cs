using Config;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

var config = AzureAIConfigProvider.LoadConfig();

var builder = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(deploymentName: config.DeploymentName, endpoint: config.Endpoint, apiKey: config.Key);

var kernel = builder.Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

string? userInput;
do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    if (userInput != null && userInput != "quit")
    {
        var result = await chatCompletionService.GetChatMessageContentAsync(prompt: userInput);
        Console.WriteLine($"Assistant> {result}");
    }
}
while (userInput != "quit");
