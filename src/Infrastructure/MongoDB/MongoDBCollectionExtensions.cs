using Infrastructure.MongoDBDriver.Products;
using MongoDB.Driver;

namespace Infrastructure.MongoDB;

public static class MongoDbCollectionExtensions
{
    public static async Task<long> CollectionCount<T>(this IMongoCollection<T> collection, CancellationToken cancellationToken)
    {
        var count = await collection.CountDocumentsAsync(_ => true, null, cancellationToken);

        return count;
    }

    public static async Task SeedData<T>(this IMongoCollection<T> collection, long numberOfRecords, CancellationToken cancellationToken)
    {
        var emptyFilter = new FilterDefinitionBuilder<T>().Empty;
        var count = await collection.CountDocumentsAsync(emptyFilter,
            cancellationToken: cancellationToken);

        // await collection.DeleteManyAsync(emptyFilter, cancellationToken: cancellationToken);

        if (count == 0)
        {
            var productsList = GenerateProducts(numberOfRecords);
            await collection.InsertManyAsync((IEnumerable<T>)productsList, cancellationToken: cancellationToken);
        }
    }

    static decimal GenerateRandomPrice(Random rand, decimal min, decimal max) =>
        min + (decimal)(rand.NextDouble() * (double)(max - min));

    static bool GenerateRandomBoolean(Random rand) =>
        rand.Next(2) == 0;

    static List<Product> GenerateProducts(long numberOfRecords)
    {
        var productsList = new List<Product>();
        Random rand = new Random();
        for (var i = 0; i < numberOfRecords; i++)
        {
            var product = new Product
            {
                ProductId = Guid.NewGuid().ToString(),
                Name = Faker.Company.Name(),
                Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                IsActive = GenerateRandomBoolean(rand),
                Picture = $"https://picsum.photos/700/700?random={i + 1}",
                Price = GenerateRandomPrice(rand, 0, 200),
                Stock = rand.Next(1, 100),
                Category = Faker.Name.First()
            };

            productsList.Add(product);
        }

        return productsList;
    }
}
