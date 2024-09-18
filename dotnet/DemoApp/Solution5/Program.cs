using Core.Utilities.Config;
using Core.Utilities.Services;
using Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Solution5;

IKernelBuilder kernelBuilder = KernelBuilderProvider.CreateKernelWithChatCompletion();

// Add the MLB Baseball Data Plugin to the kernel.
HttpClient httpClient = new();
MlbBaseballDataPlugin mlbBaseballPlugin = new(new MlbService(httpClient));

kernelBuilder.Plugins.AddFromObject(mlbBaseballPlugin);

// Add a logger to the Kernel's dependency injection provider, so the function filters can use it.
using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => 
  builder
    .AddFilter("FunctionInvocationLoggingFilter", LogLevel.Trace)
    .AddConsole()
  );
ILogger logger = loggerFactory.CreateLogger("FunctionInvocationLoggingFilter");

kernelBuilder.Services.AddSingleton<ILogger>(_ => logger);

// Add the filters to the kernel.
kernelBuilder.Services.AddSingleton<IFunctionInvocationFilter, FunctionInvocationLoggingFilter>();
kernelBuilder.Services.AddSingleton<IFunctionInvocationFilter, FunctionInvocationRetryFilter>();
kernelBuilder.Services.AddSingleton<CensorService>(_ => new("Bartman", "Billy Goat Tavern", "William Sianis", "Sox"));
kernelBuilder.Services.AddSingleton<IPromptRenderFilter, CensoredPromptRenderFilter>();

var kernel = kernelBuilder.Build();

// Execute the app logic with auto function invocation.
var executionSettings = new OpenAIPromptExecutionSettings()
{
  ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
  ChatSystemPrompt = @"
        You are a sports announcer.
        Summarize the game play-by-play as if you were the famous Cubs announcer Harry Caray.
        When describing win or loss conditions, mention the lore/superstition that may be associated with the result."
};

KernelArguments kernelArgs = new(executionSettings);

const string terminationPhrase = "quit";
string? userInput;
do
{
  Console.Write("User > ");
  userInput = Console.ReadLine();

  if (userInput != null && userInput != terminationPhrase)
  {
    Console.Write("Assistant > ");
    var response = await kernel.InvokePromptAsync(userInput, kernelArgs);
    Console.WriteLine(response);
  }
}
while (userInput != terminationPhrase);


