#pragma warning disable SKEXP0110
using Core.Utilities.Config;
using Core.Utilities.Services;
using Core.Utilities.Plugins;
using Core.Utilities.Filters;
using Core.Utilities.Agents;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
// The execution settings are moved from the chat service, to the agent.
OpenAIPromptExecutionSettings settings = new()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};
// Create the agent.
TicketAgent ticketAgent = new(mlbService)
{
    Name = "TicketPurchasing",
    Instructions =
        """
        You are a ticket agent focused on buying baseball tickets for a customer.
        You can get the team's schedule from the scheduling tool.
        Your goal is to review the schedule and select a game from the list.
        If the user wants to purchase the ticket let them know they are all set and have a ticket for the game.
        """,
    Description = "Ticket purchasing agent",
    Kernel = kernel,
    Arguments = new(settings)
};

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

        // Create instructions for the agent to adhere to.
        KernelArguments arguments = new(settings);
        // Invoke the agent instead of the chat completion service.
        await foreach (var chatUpdate in ticketAgent.InvokeStreamingAsync(chatHistory, arguments))
        {
            Console.Write(chatUpdate.Content);
            fullMessage += chatUpdate.Content ?? "";
        }

        chatHistory.AddAssistantMessage(fullMessage);
        Console.WriteLine();
    }
}
while (!terminationPhrases.Contains(userInput));
