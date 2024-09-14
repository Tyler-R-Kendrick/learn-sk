#pragma warning disable SKEXP0110
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Solution7;
using System;
using System.Reflection;
using System.Text.Json;

public class Program
{
    static async Task Main(string[] args)
    {
        var applicationSettings = GetApplicationSettings();
        var ticketAgentKernel = CreateKernelWithChatCompletion(applicationSettings);
        var validationAgentKernel = CreateKernelWithChatCompletion(applicationSettings);
        var httpClient = new HttpClient() { BaseAddress = new Uri("http://statsapi.mlb.com/api/v1/") };
        var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        var ticketAgent = new TicketAgent(httpClient)
        {
            Name = "TicketPurchasing",
            Instructions = 
                """
                You are a ticket agent focused on buy baseball tickets for a customer. 
                You can get the teams schedule from the scheduling tool. 
                Your goal is to review the schedule and select a game from the list.
                """,
            Description = "Ticket purchesing agent",
            Kernel = ticketAgentKernel,
            Arguments = new KernelArguments(openAIPromptExecutionSettings)
        };

        var validationAgent = new ValidationAgent()
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
        
        var chat = new AgentGroupChat(ticketAgent, validationAgent)
        {
            ExecutionSettings = new()
            {
                TerminationStrategy =
                    new ApprovalTerminationStrategy()
                    {
                        Agents = [validationAgent],
                        MaximumIterations = 10,
                    }
            }
        };

        ChatMessageContent input = new(AuthorRole.User, "Please buy me a ticket for the next Chicago Cubs game.");

        chat.AddChatMessage(input);

        await foreach (ChatMessageContent response in chat.InvokeAsync())
        {
            Console.WriteLine(response.Content);
        }

        Console.WriteLine("DONE");
        Console.ReadLine();
    }

    static Kernel CreateKernelWithChatCompletion(ApplicationSettings applicationSettings)
    {

        var builder = Kernel.CreateBuilder();

        builder.AddAzureOpenAIChatCompletion(
            applicationSettings.OpenAI.ModelName,
            applicationSettings.OpenAI.Endpoint,
            applicationSettings.OpenAI.Key);

        return builder.Build();
    }

    static ApplicationSettings GetApplicationSettings()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .Build();

        return config.GetSection("ApplicationSettings").Get<ApplicationSettings>();
    }
}