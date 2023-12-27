using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.MongoDBDriver.Products
{
    public sealed class Product
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("product_id")]
        public string ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Picture { get; set; }

        public bool IsActive { get; set; }

        public int Stock { get; set; }
    }
}
