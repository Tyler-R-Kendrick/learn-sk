using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Workshops.CustomConnector.Tests;

[TestClass]
public class VectorCollectionImplementationTests : TestBase
{
    [TestMethod]
    [DataRow("testKey1")]
    [DataRow("testKey2")]
    [DataRow("testKey3")]
    public async Task VectorStoreRetrievesCollectionRecord(string key)
    {
        // Arrange
        const string collectionName = "test";
        TestVectorRecord testRecord = new(key, "testValue1", new float[1536]);
        Configure(_ => new() { { collectionName, testRecord } });
        var collection = VectorStore.GetCollection<string, TestVectorRecord>(collectionName);
        await collection.CreateCollectionIfNotExistsAsync();
        await collection.UpsertAsync(testRecord);

        // Act
        var result = await collection.GetAsync(key);

        // Assert
        Assert.AreEqual(testRecord, result);
    }
}
