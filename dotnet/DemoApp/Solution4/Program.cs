﻿using Core.Utilities.Config;
using Core.Utilities.Services;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Solution4;

IKernelBuilder builder = KernelBuilderProvider.CreateKernelWithChatCompletion();
Kernel kernel = builder.Build();
HttpClient httpClient = new();
MlbBaseballData mlbBaseballPlugin = new(new MlbService(httpClient));

const string terminationPhrase = "quit";
string? userInput;

//Register the plugin
kernel.Plugins.AddFromObject(mlbBaseballPlugin);

do
{
    Console.Write("User > ");
    userInput = Console.ReadLine();

    OpenAIPromptExecutionSettings settings = new()
    {
        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
        ChatSystemPrompt = "You are a sports announcer. Summarize the game play-by-play as if you were the famous Cubs announcer Harry Caray."
    };

    KernelArguments kernelArgs = new(settings);

    if (userInput != null && userInput != terminationPhrase)
    {
        Console.Write("Assistant > ");
        await foreach (var response in kernel.InvokePromptStreamingAsync(userInput, kernelArgs))
        {
            Console.Write(response);
        }
        Console.WriteLine();
    }
}
while (userInput != terminationPhrase);