using Core.Utilities.Config;
using Core.Utilities.Services;
using Core.Utilities.Agents;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Solution7;

var ticketAgentKernel = KernelBuilderProvider.CreateKernelWithChatCompletion().Build();
var validationAgentKernel = KernelBuilderProvider.CreateKernelWithChatCompletion().Build();

HttpClient httpClient = new() { BaseAddress = new("http://statsapi.mlb.com/api/v1/") };
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};
MlbService mlbService = new(httpClient);
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
    Arguments = new(openAIPromptExecutionSettings)
};

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
    Arguments = new(openAIPromptExecutionSettings)
};

string? userInput;
const string terminationPhrase = "quit";

do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    if (userInput != null && userInput != terminationPhrase)
    {
        AgentGroupChat chat = new(ticketAgent, validationAgent)
        {
            ExecutionSettings = new()
            {
                TerminationStrategy = new ApprovalTerminationStrategy()
                {
                    Agents = [validationAgent],
                    MaximumIterations = 10,
                }
            }
        };

        //Adding the user prompt to chat history
        chat.AddChatMessage(new(AuthorRole.User, userInput));
        await foreach (ChatMessageContent response in chat.InvokeAsync())
        {
            Console.WriteLine(response.Content);
        }
    }
}
while (userInput != terminationPhrase);
