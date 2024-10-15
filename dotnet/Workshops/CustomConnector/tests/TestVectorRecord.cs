//using Microsoft.SemanticKernel.Connectors.InMemory;

using Microsoft.SemanticKernel.Data;

namespace Workshops.CustomConnector.Tests;

public record TestVectorRecord(string Key, string Value, ReadOnlyMemory<float> Vector)
{
    [VectorStoreRecordKey]
    public string Key { get; init; } = Key;

    [VectorStoreRecordData]
    public string Data { get; init; } = Value;

    [VectorStoreRecordVector(1536)]
    public ReadOnlyMemory<float> Vector { get; init; } = Vector;
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
