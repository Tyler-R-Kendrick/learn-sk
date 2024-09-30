using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel;

namespace Solution7;

public class ApprovalTerminationStrategy : TerminationStrategy
{
    protected override Task<bool> ShouldAgentTerminateAsync(
        Agent agent,
        IReadOnlyList<ChatMessageContent> history,
        CancellationToken cancellationToken)
        => Task.FromResult(history[^1].Content?.Contains("APPROVE", StringComparison.OrdinalIgnoreCase) ?? false);
}
