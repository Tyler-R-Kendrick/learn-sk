using Microsoft.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.Filters;
using Services;

public static class KernelConsumerDependencyInjectionExtensions
{
    public static IServiceCollection AddCustomFilters(
        this IServiceCollection builder,
        Func<IServiceProvider, CensorService> censorFactory) => builder
            .AddSingleton<FunctionInvocationLoggingFilter>()
            .AddSingleton<FunctionInvocationRetryFilter>()
            .AddSingleton(censorFactory)
            .AddSingleton<CensoredPromptRenderFilter>();
    public static IKernelBuilder AddCustomFilters(
        this IKernelBuilder builder,
        Func<IServiceProvider, CensorService> censorFactory)
    {
        builder.Services.AddCustomFilters(censorFactory);
        return builder;
    }

    public static IServiceCollection AddKernelConsumer(
        this IServiceCollection services,
        params string[] bannedPhrases)
        => services.AddSingleton<CustomKernelConsumer>(provider
            => new(Kernel
                .CreateBuilder()
                .AddCustomFilters(_ => new(bannedPhrases))
                .Build()));
}
