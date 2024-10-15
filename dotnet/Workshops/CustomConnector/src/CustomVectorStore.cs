using System.Runtime.CompilerServices;
using Microsoft.SemanticKernel.Data;

namespace Workshops.CustomConnector;

public class CustomVectorStore<TVector>(
    Dictionary<string, TVector> collections)
    : IVectorStore
{
    public virtual IVectorStoreRecordCollection<TKey, TRecord> GetCollection<TKey, TRecord>(
        string name, VectorStoreRecordDefinition? vectorStoreRecordDefinition = null)
        where TKey : notnull
        where TRecord : class
    {
        if(collections.ContainsKey(name))
        {
            return new VolatileVectorStoreRecordCollection<TKey, TRecord>(name);
        }
        else
        {
            throw new KeyNotFoundException($"Collection {name} not found");
        }
    }

    public virtual async IAsyncEnumerable<string> ListCollectionNamesAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var collection in collections.Keys)
        {
            if (cancellationToken.IsCancellationRequested) yield break;
            else yield return collection;
        }
        await Task.CompletedTask;
    }
}
