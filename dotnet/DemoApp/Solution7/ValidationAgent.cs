#pragma warning disable SKEXP0110
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0101
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace Solution7
{
    public class ValidationAgent : ChatHistoryKernelAgent
    {
        private bool pluginsRegistered = false;

        public async override IAsyncEnumerable<ChatMessageContent> InvokeAsync(
            ChatHistory history,
            KernelArguments? arguments = null,
            Kernel? kernel = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default
        ) {
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
        ) {
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

        [KernelFunction, Description("Gets the executives schedule.")]
        public async Task<string> GetExecutivesScheduleData()
        {
            var random = new Random();
            var next = random.Next(1, 3);

            if (next == 1)
            {
                return "Schedule is full for this day";
            }

            return "Schedule is open for this day";
        }

        private ChatMessageContent GetInstructionMessage() => new(AuthorRole.System, Instructions)
        {
            AuthorName = Name,
            Source = this
        };
    }
}
