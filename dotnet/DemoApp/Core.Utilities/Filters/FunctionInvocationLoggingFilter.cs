using Microsoft.SemanticKernel;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Core.Utilities.Filters;

public sealed class FunctionInvocationLoggingFilter(ILogger logger)
    : IFunctionInvocationFilter
{
    public async Task OnFunctionInvocationAsync(
        FunctionInvocationContext context,
        Func<FunctionInvocationContext, Task> next)
    {
        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("function invoking: {Name}", context.Function.Name);
            logger.LogTrace("arguments: {Arguments}", JsonSerializer.Serialize(context.Arguments));
        }

        await next(context);

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("function invoked: {Name}", context.Function.Name);
            logger.LogTrace("result: {Result}", context.Result);
        }
    }
}
