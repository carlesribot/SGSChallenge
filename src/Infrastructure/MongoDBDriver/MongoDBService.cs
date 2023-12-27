using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.MongoDBDriver;

public sealed class MongoDbService
{
    private readonly MongoDbOption _options;
    private readonly Lazy<MongoClient> _mongoClient;

    private MongoClient MongoClient => _mongoClient.Value;

    public MongoDbService(IOptions<MongoDbOption> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _mongoClient = new Lazy<MongoClient>(new MongoClient(options.Value.ConnectionString));
    }

    public IMongoDatabase GetMongoDatabase()
    {
        return MongoClient.GetDatabase(_options.DatabaseName);
    }

    public bool CollectionExists(string collectionName)
    {
        var filter = new BsonDocument("name", collectionName);
        var collection = GetMongoDatabase().ListCollections(new ListCollectionsOptions { Filter = filter });
        return collection.Any();
    }
}