//using Microsoft.SemanticKernel.Connectors.InMemory;

using Microsoft.Extensions.DependencyInjection;

namespace Workshops.CustomConnector.Tests;

public class TestFixture
{
    public static CustomVectorStore<TestVectorRecord> AddCustomVectorStore(IServiceProvider provider)
    {
        var collections = provider.GetRequiredService<Dictionary<string, TestVectorRecord>>();
        // TODO: Implement this method
        return new(collections);
            //new TestCustomVectorStore(new InMemoryVectorStore());
            //?? throw new NotImplementedException("You need to implement DI for CustomVectorStore in TestFixture.AddCustomVectorStore");
    }
}

//     private class TestCustomVectorStore
//         (InMemoryVectorStore inMemoryVectorStore)
//         : CustomVectorStore
//     {
//         public virtual IVectorStoreRecordCollection<TKey, TRecord> GetCollection<TKey, TRecord>(
//             string name, VectorStoreRecordDefinition? vectorStoreRecordDefinition = null)
//             where TKey : notnull
//             where TRecord : class
//             => inMemoryVectorStore.GetCollection<TKey, TRecord>(name, vectorStoreRecordDefinition);

//         public virtual IAsyncEnumerable<string> ListCollectionNamesAsync(
//             CancellationToken cancellationToken = default)
//             => inMemoryVectorStore.ListCollectionNamesAsync(cancellationToken);
//     }
