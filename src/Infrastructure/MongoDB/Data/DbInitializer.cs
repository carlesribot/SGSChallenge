using Infrastructure.MongoDB.Products;
using MongoDB.Driver;
using MongoDB.Entities;

namespace Infrastructure.MongoDB.Data;

public sealed class DbInitializer
{
    static decimal GenerateRandomPrice(Random rand, decimal min, decimal max) =>
        min + (decimal)(rand.NextDouble() * (double)(max - min));

    static bool GenerateRandomBoolean(Random rand) =>
        rand.Next(2) == 0;


    public static async Task InitDb()
    {
        await DB.InitAsync("searchDB", MongoClientSettings.FromConnectionString("mongodb://root:mongopw@localhost"));

        await DB.Index<Product>()
            .Key(x => x.Name, KeyType.Text)
            .Key(x => x.Category, KeyType.Text)
            .CreateAsync();

        //await DB.DeleteAsync<Product>(b => true);
        var count = await DB.CountAsync<Product>();
        Random rand = new Random();

        if (count == 0)
        {
            const long records = 2000;
            Console.WriteLine("No data - will attempt to seed");

            var data = new List<Product>();
            for (int i = 0; i < records; i++)
            {
                var product = new Product
                {
                    Name = Faker.Company.Name(),
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    IsActive = GenerateRandomBoolean(rand),
                    Picture = $"https://picsum.photos/700/700?random={i + 1}",
                    Price = GenerateRandomPrice(rand, 0, 200),
                    Stock = rand.Next(1, 100),
                    Category = Faker.Name.First()
                };

                data.Add(product);

                await data.SaveAsync();

                if (i % 10 == 0)
                    Console.WriteLine($"Added Record - {i} Added into the database");
            }

            Console.WriteLine($"Complete - {records} Added into the database");
        }
    }
}