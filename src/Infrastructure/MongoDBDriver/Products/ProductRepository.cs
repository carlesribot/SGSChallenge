using Infrastructure.Interfaces;
using Infrastructure.MongoDB.Products;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.MongoDBDriver.Products;

public sealed class ProductRepositoryMongoDriver : IProductRepository
{
    private const string CollectionName = "Product";

    private readonly IMongoCollection<Product> _productCollection;

    public ProductRepositoryMongoDriver(MongoDbService cosmosDbService, ILogger<ProductRepositoryMongoDriver> logger)
    {
        if (!cosmosDbService.CollectionExistsAsync(CollectionName).Result)
        {
            logger.LogInformation($"{CollectionName} collection does not exist. Creating it");
        }

        _productCollection = cosmosDbService.GetMongoDatabase().GetCollection<Product>(CollectionName);
        logger.LogInformation("Connected to Collection {0}", CollectionName);
    }

    public async Task<(IReadOnlyCollection<Product> Results, long TotalCount, int PageCount)> GetItemsAsync(SearchParams searchParams, CancellationToken cancellationToken = default)
    {
        var pageSize = searchParams.PageSize;
        var pageNumber = searchParams.PageNumber;
        var searchTerm = searchParams.SearchTerm;
        var category = searchParams.Category;
        var filteredBy = searchParams.FilteredBy;

        var builder = new FilterDefinitionBuilder<Product>();
        var filter = builder.Empty;

        if (!string.IsNullOrEmpty(searchTerm))
        {
            filter &= builder.Regex("Name", new BsonRegularExpression(searchTerm, "i"));
        }

        if (!string.IsNullOrEmpty(category))
        {
            filter &= builder.Regex("Category", new BsonRegularExpression(category, "i"));
        }

        switch (filteredBy)
        {
            case "inactive":
                filter &= builder.Eq("IsActive", false);
                filter |= builder.Where(x => x.Stock <= 0);
                break;
            case "active":
                filter &= builder.Eq("IsActive", true);
                filter &= builder.Where(x => x.Stock >= 0);
                break;
            case "available":
                filter &= builder.Where(x => x.Stock >= 0);
                break;
        }

        var sortBuilder = Builders<Product>.Sort;

        var sortDefinition = searchParams.OrderBy switch
        {
            "name-asc" => sortBuilder.Ascending("Name"),
            "name-desc" => sortBuilder.Descending("Name"),
            "price-asc" => sortBuilder.Ascending("Price"),
            "price-desc" => sortBuilder.Descending("Price"),
            "category-asc" => sortBuilder.Ascending("Category"),
            "category-desc" => sortBuilder.Descending("Category"),
            _ => sortBuilder.Ascending("Name")
        };

        var products = await _productCollection
            .Find(filter)
            .Sort(sortDefinition)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(searchParams.PageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        long totalCount = await _productCollection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        return (products, totalCount, totalPages);
    }
}