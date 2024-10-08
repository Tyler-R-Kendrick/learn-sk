using Microsoft.SemanticKernel;

namespace Core.Utilities.Filters;
using Services;

public sealed class CensoredPromptRenderFilter(CensorService censor)
    : IPromptRenderFilter
{
    public async Task OnPromptRenderAsync(
        PromptRenderContext context,
        Func<PromptRenderContext, Task> next)
    {
        await next(context);

        var renderedPrompt = context.RenderedPrompt ?? "";

        try
        {
            censor.Reject(renderedPrompt);
        }
        catch
        {
            context.Result = new(context.Function,
                "I'm sorry, you've mentioned a topic that's very sensitive to Cubs fans. I cannot continue this conversation.");
        }
    }
}
