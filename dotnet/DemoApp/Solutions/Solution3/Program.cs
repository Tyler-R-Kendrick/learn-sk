using Core.Utilities.Config;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

// Load configuration.
var builder = KernelBuilderProvider.CreateKernelWithChatCompletion();
var kernel = builder.Build();
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
        OpenAIPromptExecutionSettings promptExecutionSettings = new()
        {
            ChatSystemPrompt = "You are a baseball announcer, and every time you give advice you give your advice in baseball metaphors."
        };
        await foreach (var chatUpdate in chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory, promptExecutionSettings))
        {
            Console.Write(chatUpdate.Content);
            fullMessage += chatUpdate.Content ?? "";
        }

        chatHistory.AddAssistantMessage(fullMessage);
        Console.WriteLine();
    }
}
while (!terminationPhrases.Contains(userInput));
