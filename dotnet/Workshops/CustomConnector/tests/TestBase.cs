using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Workshops.CustomConnector.Tests;

public class TestBase
{
    private Lazy<IServiceProvider> _lazyServiceProvider = default!;
    private readonly ServiceCollection _services = new();
    protected IVectorStore VectorStore => _lazyServiceProvider.Value.GetRequiredService<IVectorStore>();

    [TestInitialize]
    public virtual Task TestInitializeAsync()
    {
        _lazyServiceProvider = new(() =>_services
            .AddSingleton<IVectorStore>(TestFixture.AddCustomVectorStore)
            .BuildServiceProvider());
        return Task.CompletedTask;
    }

    protected void Configure(Func<IServiceProvider, Dictionary<string, TestVectorRecord>> configure)
    {
        _services.AddSingleton(configure);
    }
}
