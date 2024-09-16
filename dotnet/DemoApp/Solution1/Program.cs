using Core.Utilities.Config;
using Core.Utilities.Models;
using Microsoft.SemanticKernel;

var builder = KernelBuilderProvider.CreateKernelWithChatCompletion();
var kernel = builder.Build();

string? userInput;
do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    if (userInput != null && userInput != "quit")
    {
        Console.Write("Assistant> ");
        await foreach (var response in kernel.InvokePromptStreamingAsync(userInput))
        {
            Console.Write(response);
        }
        Console.WriteLine();
    }
}
while (userInput != "quit");
