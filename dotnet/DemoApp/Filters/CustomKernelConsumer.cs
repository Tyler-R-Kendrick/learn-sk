using Microsoft.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace Filters;

internal class CustomKernelConsumer(
    IKernelBuilder kernelBuilder,
    CensorService censorService)
{
    private readonly Kernel _kernel = RegisterDependencies(kernelBuilder, censorService);
    private static Kernel RegisterDependencies(
        IKernelBuilder builder,
        CensorService censor)
    {
        builder.Services
            .AddSingleton<FunctionInvocationLoggingFilter>()
            .AddSingleton<FunctionInvocationRetryFilter>()
            .AddSingleton(censor)
            .AddSingleton<CensoredPromptRenderFilter>();
            
        return builder.Build();
    }

    public async Task<string> GetResponse(string input, CancellationToken token)
    {
        var result = await _kernel.InvokePromptAsync(input, [], cancellationToken: token);
        return result.ToString();
    }    
}