using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using System.Runtime.CompilerServices;
using Microsoft.SemanticKernel.Agents;

namespace Core.Utilities.Agents
{
    public abstract class BaseAgent : ChatHistoryKernelAgent
    {
        private bool pluginsRegistered = false;

        public async override IAsyncEnumerable<ChatMessageContent> InvokeAsync(
            ChatHistory history,
            KernelArguments? arguments = null,
            Kernel? kernel = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default
        )
        {
            List<StreamingChatMessageContent> contents = [];

            await foreach (var result in InvokeStreamingAsync(history, arguments, kernel, cancellationToken))
                contents.Add(result);

            yield return new ChatMessageContent(AuthorRole.Assistant,
                contents.Aggregate("", (init, next) => init + next.ToString()));
        }

        public async override IAsyncEnumerable<StreamingChatMessageContent> InvokeStreamingAsync(
            ChatHistory history,
            KernelArguments? arguments = null,
            Kernel? kernel = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default
        )
        {
            kernel ??= Kernel;
            arguments ??= Arguments;

            ChatMessageContent[] chat = [GetInstructionMessage(), .. history];
            var prompt = string.Join(Environment.NewLine, chat.Select(x => x.ToString()));

            if (!pluginsRegistered)
            {
                kernel.Plugins.AddFromObject(this);
                pluginsRegistered = true;
            }

            await foreach (var result in kernel.InvokePromptStreamingAsync(prompt, arguments, cancellationToken: cancellationToken))
                yield return new StreamingChatMessageContent(AuthorRole.Assistant, result.ToString());
        }

        private ChatMessageContent GetInstructionMessage() => new(AuthorRole.System, Instructions)
        {
            AuthorName = Name,
            Source = this
        };
    }
}
