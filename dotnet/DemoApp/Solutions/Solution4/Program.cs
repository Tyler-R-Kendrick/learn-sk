using Core.Utilities.Config;
using Core.Utilities.Services;
using Core.Utilities.Plugins;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = KernelBuilderProvider.CreateKernelWithChatCompletion();
var kernel = builder.Build();
HttpClient httpClient = new();
MlbService mlbService = new(httpClient);
MlbBaseballDataPlugin mlbBaseballPlugin = new(mlbService);

const string terminationPhrase = "quit";
string? userInput;

//Register the plugin
kernel.Plugins.AddFromObject(mlbBaseballPlugin);

do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    OpenAIPromptExecutionSettings settings = new()
    {
        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
        ChatSystemPrompt = "You are a sports announcer. Summarize the game play-by-play as if you were the famous Cubs announcer Harry Caray."
    };

    KernelArguments kernelArgs = new(settings);

    if (userInput != null && userInput != terminationPhrase)
    {
        Console.Write("Assistant > ");
        await foreach (var response in kernel.InvokePromptStreamingAsync(userInput, kernelArgs))
        {
            Console.Write(response);
        }
        Console.WriteLine();
    }
}
while (userInput != terminationPhrase);
