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
        var vectorStore = VectorStore;

        // Act
        var collection = vectorStore.ListCollectionNamesAsync();

        // Assert
        Assert.IsNotNull(collection);
    }
}
