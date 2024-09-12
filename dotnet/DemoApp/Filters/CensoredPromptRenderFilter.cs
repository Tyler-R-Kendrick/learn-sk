using Microsoft.SemanticKernel;

namespace Filters;

internal sealed class CensoredPromptRenderFilter(CensorService censor)
    : IPromptRenderFilter
{
    public async Task OnPromptRenderAsync(
        PromptRenderContext context,
        Func<PromptRenderContext, Task> next)
    {
        await next(context);

        var renderedPrompt = context.RenderedPrompt ?? "";
        context.RenderedPrompt = censor.Transform(renderedPrompt);
    }
}
