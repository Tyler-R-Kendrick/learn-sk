using Microsoft.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.ComponentModel;

namespace Agents;

#pragma warning disable SKEXP0110

public static class CustomAgentDependencyInjectionExtensions
{
    public static IServiceCollection AddCustomAgent<TCustomAgent>(
        this IServiceCollection services,
        string instructions,
        string? name = null,
        string? description = null,
        Func<IKernelBuilder, Kernel>? kernelFactory = null,
        Func<IServiceProvider, KernelArguments>? argFactory = null)
        where TCustomAgent : CustomAgent, new()
    {
        var type = typeof(TCustomAgent);
        var key = type.Name;
        kernelFactory ??= (builder => builder.Build());
        argFactory ??= (p => []);
        name ??= key;
        description ??= type.GetCustomAttribute<DescriptionAttribute>()?.Description;
        
        //TODO: Add plugins and filters.
        return services
            .AddKeyedTransient(key, (p, k) => Kernel.CreateBuilder())
            .AddKeyedTransient<Kernel>(key, (p, k) => kernelFactory(p.GetRequiredKeyedService<IKernelBuilder>(k)))
            .AddTransient<TCustomAgent>(provider => new()
            {
                Kernel = provider.GetRequiredKeyedService<Kernel>(key),
                Arguments = argFactory(provider),
                Instructions = instructions,
                Description = description,
                Name = name,
                Id = Guid.NewGuid().ToString()
            });
    }
}

#pragma warning restore SKEXP0110

