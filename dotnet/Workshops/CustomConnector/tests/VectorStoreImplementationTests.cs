using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Workshops.CustomConnector.Tests;

[TestClass]
public class VectorStoreImplementationTests : TestBase
{
    [TestMethod]
    [DataRow("test1")] //TODO: Change these values to expected values
    [DataRow("test2")]
    [DataRow("test3")]
    public async Task VectorStoreRetrievesCollection(string collectionName)
    {
        // Arrange
        Configure(_ => new() { { collectionName, new("testKey1", "testValue1", new float[1536]) } });
        var vectorStore = VectorStore;

        // Act
        var collection = vectorStore.GetCollection<string, TestVectorRecord>(collectionName);
        await collection.CreateCollectionIfNotExistsAsync();

        // Assert
        Assert.IsNotNull(collection);
        Assert.IsTrue(await collection.CollectionExistsAsync());
    }

    [TestMethod]
    public void VectorStoreRetrievesCollectionNames()
    {
        // Arrange
        Dictionary<string, TestVectorRecord> collection = new()
        {
            { "testCollection1", new("testKey1", "testValue1", new float[1536]) },
            { "testCollection2", new("testKey2", "testValue2", new float[1536]) },
            { "testCollection3", new("testKey3", "testValue3", new float[1536]) }
        };
        Configure(_ => collection);
        var vectorStore = VectorStore;

        // Act
        var names = vectorStore.ListCollectionNamesAsync();

        // Assert
        Assert.IsNotNull(names);
    }
}
