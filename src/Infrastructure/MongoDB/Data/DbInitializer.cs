using Infrastructure.MongoDB.Products;
using MongoDB.Driver;
using MongoDB.Entities;

namespace Infrastructure.MongoDB.Data;

public sealed class DbInitializer
{
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
                int randomNumber = rand.Next(2);

                var product = new Product
                {
                    Name = $"Product{i + 1}",
                    Description = "Ea cillum culpa esse ad nisi. Quis adipisicing fugiat amet dolore duis ad velit nisi reprehenderit id voluptate in anim exercitation. Fugiat id nisi velit in laboris aute reprehenderit nulla duis pariatur elit labore do. Tempor adipisicing non laboris reprehenderit elit dolore. Pariatur eu excepteur dolore qui sint magna consequat Lorem velit nisi officia aliqua.",
                    IsActive = randomNumber == 0,
                    Picture = $"https://picsum.photos/700/700?random={i + 1}",
                    Price = rand.Next(0, 1000),
                    Stock = rand.Next(1, 100),
                    Category = $"{i}"
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