﻿#pragma warning disable SKEXP0110
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Solution6;
using Core.Utilities;
using Core.Utilities.Services;
using Core.Utilities.Config;

public class Program : BaseProgram
{
    static async Task Main(string[] args)
    {
        var ticketAgentKernel = KernelBuilderProvider.CreateKernelWithChatCompletion();
        var validationAgentKernel = KernelBuilderProvider.CreateKernelWithChatCompletion();

        var httpClient = new HttpClient() { BaseAddress = new Uri("http://statsapi.mlb.com/api/v1/") };
        var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        var ticketAgent = new TicketAgent(new MlbService(httpClient))
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
            Kernel = ticketAgentKernel.Build(),
            Arguments = new KernelArguments(openAIPromptExecutionSettings)
        };

        ChatHistory chatMessageContents = new ChatHistory();
        chatMessageContents.AddUserMessage("Are there any games coming up for the Chicago Cubs game.");

        await foreach (ChatMessageContent response in ticketAgent.InvokeAsync(chatMessageContents))
        {
            Console.WriteLine(response.Content);
        }

        var userResponse = Console.ReadLine();

        if (userResponse != null && userResponse.ToUpper() != "EXIT")
        {
            chatMessageContents.AddUserMessage(userResponse);

            await foreach (ChatMessageContent response in ticketAgent.InvokeAsync(chatMessageContents))
            {
                Console.WriteLine(response.Content);
            }
        }

        Console.WriteLine("DONE");
        Console.ReadLine();
    }
}