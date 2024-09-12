using Microsoft.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace Filters;

internal static class KernelConsumerDependencyInjectionExtensions
{
    private static IKernelBuilder AddKernelConsumer(
        this IKernelBuilder builder,
        Func<IServiceProvider, CensorService> censorFactory)
    {
        builder.Services
            .AddSingleton<FunctionInvocationLoggingFilter>()
            .AddSingleton<FunctionInvocationRetryFilter>()
            .AddSingleton(censorFactory)
            .AddSingleton<CensoredPromptRenderFilter>();
        return builder;
    }

    internal static IServiceCollection AddKernelConsumer(
        this IServiceCollection services,
        params string[] bannedPhrases)
        => services.AddSingleton<CustomKernelConsumer>(provider
            => new(Kernel
                .CreateBuilder()
                .AddKernelConsumer(_ => new(bannedPhrases))
                .Build()));
}
