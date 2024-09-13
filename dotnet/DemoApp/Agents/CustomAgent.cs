﻿using System.Runtime.CompilerServices;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Agents;

#pragma warning disable SKEXP0110
public partial class CustomAgent : ChatHistoryKernelAgent
{
    public async override IAsyncEnumerable<ChatMessageContent> InvokeAsync(
        ChatHistory history,
        KernelArguments? arguments = null,
        Kernel? kernel = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<StreamingChatMessageContent> contents = [];
        await foreach(var result in InvokeStreamingAsync(
            history, arguments, kernel, cancellationToken))
            contents.Add(result);
        yield return new ChatMessageContent(AuthorRole.Assistant,
            contents.Aggregate("", (init, next) => init + next.ToString()));
    }

    public async override IAsyncEnumerable<StreamingChatMessageContent> InvokeStreamingAsync(
        ChatHistory history,
        KernelArguments? arguments = null,
        Kernel? kernel = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        kernel ??= Kernel;
        arguments ??= Arguments;
        ChatMessageContent[] chat = [GetInstructionMessage(), ..history];
        var prompt = string.Join(Environment.NewLine, chat.Select(x => x.ToString()));
        await foreach(var result in kernel.InvokePromptStreamingAsync(
            prompt, arguments, cancellationToken: cancellationToken))
            yield return new StreamingChatMessageContent(AuthorRole.Assistant, result.ToString());
    }

    private ChatMessageContent GetInstructionMessage() => new(AuthorRole.System, Instructions)
    {
#pragma warning disable SKEXP0001
        AuthorName = Name,
#pragma warning restore SKEXP0001
#pragma warning disable SKEXP0101
        Source = this
#pragma warning restore SKEXP0101
    };
    
}

#pragma warning restore SKEXP0110

public partial class CustomAgent
{
    //TODO: Put Kernel Functions here.
}