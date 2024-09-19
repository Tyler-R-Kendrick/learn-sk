#pragma warning disable SKEXP0110
using Core.Utilities.Config;
using Core.Utilities.Services;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Solution7;

var ticketAgentKernel = KernelBuilderProvider.CreateKernelWithChatCompletion().Build();
var validationAgentKernel = KernelBuilderProvider.CreateKernelWithChatCompletion().Build();


var httpClient = new HttpClient() { BaseAddress = new Uri("http://statsapi.mlb.com/api/v1/") };
var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

TicketAgent ticketAgent = new(new MlbService(httpClient))
{
    Name = "TicketPurchasing",
    Instructions = 
        """
        You are a ticket agent focused on buy baseball tickets for a customer. 
        You can get the teams schedule from the scheduling tool. 
        Your goal is to review the schedule and select a single game only from the list.
        If asked to pick a new game select the next game available
        """,
    Description = "Ticket purchesing agent",
    Kernel = ticketAgentKernel,
    Arguments = new KernelArguments(openAIPromptExecutionSettings)
};

ValidationAgent validationAgent = new()
{
    Name = "ScheduleValidation",
    Instructions = 
        """
        You are an assistant for a customer. 
        You are responsible for approving the ticket purchase. 
        Check the customers schedule to ensure they can attend the baseball game on that date. 
        You can get the customers schedule from the schedule tool. 
        If the customer can attend the game respond back with you approve the purchase. 
        If the customer can not attend responde back with the customer is busy select a new game.
        """,
    Description = "Validate the customer schedule is open for that game.",
    Kernel = validationAgentKernel,
    Arguments = new KernelArguments(openAIPromptExecutionSettings)
};

string? userInput;
const string terminationPhrase = "quit";

do
{
    ChatHistory chatHistory = new();

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
        chatHistory.AddUserMessage(userInput);

        chat.AddChatMessages(chatHistory);

        await foreach (ChatMessageContent response in chat.InvokeAsync())
        {
            Console.WriteLine(response.Content);
        }
    }
}
while (userInput != terminationPhrase);