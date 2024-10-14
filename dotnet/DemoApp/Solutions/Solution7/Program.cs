using Core.Utilities.Config;
using Core.Utilities.Services;
using Core.Utilities.Agents;
using Core.Utilities.Plugins;
using Core.Utilities.Filters;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Solution7;

// Create separate kernels for each agent.
var ticketAgentKernel = KernelBuilderProvider.CreateKernelWithChatCompletion().Build();
var validationAgentKernel = KernelBuilderProvider.CreateKernelWithChatCompletion().Build();

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
        You can get the teams schedule from the scheduling tool.
        Your goal is to review the schedule and select a single game only from the list.
        If asked to pick a new game select the next game available.
        """,
    Description = "Ticket purchasing agent",
    Kernel = ticketAgentKernel,
    Arguments = new(settings)
};
// Create a separate agent to communicate with.
ValidationAgent validationAgent = new()
{
    Name = "ScheduleValidation",
    Instructions =
        """
        You are an assistant for an executive.
        You are responsible for approving the ticket purchase.
        Check the executive's schedule to ensure they can attend the baseball game on that date.
        You can get the schedule's schedule from the schedule tool.
        If the executive can attend the game, respond back with you approve the purchase.
        If the executive can not attend, respond back with the executive is busy select a new game.
        """,
    Description = "Validate the executive's schedule is open for that game.",
    Kernel = validationAgentKernel,
    Arguments = new(settings)
};
// Create a group chat with the agents - instead of a chat completion service.
AgentGroupChat chat = new(ticketAgent, validationAgent)
{
    ExecutionSettings = new()
    {
        // Use a custom termination strategy for exit criteria instead of termination phrases for the agents.
        TerminationStrategy = new ApprovalTerminationStrategy
        {
            Agents = [validationAgent],
            MaximumIterations = 10,
        }
    }
};

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

    // Process assist responses.
    if (!terminationPhrases.Contains(userInput))
    {
        Console.Write("Assistant > ");

        string fullMessage = "";
        // Invoke the agent instead of the chat completion service.
        await foreach (var response in chat.InvokeStreamingAsync())
        {
            fullMessage += response.Content ?? "";
            Console.Write(response.Content);
        }
        Console.WriteLine();
    }
}
while (!terminationPhrases.Contains(userInput));
