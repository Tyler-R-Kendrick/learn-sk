using Microsoft.Extensions.DependencyInjection;

namespace Agents;

public static class CustomAgentDependencyInjectionExtensions
{
    public static IServiceCollection AddCustomAgent(
        this IServiceCollection services)
    {
        //TODO: Add plugins and filters.
        services.AddSingleton<CustomAgent>();
        return services;
    }
}
