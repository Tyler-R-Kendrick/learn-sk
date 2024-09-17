using Core.Utilities;
using Core.Utilities.Config;
using Core.Utilities.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.DependencyInjection;

namespace Solution5;

public class Program(Kernel kernel) : BaseProgram
{
    public static async Task Main()
    {
        // Configure app dependencies.
        AISettings applicationSettings = AISettingsProvider.GetSettings();
        var serviceProvider = new ServiceCollection()
            .AddProgram(applicationSettings)
            .BuildServiceProvider();

        // Resolve the app service.
        var program = serviceProvider.GetRequiredService<Program>();

        // Execute the app logic with auto function invocation.
        await program.ExecuteAsync(new OpenAIPromptExecutionSettings()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
            ChatSystemPrompt = @"
                You are a sports announcer.
                Summarize the game play-by-play as if you were the famous Cubs announcer Harry Caray.
                When describing win or loss conditions, mention the lore/superstition that may be associated with the result."
        });
    }

    public override async Task ExecuteAsync(PromptExecutionSettings settings)
    {
        var response = await kernel.InvokePromptAsync(
            promptTemplate: "What happened in the last Chicago Cubs game that they lost?",
            arguments: new(settings));
        Console.WriteLine(response);
        Console.ReadLine();
    }
}