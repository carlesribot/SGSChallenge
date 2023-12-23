using MongoDB.Driver;

namespace Infrastructure.MongoDB;

public static class MongoDbCollectionExtensions
{
    public static async Task<long> CollectionCount<T>(this IMongoCollection<T> collection, CancellationToken cancellationToken)
    {
        var count = await collection.CountDocumentsAsync(_ => true, null, cancellationToken);

        return count;
    }
}