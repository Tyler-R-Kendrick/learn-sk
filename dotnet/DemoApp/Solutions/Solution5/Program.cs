using Core.Utilities.Config;
using Core.Utilities.Services;
using Core.Utilities.Plugins;
using Core.Utilities.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;

// Load configuration.
var builder = KernelBuilderProvider.CreateKernelWithChatCompletion();
// Add the filters to the kernel.
builder.Services
    .AddSingleton<IFunctionInvocationFilter, FunctionInvocationLoggingFilter>()
    .AddSingleton<CensorService>(_ => new("Bartman", "Billy Goat Tavern", "William Sianis", "Sox"))
    .AddSingleton<IPromptRenderFilter, CensoredPromptRenderFilter>()
    .AddSingleton(_ =>
    {
        // Add a logger to the Kernel's dependency injection provider, so the function filters can use it.
        using var loggerFactory = LoggerFactory.Create(builder =>
        builder
            .AddFilter("FunctionInvocationLoggingFilter", LogLevel.Trace)
            .AddConsole()
        );
        return loggerFactory.CreateLogger("FunctionInvocationLoggingFilter");
    });
var kernel = builder.Build();

// build the mlb service and plugin
HttpClient httpClient = new();
MlbService mlbService = new(httpClient);
MlbBaseballDataPlugin mlbBaseballPlugin = new(mlbService);

//Register the plugin
kernel.Plugins.AddFromObject(mlbBaseballPlugin);

// Uses the OpenAI chat completions API.
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
// Insert a system prompt as instructions into the history.
ChatHistory chatHistory = [];

// Execute program.
string[] terminationPhrases = ["quit", "exit"];
string? userInput;
do
{
    // Get user input.
    Console.WriteLine("Type 'quit' or 'exit' to terminate the program.");
    Console.Write("User > ");
    userInput = Console.ReadLine()
        ?.Trim().ToLowerInvariant();

    // Validate user input.
    while(string.IsNullOrWhiteSpace(userInput))
    {
        Console.WriteLine("Please type in something for the llm to respond to.");
        Console.Write("User > ");
        userInput = Console.ReadLine()
            ?.Trim().ToLowerInvariant();
    }

    //Adding the user prompt to chat history
    chatHistory.AddUserMessage(userInput);

    // Process assist responses.
    if (!terminationPhrases.Contains(userInput))
    {
        Console.Write("Assistant > ");

        string fullMessage = "";
        // Create instructions for the llm to adhere to.

        OpenAIPromptExecutionSettings settings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
            ChatSystemPrompt = @"
                You are a sports announcer.
                Summarize the game play-by-play as if you were the famous Cubs announcer Harry Caray.
                When describing win or loss conditions, mention the lore/superstition that may be associated with the result."
        };
        await foreach (var chatUpdate in chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory, settings))
        {
            Console.Write(chatUpdate.Content);
            fullMessage += chatUpdate.Content ?? "";
        }

        chatHistory.AddAssistantMessage(fullMessage);
        Console.WriteLine();
    }
}
while (!terminationPhrases.Contains(userInput));
