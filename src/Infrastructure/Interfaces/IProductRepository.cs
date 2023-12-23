
using Infrastructure.MongoDB.Products;
using Product = Infrastructure.MongoDBDriver.Products.Product;

namespace Infrastructure.Interfaces;

public interface IProductRepository
{
    public Task<(IReadOnlyCollection<Product> Results, long TotalCount, int PageCount)> GetItemsAsync(SearchParams searchParams,
        CancellationToken cancellationToken);
}