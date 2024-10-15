using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.ChatCompletion;

var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.Services.AddSingleton<IChatCompletionService, ChatCompletionServiceProxy>();
var kernel = kernelBuilder.Build();

var semanticFunction = KernelFunctionFactory.CreateFromPrompt("respond with 'hello semantic world'");
var nativeFunction = KernelFunctionFactory.CreateFromMethod(() => "hello native world");
var plugin = KernelPluginFactory.CreateFromFunctions("hello-world", [
    semanticFunction,
    nativeFunction
]);

ChatCompletionAgent agent = new()
{
    Arguments = [],
    //HistoryReducer = new ChatHistorySummarizationReducer(),
    Instructions = "",
    Name = "hello-world",
    Description = "A simple hello world agent",
    Id = Guid.NewGuid().ToString(),
    Kernel = kernel,
};

file class ChatCompletionServiceProxy(
    IReadOnlyDictionary<string, object?>? attributes = null,
    IReadOnlyList<ChatMessageContent>? chatMessageContents = null,
    IAsyncEnumerable<StreamingChatMessageContent>? streamingChatMessageContents = null
) : IChatCompletionService
{
    public IReadOnlyDictionary<string, object?> Attributes => attributes ?? new Dictionary<string, object?>();

    public Task<IReadOnlyList<ChatMessageContent>> GetChatMessageContentsAsync(
        ChatHistory chatHistory,
        PromptExecutionSettings? executionSettings = null,
        Kernel? kernel = null,
        CancellationToken cancellationToken = default)
        => Task.FromResult(chatMessageContents ?? []);

    public IAsyncEnumerable<StreamingChatMessageContent> GetStreamingChatMessageContentsAsync(
        ChatHistory chatHistory,
        PromptExecutionSettings? executionSettings = null,
        Kernel? kernel = null,
        CancellationToken cancellationToken = default)
        => streamingChatMessageContents
            ?? Array.Empty<StreamingChatMessageContent>().ToAsyncEnumerable();
}
