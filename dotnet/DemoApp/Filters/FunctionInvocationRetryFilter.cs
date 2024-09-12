using Microsoft.SemanticKernel;

namespace Filters;

internal sealed class FunctionInvocationRetryFilter(ushort times = 3)
    : IFunctionInvocationFilter
{
    public async Task OnFunctionInvocationAsync(
        FunctionInvocationContext context,
        Func<FunctionInvocationContext, Task> next)
    {
        ushort count = 0;
        do
        {
            try
            {
                await next(context);
            }
            catch
            {
            }
        } while (count++ < times);
    }
}
