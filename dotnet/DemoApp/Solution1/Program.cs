using Core.Utilities.Config;
using Microsoft.SemanticKernel;

IKernelBuilder builder = KernelBuilderProvider.CreateKernelWithChatCompletion();
Kernel kernel = builder.Build();

// Execute program.
const string terminationPhrase = "quit";
string? userInput;
do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    if (userInput != null && userInput != terminationPhrase)
    {
        Console.Write("Assistant > ");
        await foreach (var response in kernel.InvokePromptStreamingAsync(userInput))
        {
            Console.Write(response);
        }
        Console.WriteLine();
    }
}
while (userInput != terminationPhrase);
