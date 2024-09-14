using Core.Utilities;
using Core.Utilities.Models;
using Core.Utilities.Services;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Solution4;

public class Program : BaseProgram
{
    static async Task Main(string[] args)
    {
        ApplicationSettings applicationSettings = GetApplicationSettings();
        IKernelBuilder kernelBuilder = CreateKernelWithChatCompletion(applicationSettings);
        HttpClient httpClient = new () { BaseAddress = new Uri("http://statsapi.mlb.com/api/v1/") };
        MlbBaseballPlugin mlbBaseballPlugin = new (new MlbService(httpClient));
        OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };

        kernelBuilder.Plugins.AddFromObject(mlbBaseballPlugin);
        Kernel kernel = kernelBuilder.Build();

        Console.WriteLine(await kernel.InvokePromptAsync("Whats the schedule for the Chicago Cubs.", new(settings)));
        Console.ReadLine();
    }
}