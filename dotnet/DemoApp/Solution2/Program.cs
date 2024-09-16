using Core.Utilities.Config;
using Core.Utilities.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

AISettings applicationSettings = AISettingsProvider.GetSettings();

var builder = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(
        deploymentName: applicationSettings.OpenAI.ModelName,
        endpoint: applicationSettings.OpenAI.Endpoint,
        apiKey: applicationSettings.OpenAI.Key);

var kernel = builder.Build();

string? userInput;
do
{
  Console.Write("User > ");
  userInput = Console.ReadLine();

  var promptExecutionSettings = new OpenAIPromptExecutionSettings
  {
    ChatSystemPrompt = "You are a baseball announcer, and every time you give advice you give your advice in baseball metaphors."
  };

  var kernelArgs = new KernelArguments(promptExecutionSettings);

  if (userInput != null && userInput != "quit")
  {
    Console.Write("Assistant> ");
    await foreach (var response in kernel.InvokePromptStreamingAsync(userInput, kernelArgs))
    {
      Console.Write(response);
    }
    Console.WriteLine();
  }
}
while (userInput != "quit");
