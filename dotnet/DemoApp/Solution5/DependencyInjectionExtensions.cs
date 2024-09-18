using Core.Utilities;
using Core.Utilities.Models;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Filters;

namespace Solution5;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddProgram(
        this IServiceCollection services,
        AISettings settings) => AddProgram<Program>(
            services, settings, (provider, kernel) => new(kernel));

    private static IServiceCollection AddProgram<TProgram>(
        this IServiceCollection services,
        AISettings settings,
        Func<IServiceProvider, Kernel, TProgram> implementationFactory)
        where TProgram : BaseProgram
    {
        var serviceKey = typeof(TProgram);
        return services
            .AddTransient(p => p.GetRequiredService<IKernelBuilder>().Build())
            .AddKeyedSingleton(
                serviceKey: serviceKey,
                implementationFactory: (p, serviceKey) =>
                {
                    var builder = BaseProgram.CreateKernelWithChatCompletion(settings);

                    MlbBaseballDataPlugin mlbBaseballPlugin = new(new(new()));
                    builder.Plugins.AddFromObject(mlbBaseballPlugin);
                    builder.Services.AddScoped<MlbCitationFunctionFilter>();
                    builder.Services.AddCustomFilters(_ => new(
                        "Bartman",
                        "Billy Goat Tavern",
                        "William Sianis",
                        "Sox"));
                    return builder;
                })
            .AddKeyedTransient(
                serviceKey: serviceKey,
                implementationFactory: (provider, serviceKey) => provider
                    .GetRequiredKeyedService<IKernelBuilder>(serviceKey)
                    .Build()
            )
            .AddSingleton(provider =>
            {
                var kernel = provider.GetRequiredKeyedService<Kernel>(serviceKey);
                return implementationFactory(provider, kernel);
            });
    }
}
