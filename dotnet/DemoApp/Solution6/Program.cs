#pragma warning disable SKEXP0110
using Core.Utilities.Config;
using Core.Utilities.Services;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Solution6;

IKernelBuilder builder = KernelBuilderProvider.CreateKernelWithChatCompletion();
Kernel kernel = builder.Build();
HttpClient httpClient = new();
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
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
        Your goal is to review the schedule and select a game from the list.
        If the user wants to purchase the ticket let them know they are all set and have a ticket for the game.
        """,
    Description = "Ticket purchesing agent",
    Kernel = kernel,
    Arguments = new KernelArguments(openAIPromptExecutionSettings)
};

const string terminationPhrase = "quit";
string? userInput;
ChatHistory chatHistory = new();

do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    if (userInput != null && userInput != terminationPhrase)
    {
        //Adding the user prompt to chat history
        chatHistory.AddUserMessage(userInput);

        await foreach (ChatMessageContent response in ticketAgent.InvokeAsync(chatHistory))
        {
            Console.WriteLine(response.Content);
        }
    }
}
while (userInput != terminationPhrase);