#pragma warning disable SKEXP0110
using Core.Utilities.Config;
using Core.Utilities.Services;
using Core.Utilities.Agents;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = KernelBuilderProvider.CreateKernelWithChatCompletion();
var kernel = builder.Build();
HttpClient httpClient = new();
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
        You can get the team's schedule from the scheduling tool. 
        Your goal is to review the schedule and select a game from the list.
        If the user wants to purchase the ticket let them know they are all set and have a ticket for the game.
        """,
    Description = "Ticket purchasing agent",
    Kernel = kernel,
    Arguments = new(openAIPromptExecutionSettings)
};

const string terminationPhrase = "quit";
string? userInput;
ChatHistory chatHistory = [];

do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    if (userInput != null && userInput != terminationPhrase)
    {
        //Adding the user prompt to chat history
        chatHistory.AddUserMessage(userInput);

        await foreach (var response in ticketAgent.InvokeAsync(chatHistory))
        {
            Console.WriteLine(response.Content);
        }
    }
}
while (userInput != terminationPhrase);
