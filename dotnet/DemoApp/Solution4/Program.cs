using Core.Utilities;
using Core.Utilities.Config;
using Core.Utilities.Models;
using Core.Utilities.Services;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Solution4;

public class Program : BaseProgram
{
    static async Task Main(string[] args)
    {
        AISettings applicationSettings = AISettingsProvider.GetSettings();
        IKernelBuilder kernelBuilder = CreateKernelWithChatCompletion(applicationSettings);
        HttpClient httpClient = new ();
        MlbBaseballData mlbBaseballPlugin = new(new MlbService(httpClient));
        OpenAIPromptExecutionSettings settings = new() 
        { 
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
            ChatSystemPrompt = "You are a sports announcer. Summarize the game play-by-play as if you were the famous Cubs announcer Harry Caray."
        };

        kernelBuilder.Plugins.AddFromObject(mlbBaseballPlugin);
        Kernel kernel = kernelBuilder.Build();

        Console.WriteLine(await kernel.InvokePromptAsync("What happened in the last Chicago Cubs game.", new(settings)));
        Console.ReadLine();
    }
}