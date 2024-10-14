using Core.Utilities.Config;
using Microsoft.SemanticKernel;

// Load configuration.
var builder = KernelBuilderProvider.CreateKernelWithChatCompletion();
var kernel = builder.Build();

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
        await foreach (var response in kernel.InvokePromptStreamingAsync(userInput))
        {
            Console.Write(response);
        }
        Console.WriteLine();
    }
}
while (!terminationPhrases.Contains(userInput));
